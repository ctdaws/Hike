using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    public GameObject hand;
    private Hand handScript;

    public Queue<CardTypes> c = new Queue<CardTypes>();

    public GameObject cardPrefab;

    void Start() {
        handScript = hand.GetComponent<Hand>();
        GenerateDeck();
    }

    void GenerateDeck() {
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.FIRELIGHTER);
        c.Enqueue(CardTypes.UNCOOKED_BEANS);
        c.Enqueue(CardTypes.ENERGY_BAR);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.ENERGY_BAR);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.ENERGY_BAR);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.TARP);
        c.Enqueue(CardTypes.TARP);
    }

    void OnMouseDown() {
        GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, hand.transform);

        card.GetComponent<Card>().InitialiseCard(c.Dequeue());

        float nextCardXPos = handScript.cards.Count * 1.5f;
        card.transform.localPosition = new Vector3(nextCardXPos, 0f, handScript.transform.position.z);

        handScript.cards.Add(card);
    }
}
