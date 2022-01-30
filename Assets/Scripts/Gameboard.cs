using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gameboard : MonoBehaviour {
    private Grid grid = null;
    public Tilemap tilemap = null;
    public Tile hoverTile = null;

    void Start() {
        grid = gameObject.GetComponent<Grid>();
    }

    void Update() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tileCoordinate = grid.WorldToCell(mouseWorldPos);
        tilemap.SetTile(tileCoordinate, hoverTile);
    }
}
