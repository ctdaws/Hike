using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private static EventManager _instance;
    public static EventManager Instance { get { return _instance; } }

    public event Action onEndTurn;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void EndTurn() {
        onEndTurn?.Invoke();
    }
}
