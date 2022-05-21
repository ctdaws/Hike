using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
public GameObject encounterSystem;
private EncounterSystem encounterSystemScript;

    // Start is called before the first frame update
    void Start()
    {
        encounterSystemScript = encounterSystem.GetComponent<EncounterSystem>();
    }

    void OnMouseDown() {
        encounterSystemScript.OnEndTurnButton();
    }

}
