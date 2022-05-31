using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EncounterState {
    START,
    PLAYERTURN,
    ENEMYTURN
}

public class EncounterSystem : MonoBehaviour {
    public EncounterState state;
    public GameObject turnCounter;
    private TurnCounter turnCounterScript;

    // Start is called before the first frame update
    void Start() {
        state = EncounterState.START;
        turnCounterScript = turnCounter.GetComponent<TurnCounter>();
        // Debug.Log("turn num: " + turnCounter + ", state: " + state);
        SetupEncounter();
    }

    void SetupEncounter() {
        // map generation, create/randomise deck, put weapon in slot
        PlayerTurn();
    }

    void PlayerTurn() {
        state = EncounterState.PLAYERTURN;
        // turnCounterScript.IncrementTurnCounter();
        // draw 1 card
        // TODO: add card to hand at start of turn instead of click to draw,
        // fix issue where it tries to add card to hand before deck queue is filled
        // play 1 card
        // TODO: add check for if a card has been played this turn
    }

    public void OnEndTurnButton() {
        // if state = PLAYERTURN, move to enemy turn
        if (state == EncounterState.PLAYERTURN) {
            EnemyTurn();
        }
    }

    void EnemyTurn() {
        state = EncounterState.ENEMYTURN;
        // for now do nothing, skip to player turn
        PlayerTurn();
    }

}
