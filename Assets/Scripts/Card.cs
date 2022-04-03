using UnityEngine;

public class Card : MonoBehaviour {
    public bool isSelected = false;
    public bool isPlaced = false;

    public CardTypes cardType;

    public CardModel data;

    private BoxCollider2D col;

    void Start() {
        col = gameObject.GetComponent<BoxCollider2D>();
        InitialiseCard(cardType);
    }

    public void InitialiseCard(CardTypes cardType) {
        this.cardType = cardType;

        switch (cardType) {
            case CardTypes.TARP:
                data = CardsData.getTarp();
                // For testing, colour the card
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case CardTypes.FOOD:
                data = CardsData.getFood();
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
    }

    // TODO: calling update just for this may be bad for performance,
    // though not worth worrying about this yet as all this code may get deleted
    void Update() {
        if (isPlaced) {
            isSelected = false;
        }

        if (isSelected) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, gameObject.transform.position.z);
        }
    }
    void OnMouseDown() {
        if (!isPlaced) {
            isSelected = true;
            col.enabled = false;
        }
    }
}
