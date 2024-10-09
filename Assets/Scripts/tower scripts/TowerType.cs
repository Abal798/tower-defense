using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerType : MonoBehaviour
{
    public GameObject TowerTypeButtons;
    public bool isSelected = false;
    public int powerType;
    void Start()
    {
        isSelected = false;
        TowerTypeButtons.SetActive(false);
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        isSelected = !isSelected;
        if (isSelected == true)
        {
            TowerTypeButtons.SetActive(true);
        }
        else
        {
            TowerTypeButtons.SetActive(false);
        }
    }
}
