using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    bool isSelected;
    
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        if (!isSelected) {
            isSelected = true;
            Debug.Log("Card selected");
        }
        else if (isSelected) {
            isSelected = false;
            Debug.Log("Card deselected");
        }
    }
}
