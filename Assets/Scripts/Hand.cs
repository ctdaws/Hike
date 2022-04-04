using System.Collections.Generic;
using UnityEngine;


public class Hand : MonoBehaviour {
    public List<GameObject> cards;

    public void moveCards() {
        for (int i = 0; i < cards.Count; i++) {
            cards[i].transform.localPosition = new Vector3((i * 1.5f), 0, -1f);
        }
    }
}
