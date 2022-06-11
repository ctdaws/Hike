using UnityEngine;

public enum Direction {
    up,
    down,
    left,
    right
}

public class EnemyController : MonoBehaviour {
    public Direction dir;

    private void Start() {
        EventManager.Instance.onEnemyTurn += Move;
        dir = Direction.right;
    }

    private void Move() {
        var cardScript = gameObject.GetComponent<Card>();

        if (dir == Direction.right) {
            var nextTilePosition = new Vector2Int(cardScript.tilemapPosition.x + 1, cardScript.tilemapPosition.y);
            if (TilemapUtils.IsEmptyTileCell(nextTilePosition)) {
                TilemapUtils.MoveCardToCell(gameObject, nextTilePosition);
            } else {
                // Attack the tile
                TilemapUtils.AttackCard(gameObject, nextTilePosition);
            }
        }
    }
}
