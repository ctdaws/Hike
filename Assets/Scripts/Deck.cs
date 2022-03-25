using UnityEngine;

public class Deck : MonoBehaviour {
    public GameObject hand;
    private Hand handScript;

    void Start() {
        handScript = hand.GetComponent<Hand>();
    }
}
