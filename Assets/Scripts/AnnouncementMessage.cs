using UnityEngine;

public class AnnouncementMessage : MonoBehaviour {
    public TMPro.TextMeshPro textMesh;

    void Start() {
        EventManager.Instance.onEncounterStart += ShowEncounterStartMessage;
        EventManager.Instance.onEncounterFailed += ShowEncounterFailedMessage;
        EventManager.Instance.onEncounterSucceeded += ShowEncounterSucceededMessage;
        EventManager.Instance.onStartOfNight += ShowStartOfNightMessage;
        EventManager.Instance.onPlayerTurn += ClearMessage;
    }

    void ShowEncounterStartMessage() {
        textMesh.text = "Place starting camp";
    }

    void ShowEncounterFailedMessage() {
        textMesh.text = "You lost";
    }

    void ShowEncounterSucceededMessage() {
        textMesh.text = "You won";
    }

    void ShowStartOfNightMessage() {
        textMesh.text = "Start of night";
    }

    void ClearMessage() {
        textMesh.text = "";
    }
}
