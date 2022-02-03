using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard : MonoBehaviour
{
    public bool isSelected;

    void Start()
    {
        isSelected = false;
    }

    void OnMouseDown() {
        if (!isSelected) {
            isSelected = true;
        }
        else if (isSelected) {
            isSelected = false;
        }
    }
}
