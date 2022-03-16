using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyMeter : MonoBehaviour {
    public TMPro.TextMeshProUGUI textMesh;
    public int energy;
    public int maxEnergy;

    void Start() {
        maxEnergy = 10;
        energy = maxEnergy;
        textMesh.text = "Beans: " + energy.ToString() + "/" + maxEnergy.ToString();
    }

    void Update() {

    }
}
