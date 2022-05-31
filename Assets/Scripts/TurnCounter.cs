using System.Collections.Generic;
using UnityEngine;

public class TurnCounter : MonoBehaviour {
    public TMPro.TextMeshPro textMesh;
    public int turnCounter;

    public GameObject tilemap;
    private Gameboard gameboardScript;

    public GameObject deck;
    private Deck deckScript;

    private List<GameObject> cardsToUpdate = new List<GameObject>();

    void Start() {
        turnCounter = 0;
        gameboardScript = tilemap.GetComponent<Gameboard>();
        deckScript = deck.GetComponent<Deck>();
    }

    void Update() {
        textMesh.text = "Turn " + turnCounter;
    }

    public void IncrementTurnCounter() {
        turnCounter++;

        foreach (GameObject card in cardsToUpdate) {
            var cardScript = card.GetComponent<Card>();
            if (cardScript.data.lifetime > 0) {
                cardScript.data.lifetime--;
                gameboardScript.UpdateCard(card);
            }
        }

        deckScript.DrawCard();
    }

    public void AddCardToUpdateList(GameObject card) {
        cardsToUpdate.Add(card);
    }

    public void removeCardToUpdateList(GameObject card) {
        cardsToUpdate.Remove(card);
    }
}
