using System.Collections.Generic;
using UnityEngine;


public class Hand : MonoBehaviour {
    public List<GameObject> cards;
    public GameObject cardPrefab;

    public void moveCards() {
        for (int i = 0; i < cards.Count; i++) {
            cards[i].transform.localPosition = new Vector3((i * 1.5f), 0, -1f);
        }
    }

    public GameObject GetSelectedCard() {
        return cards.Find(c => c.GetComponent<Card>().isSelected == true);
    }

    public void CreateCardInHand(CardTypes cardType) {
        GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
        Card cardScript = card.GetComponent<Card>();
        cardScript.InitialiseCard(cardType);

        float cardX = cards.Count * 1.5f;
        card.transform.localPosition = new Vector3(cardX, 0f, gameObject.transform.position.z);

        cards.Add(card);
    }
}
