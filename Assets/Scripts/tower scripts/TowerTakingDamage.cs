using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerTakingDamage : MonoBehaviour
{
    private TowerStats towerStats;
    public Image healthBar;


    private void Start()
    {
        towerStats = GetComponent<TowerStats>();
    }

    public void TakeDamage(float damage)
    {
        
        towerStats.health -= damage;




        if (towerStats.health <= 0)
        {
            List<Vector3Int> placement = new List<Vector3Int>();
            placement.Add(Vector3Int.FloorToInt(transform.parent.transform.position));
            GridBuilding.current.MainTilemap.SetTile(Vector3Int.FloorToInt(transform.parent.transform.position),GridBuilding.tileBases[TileType.Grass]);
            Destroy(transform.parent.gameObject);
            

        }

        healthBar.fillAmount = towerStats.health / towerStats.maxHealth;
        
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
