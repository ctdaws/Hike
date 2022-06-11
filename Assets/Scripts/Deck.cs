using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    public GameObject hand;
    private Hand handScript;

    public Queue<CardTypes> deck = new Queue<CardTypes>();

    public GameObject cardPrefab;

    void Start() {
        handScript = hand.GetComponent<Hand>();
        GenerateDeck();
        DrawCard();

        EventManager.Instance.onEndPlayerTurn += DrawCard;
    }

    void GenerateDeck() {
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.FIRELIGHTER);
        deck.Enqueue(CardTypes.UNCOOKED_BEANS);
        deck.Enqueue(CardTypes.ENERGY_BAR);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.ENERGY_BAR);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.ENERGY_BAR);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.TARP);
        deck.Enqueue(CardTypes.TARP);
    }

    public void DrawCard() {
        GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, hand.transform);

        if (deck.Count > 0) {
            card.GetComponent<Card>().InitialiseCard(deck.Dequeue());

            float nextCardXPos = handScript.cards.Count * 1.5f;
            card.transform.localPosition = new Vector3(nextCardXPos, 0f, handScript.transform.position.z);

            handScript.cards.Add(card);
        }
    }
}
