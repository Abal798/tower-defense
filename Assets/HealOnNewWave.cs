using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnNewWave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealTower()
    {
        transform.GetChild(0).GetComponent<TowerStats>().health =
            transform.GetChild(0).GetComponent<TowerStats>().maxHealth;
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
}
