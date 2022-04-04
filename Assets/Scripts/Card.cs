using UnityEngine;

public class Card : MonoBehaviour {
    public bool isSelected = false;
    public bool isPlaced = false;

    public CardTypes cardType;

    public CardModel data;

    private BoxCollider2D col;
    public TMPro.TextMeshProUGUI textMesh;

    void Start() {
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    public void InitialiseCard(CardTypes cardType) {
        this.cardType = cardType;

        switch (cardType) {
            case CardTypes.TARP:
                data = CardsData.getTarp();
                textMesh.text = "Tarp";
                break;
            case CardTypes.ENERGY_BAR:
                data = CardsData.getFood();
                textMesh.text = "Energy Bar";
                break;
            case CardTypes.FIRELIGHTER:
                data = CardsData.getFirelighter();
                textMesh.text = "Firelighter";
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
