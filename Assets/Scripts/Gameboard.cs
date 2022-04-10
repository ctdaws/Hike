using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gameboard : MonoBehaviour {
    private Tilemap tilemap = null;
    private Grid grid = null;

    // Tilemap bottom left cell position in cell coordinates
    private Vector3Int tilemapPosition;
    // Tilemap size in cell coordinates
    private Vector3Int tilemapSize;

    public GameObject hand;
    private Hand handScript;

    public GameObject energyMeter;
    private EnergyMeter energyMeterScript;

    // Assuming a gameboard of 14x8
    // Bottom left is 0,0
    public CardModel[,] gameboardData = new CardModel[14, 8];

    public GameObject cardPrefab;

    void Start() {
        tilemap = gameObject.GetComponent<Tilemap>();
        grid = tilemap.layoutGrid;
        tilemapPosition = tilemap.cellBounds.position;
        tilemapSize = tilemap.cellBounds.size;
        handScript = hand.GetComponent<Hand>();
        energyMeterScript = energyMeter.GetComponent<EnergyMeter>();

        GenerateMap();
    }

    void OnMouseDown() {
        foreach(GameObject card in handScript.cards) {
            Card cardScript = card.GetComponent<Card>();
            if (cardScript.isSelected) {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

                if (IsCellInBounds(tileCell)) {
                    // Store the card attributes in gameboardData
                    // TODO: I think this should be plus not minus, need to double check
                    int normalisedCellX = tileCell.x - tilemapPosition.x;
                    int normalisedCellY = tileCell.y - tilemapPosition.y;
                    gameboardData[normalisedCellX, normalisedCellY] = cardScript.data;

                    // Move the card to overlay the tilemap
                    card.transform.position = tilemap.GetCellCenterWorld(tileCell) + new Vector3(0, 0, -1);
                    card.transform.SetParent(tilemap.transform);
                    cardScript.isPlaced = true;

                    // Manage energy
                    energyMeterScript.UpdateEnergy(cardScript.data.energyChange);
                    List<CardTypes> foodCards = new List<CardTypes>{CardTypes.UNCOOKED_BEANS, CardTypes.COOKED_BEANS, CardTypes.ENERGY_BAR};
                    if (foodCards.Contains(cardScript.cardType)) {
                        Destroy(card);
                    }

                    handScript.cards.Remove(card);
                    handScript.moveCards();
                    break;
                }
            }
        }
    }

    void PlaceCardAtCell(GameObject card, int cellX, int cellY) {
        int normalisedCellX = cellX + tilemapPosition.x;
        int normalisedCellY = cellY + tilemapPosition.y;
        Vector3Int cell = new Vector3Int(normalisedCellX, normalisedCellY, 0);

        if (IsCellInBounds(cell)) {
            Card cardScript = card.GetComponent<Card>();
            gameboardData[cellX, cellY] = cardScript.data;

            card.transform.position = grid.GetCellCenterWorld(cell) + new Vector3(0, 0, -1);
        }
    }

    CardModel CreateCardAtCellPosition(CardTypes cardType, int cellX, int cellY) {
        GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
        Card cardScript = card.GetComponent<Card>();
        cardScript.InitialiseCard(cardType);
        PlaceCardAtCell(card, cellX, cellY);
        return cardScript.data;
    }

    void GenerateMap() {
        CardModel[,] mapData = new CardModel[14, 8];

        mapData[0, 6] = CreateCardAtCellPosition(CardTypes.TREE, 0, 6);
        mapData[1, 7] = CreateCardAtCellPosition(CardTypes.TREE, 1, 7);
        mapData[10, 0] = CreateCardAtCellPosition(CardTypes.TREE, 10, 0);
        mapData[11, 0] = CreateCardAtCellPosition(CardTypes.TREE, 11, 0);
        mapData[12, 0] = CreateCardAtCellPosition(CardTypes.TREE, 12, 0);
        mapData[13, 0] = CreateCardAtCellPosition(CardTypes.TREE, 13, 0);
        mapData[11, 1] = CreateCardAtCellPosition(CardTypes.TREE, 11, 1);
        mapData[12, 1] = CreateCardAtCellPosition(CardTypes.TREE, 12, 1);
        mapData[12, 2] = CreateCardAtCellPosition(CardTypes.TREE, 12, 2);
        mapData[13, 1] = CreateCardAtCellPosition(CardTypes.TREE, 13, 1);
        mapData[13, 2] = CreateCardAtCellPosition(CardTypes.TREE, 13, 2);
        mapData[13, 3] = CreateCardAtCellPosition(CardTypes.TREE, 13, 3);

        gameboardData = mapData;
    }

    bool IsCellInBounds(Vector3Int tileCell) {
        if (
            tileCell.x >= tilemapPosition.x &&
            tileCell.x < (tilemapPosition.x + tilemapSize.x) &&
            tileCell.y >= tilemapPosition.y &&
            tileCell.y < (tilemapPosition.y + tilemapSize.y)
        ) {
            return true;
        } else {
            return false;
        }
    }

    // This sucks lol
    void DEBUG_logGameboardData() {
        for (int i = 0; i < gameboardData.GetLength(0); i++) {
            for (int j = 0; j < gameboardData.GetLength(1); j++) {
                if (gameboardData[i, j] != null) {
                    Debug.Log("x " + i + " y " + j + " : " + gameboardData[i,j].health);
                }
            }
        }
    }
}
