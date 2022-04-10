using System;
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

    public GameObject weaponSlot;

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
        GameObject cardUnderCursor = GetCardAtMousePosition();
        if (cardUnderCursor != null) {
            Card cardScript = cardUnderCursor.GetComponent<Card>();
            if (cardScript.data.type == CardTypes.TREE) {
                gameboardData[cardScript.tilemapPosition.x, cardScript.tilemapPosition.y] = new CardModel();
                Destroy(cardUnderCursor);
                energyMeterScript.UpdateEnergy(weaponSlot.GetComponentInChildren<Card>().data.energyChange);
                CreateCardInHand(CardTypes.WOOD);
            } else if (cardScript.data.type == CardTypes.WOOD) {
                GameObject selectedCard = GetSelectedCard();
                if (selectedCard != null) {
                    if (selectedCard.GetComponent<Card>().data.type == CardTypes.FIRELIGHTER && cardScript.data.type == CardTypes.WOOD)  {
                        Destroy(selectedCard);
                        cardScript.InitialiseCard(CardTypes.CAMPFIRE);
                        gameboardData[cardScript.tilemapPosition.x, cardScript.tilemapPosition.y] = cardScript.data;
                    }
                }
            }
        } else {
            foreach(GameObject card in handScript.cards) {
                Card cardScript = card.GetComponent<Card>();
                if (cardScript.isSelected) {
                    if (MoveCardToTileAtMousePosition(card, cardScript)) {
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

    GameObject GetSelectedCard() {
        return handScript.cards.Find(c => c.GetComponent<Card>().isSelected == true);
    }

    // Returns true if the card can be placed at the tile, false otherwise
    bool MoveCardToTileAtMousePosition(GameObject card, Card cardScript) {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

        if (!IsCellInBounds(tileCell)) {
            return false;
        }

        int normalisedCellX = tileCell.x - tilemapPosition.x;
        int normalisedCellY = tileCell.y - tilemapPosition.y;
        gameboardData[normalisedCellX, normalisedCellY] = cardScript.data;
        cardScript.tilemapPosition = new Vector2Int(normalisedCellX, normalisedCellY);

        card.transform.position = tilemap.GetCellCenterWorld(tileCell) + new Vector3(0, 0, -1);
        card.transform.SetParent(tilemap.transform);
        cardScript.isPlaced = true;
        card.GetComponent<BoxCollider2D>().enabled = false;
        return true;
    }

    GameObject GetCardAtMousePosition() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

        if (!IsCellInBounds(tileCell)) {
            return null;
        }

        int normalisedCellX = tileCell.x - tilemapPosition.x;
        int normalisedCellY = tileCell.y - tilemapPosition.y;
        Vector2 v = new Vector2(normalisedCellX, normalisedCellY);
        List<Transform> ts = new List<Transform>();

        foreach (Transform child in gameObject.transform) {
            ts.Add(child);
        }

        Transform g = ts.Find(t => t.gameObject.GetComponent<Card>().tilemapPosition == v);
        if (g == null) {
            return null;
        }
        return g.gameObject;
    }

    CardModel GetTileDataAtMousePosition() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

        if (!IsCellInBounds(tileCell)) {
            return null;
        }

        int normalisedCellX = tileCell.x - tilemapPosition.x;
        int normalisedCellY = tileCell.y - tilemapPosition.y;
        return gameboardData[normalisedCellX, normalisedCellY];
    }

    void PlaceCardAtCell(GameObject card, int cellX, int cellY) {
        int normalisedCellX = tilemapPosition.x + cellX;
        int normalisedCellY = tilemapPosition.y + cellY;
        Vector3Int cell = new Vector3Int(normalisedCellX, normalisedCellY, 0);

        if (IsCellInBounds(cell)) {
            Card cardScript = card.GetComponent<Card>();
            cardScript.tilemapPosition = new Vector2Int(cellX, cellY);
            gameboardData[cellX, cellY] = cardScript.data;

            card.transform.position = grid.GetCellCenterWorld(cell) + new Vector3(0, 0, -1);
        }
    }

    void CreateCardInHand(CardTypes cardType) {
        GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, hand.transform);
        Card cardScript = card.GetComponent<Card>();
        cardScript.InitialiseCard(cardType);

        float cardX = handScript.cards.Count * 1.5f;
        card.transform.localPosition = new Vector3(cardX, 0f, handScript.transform.position.z);

        handScript.cards.Add(card);
    }

    CardModel CreateCardAtCellPosition(CardTypes cardType, int cellX, int cellY) {
        GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
        Card cardScript = card.GetComponent<Card>();
        cardScript.InitialiseCard(cardType);
        PlaceCardAtCell(card, cellX, cellY);
        cardScript.isPlaced = true;
        card.GetComponent<BoxCollider2D>().enabled = false;
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
