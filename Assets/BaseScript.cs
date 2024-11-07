using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour
{
    public float baseHP;
    public GameObject panelGameOver;

    public void TakeDamage(float damage)
    {

        baseHP -= damage;




        if (baseHP <= 0)
        {
            Time.timeScale = 0;
            panelGameOver.SetActive(true);


        }
    }
}