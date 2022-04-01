using UnityEngine;

public class Deck : MonoBehaviour {
    public GameObject hand;
    private Hand handScript;

    public GameObject cardPrefab;

    void Start() {
        handScript = hand.GetComponent<Hand>();
    }

    void OnMouseDown() {
        int numCardsInHand = handScript.cards.Count;
        float nextCardXPos = numCardsInHand * 1.5f;
        GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        card.transform.SetParent(hand.transform);
        card.transform.localPosition = new Vector3(nextCardXPos, 0f, handScript.transform.position.z);
        handScript.cards.Add(card);
    }
}
