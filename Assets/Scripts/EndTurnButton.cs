using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour {
    // public GameObject encounterSystem;
    // private EncounterSystem encounterSystemScript;

    public GameObject turnCounter;
    private TurnCounter turnCounterScript;

    void Start() {
        // encounterSystemScript = encounterSystem.GetComponent<EncounterSystem>();
        turnCounterScript = turnCounter.GetComponent<TurnCounter>();
    }

    void OnMouseDown() {
        // encounterSystemScript.OnEndTurnButton();
        turnCounterScript.IncrementTurnCounter();
    }

}
