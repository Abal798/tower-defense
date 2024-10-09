using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerType : MonoBehaviour
{
    public GameObject TowerTypeInterface;
    public GameObject setFireTypeButton;
    public GameObject setElectricTypeButton;
    public GameObject setIceTypeButton;
    
    void Start()
    {
        TowerTypeInterface.SetActive(false);
    }

    // Update is called once per frame
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TowerTypeInterface.SetActive(true);
        }
    }
}
