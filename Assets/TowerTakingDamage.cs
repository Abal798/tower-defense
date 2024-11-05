using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTakingDamage : MonoBehaviour
{
    private TowerStats towerStats;


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
            Debug.Log("???????????????????????????????????????????????");
            Destroy(transform.parent.gameObject);
            

        }
    }
}
