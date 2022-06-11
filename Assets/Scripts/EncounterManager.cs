using UnityEngine;

public class EncounterManager : MonoBehaviour {
    // enum EncounterState {
    //     ENCOUNTER_START,
    //     PLAYER_TURN,
    //     ENEMY_TURN,
    //     ENCOUNTER_SUCCESS,
    //     ENCOUNTER_FAIL
    // }

    // private EncounterState state;

    private void Start() {
        EventManager.Instance.onPlayerTurn += OnPlayerTurn;
        EventManager.Instance.onEndPlayerTurn += EnemyTurn;

        // state = EncounterState.ENCOUNTER_START;
        EventManager.Instance.EncounterStart();
    }

    private void OnPlayerTurn() {
        // state = EncounterState.PLAYER_TURN;
        // EventManager.Instance.PlayerTurn();
    }

    private void EnemyTurn() {
        EventManager.Instance.EnemyTurn();
    }
}
