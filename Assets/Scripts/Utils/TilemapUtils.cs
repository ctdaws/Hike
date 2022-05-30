using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapUtils {
    public static Tilemap tilemap;
    // The grid the tilemap is mapped to
    public static Grid grid;
    // A 2 dimensional array storing the CardModel present on a tile
    // // Bottom left is 0,0
    public static CardModel[,] gameboardData;
    // Tilemap bottom left cell position in cell coordinates
    public static Vector3Int tilemapPosition;
    // Tilemap size in cell coordinates
    public static Vector3Int tilemapSize;
    public static GameObject cardPrefab;

    public static bool IsCellInBounds(Vector3Int tileCell) {
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

    public static CardModel GetTileDataAtMousePosition() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

        if (!IsCellInBounds(tileCell)) {
            return null;
        }

        int normalisedCellX = tileCell.x - tilemapPosition.x;
        int normalisedCellY = tileCell.y - tilemapPosition.y;
        return gameboardData[normalisedCellX, normalisedCellY];
    }

    public static List<Vector3Int> GetTilesInRadius(Vector3Int tileCell, int radius) {
        var topLeftX = tileCell.x - radius;
        var topLeftY = tileCell.y + radius;

        var tilesCoords = new List<Vector3Int>();
        foreach (var x in Enumerable.Range(0, (radius * 2) + 1)) {
            foreach (var y in Enumerable.Range(0, (radius * 2) + 1)) {
                tilesCoords.Add(new Vector3Int(topLeftX + x, topLeftY - y, 0));
            }
        }

        return tilesCoords;
    }

    // Returns true if the card can be placed at the tile, false otherwise
    public static bool MoveCardToTileAtMousePosition(GameObject card) {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

        if (!IsCellInBounds(tileCell)) {
            return false;
        }

        var cardScript = card.GetComponent<Card>();
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

    public static GameObject GetCardAtMousePosition() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

        if (!IsCellInBounds(tileCell)) {
            return null;
        }

        int normalisedCellX = tileCell.x - tilemapPosition.x;
        int normalisedCellY = tileCell.y - tilemapPosition.y;
        Vector2 tilePosition = new Vector2(normalisedCellX, normalisedCellY);
        List<Transform> transforms = new List<Transform>();

        // Get the transforms of all the Cards in play
        foreach (Transform child in tilemap.transform) {
            transforms.Add(child);
        }

        // Find the Card placed on the tile, if it exists
        Transform cardTransform = transforms.Find(t => t.gameObject.GetComponent<Card>().tilemapPosition == tilePosition);

        if (cardTransform == null) {
            return null;
        }

        return cardTransform.gameObject;
    }

    public static void PlaceCardAtCell(GameObject card, int cellX, int cellY) {
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

    public static CardModel CreateCardAtCellPosition(CardTypes cardType, int cellX, int cellY) {
        GameObject card = Object.Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, tilemap.transform);
        Card cardScript = card.GetComponent<Card>();
        cardScript.InitialiseCard(cardType);
        PlaceCardAtCell(card, cellX, cellY);
        cardScript.isPlaced = true;
        card.GetComponent<BoxCollider2D>().enabled = false;
        return cardScript.data;
    }

    public static void GenerateMap() {
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

    // This sucks lol
    public static void DEBUG_logGameboardData() {
        for (int i = 0; i < gameboardData.GetLength(0); i++) {
            for (int j = 0; j < gameboardData.GetLength(1); j++) {
                if (gameboardData[i, j] != null) {
                    Debug.Log("x " + i + " y " + j + " : " + gameboardData[i,j].health);
                }
            }
        }
    }
}