using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerType : MonoBehaviour
{
    public GameObject towerTypeButtons;
    public Renderer towerSpriteCircle;
    public Renderer towerSpriteSquare;
    public bool isSelected = false;
    public int powerType;
    void Start()
    {
        isSelected = true;
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        isSelected = !isSelected;
    }

    void Update()
    {
        if (isSelected == true)
        {
            towerSpriteCircle.material.color = Color.yellow;
            towerSpriteSquare.material.color = Color.yellow;
            towerTypeButtons.SetActive(true);
        }
        else
        {
            towerSpriteCircle.material.color = Color.white;
            towerSpriteSquare.material.color = Color.white;
            towerTypeButtons.SetActive(false);
        }
    }
}
