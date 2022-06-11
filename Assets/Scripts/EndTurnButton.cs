using UnityEngine;

public class EndTurnButton : MonoBehaviour {
    void OnMouseDown() {
        EventManager.Instance.EndPlayerTurn();
    }
}
