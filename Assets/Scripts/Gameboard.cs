using System.Collections;
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

    public GameObject card = null;
    private SelectCard selectCard;

    // Assuming a gameboard of 14x8
    // TODO: change type from int to a data structure that can hold all the fields
    // for what effects can be applied to a tile. E.g. health, buffs etc.
    // For now, will be true if something is in the square
    // Bottom left is 0,0
    private bool[,] gameboardData = new bool[14, 8];

    void Start() {
        tilemap = gameObject.GetComponent<Tilemap>();
        grid = tilemap.layoutGrid;
        tilemapPosition = tilemap.cellBounds.position;
        tilemapSize = tilemap.cellBounds.size;
        selectCard = card.GetComponent<SelectCard>();
    }

    void OnMouseDown() {
        if (selectCard.isSelected) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

            if (canPlaceCardAtCell(tileCell)) {
                // Store the card attributes in gameboardData
                int normalisedCellX = tileCell.x - tilemapPosition.x;
                int normalisedCellY = tileCell.y - tilemapPosition.y;
                gameboardData[normalisedCellX, normalisedCellY] = true;
                // Assuming a card is 2x1
                gameboardData[normalisedCellX, normalisedCellY - 1] = true;

                // Move the card to overlay the tilemap
                float cellHeight = grid.cellSize.y;
                // This line is assuming 2x1 card in vertical position
                card.transform.position = (tilemap.GetCellCenterWorld(tileCell) + new Vector3(0f, -(cellHeight / 2), 0f));
                selectCard.isPlaced = true;
            }
        }
    }

    bool isCellInBounds(Vector3Int tileCell) {
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

    // This function assumes a card is 2x1 tiles and the tile under the cursor is the top tile of the card
    bool canPlaceCardAtCell(Vector3Int tileCell) {
        if (isCellInBounds(tileCell)) {
            return tileCell.y > tilemapPosition.y;
        }
        return false;
    }

    // This sucks lol
    void DEBUG_logGameboardData() {
        for (int i = 0; i < gameboardData.GetLength(0); i++) {
            for (int j = 0; j < gameboardData.GetLength(1); j++) {
                Debug.Log("x " + i + " y " + j + " : " + gameboardData[i,j]);
            }
        }
    }
}
