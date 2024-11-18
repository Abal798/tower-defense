using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    public float basicBaseHealth;
    public float baseHealth;
    public GameObject panelGameOver;
    public GameObject panelSort;
    public GameObject ingamePanel;
    public Image healthBar;

    void Start()
    {
        baseHealth = basicBaseHealth;
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
    public void TakeDamage(float damage)
    {

        baseHealth -= damage;

        healthBar.fillAmount = baseHealth / basicBaseHealth;
        
        transform.GetChild(0).gameObject.SetActive(true);


        if (baseHealth <= 0)
        {
            Time.timeScale = 0;
            panelGameOver.SetActive(true);


        }
    }


    private void OnMouseDown()
    {
        panelSort.SetActive(true);
        ingamePanel.SetActive(false);
    }
}