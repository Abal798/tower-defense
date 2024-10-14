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
    public bool isPlaced;
    public bool firstTime = true;

    public Color selectedColor;
    public Color fireColor;
    public Color WaterColor;
    public Color plantColor;

    public Building Building;
    //public Building Building;
    void Start()
    {
        Building = transform.parent.parent.GetComponent<Building>();
    }
    

    // Update is called once per frame
    private void OnMouseDown()
    {
        isSelected = !isSelected;
    }

    void Update()
    {
        
        if (Building.Placed && firstTime)
        {
            isSelected = true;
            firstTime = false;
        }
        
        if (isSelected == true)
        {
            towerSpriteCircle.material.color = selectedColor;
            towerSpriteSquare.material.color = selectedColor;
            towerTypeButtons.SetActive(true);
        }
        else
        {
            if (powerType == 1)
            {
                towerSpriteCircle.material.color = fireColor;
                towerSpriteSquare.material.color = fireColor; 
            }
            if (powerType == 2)
            {
                towerSpriteCircle.material.color = WaterColor;
                towerSpriteSquare.material.color = WaterColor; 
            }
            if (powerType == 3)
            {
                towerSpriteCircle.material.color = plantColor;
                towerSpriteSquare.material.color = plantColor; 
            }
            towerTypeButtons.SetActive(false);
        }
    }
}
