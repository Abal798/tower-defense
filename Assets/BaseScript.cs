using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    public float baseHP;
    public float basicBaseHP;
    public GameObject panelGameOver;
    public GameObject panelSort;
    public GameObject ingamePanel;
    public Image healthBar;

    private void Start()
    {
        baseHP = basicBaseHP;
        transform.GetChild(1).gameObject.SetActive(false);
    }


    public void TakeDamage(float damage)
    {

        baseHP -= damage;




        if (baseHP <= 0)
        {
            Time.timeScale = 0;
            panelGameOver.SetActive(true);


        }
        healthBar.fillAmount = baseHP / basicBaseHP;
        
        transform.GetChild(1).gameObject.SetActive(true);
    }


    private void OnMouseDown()
    {
        panelSort.SetActive(true);
        ingamePanel.SetActive(false);
    }
}