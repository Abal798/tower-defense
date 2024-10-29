using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerType : MonoBehaviour
{
    public Renderer towerSpriteCircle;
    public Renderer towerSpriteSquare;
    public bool isSelected = false;
    public int powerType = 1;
    public bool isPlaced;

    public TowerStats TS;
    
    public Color fireColor;
    public Color WaterColor;
    public Color plantColor;

    public void Start()
    {
        powerType = TS.towerType;
    }

    private void OnMouseDown()
    {
        isSelected = !isSelected;
    }

    void Update()
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
    }
}
