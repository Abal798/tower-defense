using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIDisplay : MonoBehaviour
{
    private BaseScript baseScript;
    public Image healthBar;


    private void Start()
    {
        baseScript = GetComponent<BaseScript>();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        
        baseScript.baseHealth -= damage;


    }
}
