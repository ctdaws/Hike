using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gameboard : MonoBehaviour {
    public Tilemap tilemap;
    public Grid grid;

    public GameObject hand;
    private Hand handScript;

    public GameObject energyMeter;
    private EnergyMeter energyMeterScript;

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

        TilemapUtils.GenerateMap();
    }

    void OnMouseDown() {
        GameObject cardUnderCursor = TilemapUtils.GetCardAtMousePosition();
        if (cardUnderCursor != null) {
            Card cardScript = cardUnderCursor.GetComponent<Card>();
            if (cardScript.data.type == CardTypes.TREE) {
                TilemapUtils.gameboardData[cardScript.tilemapPosition.x, cardScript.tilemapPosition.y] = new CardModel();
                Destroy(cardUnderCursor);
                energyMeterScript.UpdateEnergy(weaponSlot.GetComponentInChildren<Card>().data.energyChange);
                handScript.CreateCardInHand(CardTypes.WOOD);
            } else if (cardScript.data.type == CardTypes.WOOD) {
                GameObject selectedCard = handScript.GetSelectedCard();
                if (selectedCard != null) {
                    if (selectedCard.GetComponent<Card>().data.type == CardTypes.FIRELIGHTER)  {
                        energyMeterScript.UpdateEnergy(selectedCard.GetComponent<Card>().data.energyChange);
                        handScript.cards.Remove(selectedCard);
                        handScript.moveCards();
                        Destroy(selectedCard);
                        cardScript.InitialiseCard(CardTypes.CAMPFIRE);
                        TilemapUtils.gameboardData[cardScript.tilemapPosition.x, cardScript.tilemapPosition.y] = cardScript.data;
                        // Campfire light radius
                        Vector3Int tileCell = grid.WorldToCell(cardUnderCursor.transform.position);
                        var tiles = TilemapUtils.GetTilesInRadius(tileCell, 2);
                        foreach (var tile in tiles) {
                            // Flag the tile, inidicating that it can change colour.
                            // By default it's set to "Lock Colour".
                            tilemap.SetTileFlags(tile, TileFlags.None);

                            // Set the colour.
                            tilemap.SetColor(tile, Color.red);
                        }
                    }
                }
            } else if (cardScript.data.type == CardTypes.CAMPFIRE) {
                GameObject selectedCard = handScript.GetSelectedCard();
                if (selectedCard != null) {
                    if (selectedCard.GetComponent<Card>().data.type == CardTypes.UNCOOKED_BEANS)  {
                        // energyMeterScript.UpdateEnergy(selectedCard.GetComponent<Card>().data.energyChange);
                        handScript.cards.Remove(selectedCard);
                        Destroy(selectedCard);
                        handScript.CreateCardInHand(CardTypes.COOKED_BEANS);
                        handScript.moveCards();
                    }
                }
            }
        } else {
            foreach(GameObject card in handScript.cards) {
                Card cardScript = card.GetComponent<Card>();
                if (cardScript.isSelected) {
                    if (TilemapUtils.MoveCardToTileAtMousePosition(card, cardScript)) {
                        // Manage energy
                        energyMeterScript.UpdateEnergy(cardScript.data.energyChange);
                        List<CardTypes> foodCards = new List<CardTypes>{CardTypes.UNCOOKED_BEANS, CardTypes.COOKED_BEANS, CardTypes.ENERGY_BAR};
                        if (foodCards.Contains(cardScript.data.type)) {
                            Destroy(card);
                        }

                        handScript.cards.Remove(card);
                        handScript.moveCards();
                        break;
                    }
                }
            }
        }
    }
}
