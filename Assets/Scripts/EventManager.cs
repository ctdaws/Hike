using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    private static EventManager _instance;
    public static EventManager Instance { get { return _instance; } }

    public event Action onEndPlayerTurn;
    public event Action onPlayerTurn;
    public event Action onEnemyTurn;
    public event Action onEncounterStart;
    public event Action onEncounterFailed;
    public event Action onEncounterSucceeded;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public void EndPlayerTurn() {
        onEndPlayerTurn?.Invoke();
    }

    public void EnemyTurn() {
        onEnemyTurn?.Invoke();
    }

    public void EncounterStart() {
        onEncounterStart?.Invoke();
    }

    public void PlayerTurn() {
        onPlayerTurn?.Invoke();
    }

    public void EncounterFailed() {
        onEncounterFailed?.Invoke();
    }

    public void EncounterSucceeded() {
        onEncounterSucceeded?.Invoke();
    }
}
