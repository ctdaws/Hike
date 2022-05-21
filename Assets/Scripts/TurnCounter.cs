using UnityEngine;

public class TurnCounter : MonoBehaviour {
    public TMPro.TextMeshPro textMesh;
    public int turnCounter;

    void Start() {
        turnCounter = 0;
    }

    void Update() {
        textMesh.text = "Turn " + turnCounter;
    }

    public void IncrementTurnCounter() {
        turnCounter++;
    }
}
