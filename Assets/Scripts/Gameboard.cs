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

    void Start() {
        tilemap = gameObject.GetComponent<Tilemap>();
        grid = tilemap.layoutGrid;
        tilemapPosition = tilemap.cellBounds.position;
        tilemapSize = tilemap.cellBounds.size;
        handScript = hand.GetComponent<Hand>();
        energyMeterScript = energyMeter.GetComponent<EnergyMeter>();
    }

    void OnMouseDown() {
        foreach(GameObject card in handScript.cards) {
            Card cardScript = card.GetComponent<Card>();
            if (cardScript.isSelected) {
                Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int tileCell = grid.WorldToCell(mouseWorldPos);

                if (isCellInBounds(tileCell)) {
                    // Store the card attributes in gameboardData
                    int normalisedCellX = tileCell.x - tilemapPosition.x;
                    int normalisedCellY = tileCell.y - tilemapPosition.y;
                    gameboardData[normalisedCellX, normalisedCellY] = cardScript.data;

                    // Move the card to overlay the tilemap
                    card.transform.position = tilemap.GetCellCenterWorld(tileCell);
                    cardScript.isPlaced = true;

                    // Manage energy
                    if (cardScript.cardType != CardTypes.ENERGY_BAR) {
                        energyMeterScript.energy--;
                    } else {
                        int newEnergy = energyMeterScript.energy += cardScript.data.energyRestored;
                        if (newEnergy > energyMeterScript.maxEnergy) {
                            energyMeterScript.energy = energyMeterScript.maxEnergy;
                        }
                        Destroy(card);
                    }

                    handScript.cards.Remove(card);
                    handScript.moveCards();
                    break;
                }
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
