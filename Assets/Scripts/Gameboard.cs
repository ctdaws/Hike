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
    public GameObject enemyCardPrefab;

    public GameObject weaponSlot;

    private bool isEncounterStart = true;

    void Start() {
        TilemapUtils.tilemap = tilemap;
        TilemapUtils.grid = grid;
        // TODO: Change this to when we have a flexible gameboard size
        TilemapUtils.gameboardData = new CardModel[14,8];
        TilemapUtils.tilemapPosition = tilemap.cellBounds.position;
        TilemapUtils.tilemapSize = tilemap.cellBounds.size;
        TilemapUtils.cardPrefab = cardPrefab;
        TilemapUtils.enemyCardPrefab = enemyCardPrefab;

        handScript = hand.GetComponent<Hand>();
        energyMeterScript = energyMeter.GetComponent<EnergyMeter>();
        turnCounterScript = turnCounter.GetComponent<TurnCounter>();

        EventManager.Instance.onPlayerTurn += ClearEncounterStartFlag;

        TilemapUtils.GenerateMap();
    }

    void OnMouseDown() {
        if (isEncounterStart) {
            var tile = TilemapUtils.GetTileAtMousePosition();
            if (TilemapUtils.IsCellInBounds(tile)) {
                var cell = TilemapUtils.ConvertTilemapPositionToCellPosition(tile);
                if (TilemapUtils.IsEmptyTileCell(cell)) {
                    TilemapUtils.CreateCardAtCellPosition(CardTypes.CAMP, cell.x, cell.y);
                    EventManager.Instance.PlayerTurn();
                }
                return;
            }
        }

        var selectedCard = handScript.GetSelectedCard();
        var cardUnderCursor = TilemapUtils.GetCardAtMousePosition();

        if ((cardUnderCursor == null) && (selectedCard == null)) {
            return;
        }

        if (selectedCard != null) {
            if (cardUnderCursor == null) {
                PlaceCard(selectedCard);
                EventManager.Instance.EndPlayerTurn();
                return;
            }
        }

        if (cardUnderCursor != null) {
            if(selectedCard == null) {
                if (CardsData.IsTree(cardUnderCursor)) {
                    handScript.CreateCardInHand(CardTypes.WOOD);
                }

                var weaponCard = weaponSlot.transform.GetChild(0).gameObject;
                TilemapUtils.AttackCard(weaponCard, cardUnderCursor);
                energyMeterScript.ProcessEnergyCost(weaponCard);
                EventManager.Instance.EndPlayerTurn();
                return;
            }
        }

        Assert.IsNotNull(selectedCard);
        Assert.IsNotNull(cardUnderCursor);

        if (CardsData.IsFireLightingCard(selectedCard)) {
            if(CardsData.IsFuelCard(cardUnderCursor)) {
                CreateCampfire(selectedCard, cardUnderCursor);
                EventManager.Instance.EndPlayerTurn();
            }
        } else if (CardsData.IsCookableCard(selectedCard)) {
            if(CardsData.IsCampfire(cardUnderCursor)) {
                CookCard(selectedCard);
                EventManager.Instance.EndPlayerTurn();
            }
        }
    }

    private void ClearEncounterStartFlag() {
        isEncounterStart = false;
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

        Vector3Int tileCell = grid.WorldToCell(cardUnderCursor.transform.position);
        TilemapUtils.LightTilesInRadius(tileCell, cardScript.data.lifetime);
    }
}
