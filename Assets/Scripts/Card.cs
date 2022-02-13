using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    public bool isSelected = false;
    public bool isPlaced = false;

    public CardData data;

    private BoxCollider2D col;

    void Start() {
        col = gameObject.GetComponent<BoxCollider2D>();
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
