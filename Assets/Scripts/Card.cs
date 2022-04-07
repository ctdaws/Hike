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
        InitialiseCard(cardType);
    }

    public void InitialiseCard(CardTypes cardType) {
        this.cardType = cardType;

        switch (cardType) {
            case CardTypes.TARP:
                data = CardsData.getTarp();
                textMesh.text = "Tarp";
                break;
            case CardTypes.ENERGY_BAR:
                data = CardsData.getEnergyBar();
                textMesh.text = "Energy Bar";
                break;
            case CardTypes.FIRELIGHTER:
                data = CardsData.getFirelighter();
                textMesh.text = "Firelighter";
                break;
            case CardTypes.UNCOOKED_BEANS:
                data = CardsData.getUncookedBeans();
                textMesh.text = "Uncooked Beans";
                break;
            case CardTypes.COOKED_BEANS:
                data = CardsData.getCookedBeans();
                textMesh.text = "Cooked Beans";
                break;
            case CardTypes.TREE:
                data = CardsData.getTree();
                textMesh.text = "Tree";
                break;
            case CardTypes.CAMPFIRE:
                data = CardsData.getCampfire();
                textMesh.text = "Campfire";
                break;
            case CardTypes.AXE:
                data = CardsData.getAxe();
                textMesh.text = "Axe";
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
