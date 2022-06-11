using UnityEngine;

public class TurnCounter : MonoBehaviour {
    public TMPro.TextMeshPro textMesh;
    public int turnCounter;

    private int startOfNight = 2;
    private int endOfNight = 4;

    void Start() {
        turnCounter = 0;
        textMesh.text = "Turn " + turnCounter;
        EventManager.Instance.onEndPlayerTurn += IncrementTurnCounter;
    }

    public void IncrementTurnCounter() {
        turnCounter++;

        if (turnCounter == startOfNight) {
            EventManager.Instance.StartOfNight();
        } else if (turnCounter == endOfNight) {
            EventManager.Instance.EncounterSucceeded();
        }

        textMesh.text = "Turn " + turnCounter;
    }
}
