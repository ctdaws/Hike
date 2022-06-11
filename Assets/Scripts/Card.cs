using UnityEngine;

public class Card : MonoBehaviour {
    public bool isSelected = false;
    public bool isPlaced = false;

    public CardModel data;
    public Vector2Int tilemapPosition;

    private BoxCollider2D col;
    public TMPro.TextMeshProUGUI cardName;
    public TMPro.TextMeshProUGUI health;
    public TMPro.TextMeshProUGUI attack;

    private void Start() {
        col = gameObject.GetComponent<BoxCollider2D>();
        InitialiseCard(data.type);

        EventManager.Instance.onEndPlayerTurn += onEndPlayerTurn;
    }

    public void InitialiseCard(CardTypes cardType) {
        switch (cardType) {
            case CardTypes.CAMP:
                data = CardsData.getCamp();
                cardName.text = "Camp";
                health.text = data.health.ToString();
                break;
            case CardTypes.TARP:
                data = CardsData.getTarp();
                cardName.text = "Tarp";
                health.text = data.health.ToString();
                break;
            case CardTypes.ENERGY_BAR:
                data = CardsData.getEnergyBar();
                cardName.text = "Energy Bar";
                break;
            case CardTypes.FIRELIGHTER:
                data = CardsData.getFirelighter();
                cardName.text = "Firelighter";
                break;
            case CardTypes.UNCOOKED_BEANS:
                data = CardsData.getUncookedBeans();
                cardName.text = "Uncooked Beans";
                break;
            case CardTypes.COOKED_BEANS:
                data = CardsData.getCookedBeans();
                cardName.text = "Cooked Beans";
                break;
            case CardTypes.TREE:
                data = CardsData.getTree();
                cardName.text = "Tree";
                health.text = data.health.ToString();
                attack.text = "";
                break;
            case CardTypes.CAMPFIRE:
                data = CardsData.getCampfire();
                cardName.text = "Campfire";
                health.text = data.health.ToString();
                break;
            case CardTypes.AXE:
                data = CardsData.getAxe();
                cardName.text = "Axe";
                attack.text = data.attack.ToString();
                break;
            case CardTypes.WOOD:
                data = CardsData.getWood();
                cardName.text = "Wood";
                health.text = data.health.ToString();
                break;
            case CardTypes.WOLF:
                data = CardsData.getWolf();
                cardName.text = "Wolf";
                health.text = data.health.ToString();
                attack.text = data.attack.ToString();
                break;
        }
    }

    // TODO: calling update just for this may be bad for performance,
    // though not worth worrying about this yet as all this code may get deleted
    private void Update() {
        if (isPlaced) {
            isSelected = false;
        }

        if (isSelected) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, gameObject.transform.position.z);
        }
    }

    private void OnMouseDown() {
        if (!isPlaced) {
            isSelected = true;
            col.enabled = false;
        }
    }

    private void onEndPlayerTurn() {
        if (data.lifetime > 0) {
            data.lifetime--;

            if (CardsData.IsCampfire(gameObject)) {
                var tileCell = TilemapUtils.ConvertCellPositionToTilemapPosition(tilemapPosition);
                TilemapUtils.UpdateTilesLightingInRadius(tileCell, data.lifetime + 1, data.lifetime);
            }
        }
    }



    public void UpdateHealth() {
        health.text = data.health.ToString();
    }

    private void OnDestroy() {
        if (CardsData.IsCampfire(gameObject)) {
            var tileCell = TilemapUtils.ConvertCellPositionToTilemapPosition(tilemapPosition);
            TilemapUtils.UpdateTilesLightingInRadius(tileCell, data.lifetime, 0);
        }
        EventManager.Instance.onEndPlayerTurn -= onEndPlayerTurn;
    }
}
