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

    void Start() {
        tilemap = gameObject.GetComponent<Tilemap>();
        grid = tilemap.layoutGrid;
        tilemapPosition = tilemap.cellBounds.position;
        tilemapSize = tilemap.cellBounds.size;
        selectCard = card.GetComponent<SelectCard>();
        Debug.Log(grid.cellSize);
    }

    void OnMouseDown() {
        if (selectCard.isSelected) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

            if (canPlaceCardAtCell(tileCell)) {
                float cellHeight = grid.cellSize.y;
                // This line is assuming 2x1 card in vertical position
                card.transform.position = (tilemap.GetCellCenterWorld(tileCell) + new Vector3(0f, -(cellHeight / 2), 0f));
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
}
