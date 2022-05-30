using UnityEngine;

public class EnergyMeter : MonoBehaviour {
    public TMPro.TextMeshProUGUI textMesh;
    public int energy;
    public int maxEnergy;

    void Start() {
        maxEnergy = 10;
        energy = maxEnergy;
    }

    void Update() {
        textMesh.text = "Beans: " + energy.ToString() + "/" + maxEnergy.ToString();
    }

    public void ProcessEnergyCost(GameObject card) {
        var cardScript = card.GetComponent<Card>();

        energy += cardScript.data.energyChange;

        if (energy > maxEnergy) {
            energy = maxEnergy;
        } else if (energy < 0) {
            energy = 0;
        }
    }
}
