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
