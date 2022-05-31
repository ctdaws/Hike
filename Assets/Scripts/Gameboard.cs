using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class Gameboard : MonoBehaviour {
    public Tilemap tilemap;
    public Grid grid;

    public GameObject hand;
    private Hand handScript;

    public GameObject energyMeter;
    private EnergyMeter energyMeterScript;

    public GameObject turnCounter;
    private TurnCounter turnCounterScript;

    public GameObject cardPrefab;

    public GameObject weaponSlot;

    void Start() {
        TilemapUtils.tilemap = tilemap;
        TilemapUtils.grid = grid;
        // TODO: Change this to when we have a flexible gameboard size
        TilemapUtils.gameboardData = new CardModel[14,8];
        TilemapUtils.tilemapPosition = tilemap.cellBounds.position;
        TilemapUtils.tilemapSize = tilemap.cellBounds.size;
        TilemapUtils.cardPrefab = cardPrefab;

        handScript = hand.GetComponent<Hand>();
        energyMeterScript = energyMeter.GetComponent<EnergyMeter>();
        turnCounterScript = turnCounter.GetComponent<TurnCounter>();

        TilemapUtils.GenerateMap();
    }

    void OnMouseDown() {
        var selectedCard = handScript.GetSelectedCard();
        var cardUnderCursor = TilemapUtils.GetCardAtMousePosition();

        if ((cardUnderCursor == null) && (selectedCard == null)) {
            return;
        }

        if (selectedCard != null) {
            if (cardUnderCursor == null) {
                PlaceCard(selectedCard);
                turnCounterScript.IncrementTurnCounter();
                return;
            }
        }

        if (cardUnderCursor != null) {
            if(selectedCard == null) {
                if (CardsData.IsTree(cardUnderCursor)) {
                    ChopTree(cardUnderCursor);
                    turnCounterScript.IncrementTurnCounter();
                }
                return;
            }
        }

        Assert.IsNotNull(selectedCard);
        Assert.IsNotNull(cardUnderCursor);

        if (CardsData.IsFireLightingCard(selectedCard)) {
            if(CardsData.IsFuelCard(cardUnderCursor)) {
                CreateCampfire(selectedCard, cardUnderCursor);
                turnCounterScript.IncrementTurnCounter();
            }
        } else if (CardsData.IsCookableCard(selectedCard)) {
            if(CardsData.IsCampfire(cardUnderCursor)) {
                CookCard(selectedCard);
                turnCounterScript.IncrementTurnCounter();
            }
        }
    }

    private void PlaceCard(GameObject card) {
        if (TilemapUtils.MoveCardToTileAtMousePosition(card)) {
            energyMeterScript.ProcessEnergyCost(card);

            if (CardsData.IsFoodCard(card)) {
                Destroy(card);
            }

            handScript.RemoveCard(card);
        }
    }

    private void ChopTree(GameObject tree) {
        var cardScript = tree.GetComponent<Card>();

        TilemapUtils.gameboardData[cardScript.tilemapPosition.x, cardScript.tilemapPosition.y] = new CardModel();

        Destroy(tree);

        var weaponCard = weaponSlot.transform.GetChild(0).gameObject;
        energyMeterScript.ProcessEnergyCost(weaponCard);

        handScript.CreateCardInHand(CardTypes.WOOD);
    }

    private void CookCard(GameObject card) {
        handScript.RemoveCard(card);
        Destroy(card);

        handScript.CreateCardInHand(CardTypes.COOKED_BEANS);
    }

    private void CreateCampfire(GameObject selectedCard, GameObject cardUnderCursor) {
        energyMeterScript.ProcessEnergyCost(selectedCard);

        handScript.RemoveCard(selectedCard);
        Destroy(selectedCard);

        var cardScript = cardUnderCursor.GetComponent<Card>();
        cardScript.InitialiseCard(CardTypes.CAMPFIRE);
        TilemapUtils.gameboardData[cardScript.tilemapPosition.x, cardScript.tilemapPosition.y] = cardScript.data;
        turnCounterScript.AddCardToUpdateList(cardUnderCursor);

        Vector3Int tileCell = grid.WorldToCell(cardUnderCursor.transform.position);
        TilemapUtils.LightTilesInRadius(tileCell, cardScript.data.lifetime);
    }

    public void UpdateCard(GameObject card) {
        Vector3Int tileCell = grid.WorldToCell(card.transform.position);
        var cardScript = card.GetComponent<Card>();
        TilemapUtils.UpdateTilesLightingInRadius(tileCell, cardScript.data.lifetime + 1, cardScript.data.lifetime);

    }
}
