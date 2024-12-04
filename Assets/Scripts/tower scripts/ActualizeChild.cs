using System;
using System.Collections;
using UnityEngine;

public class ActualizeChild : MonoBehaviour
{
    public TowerStats towerStats;
    
    private void Start()
    {
        towerStats = transform.GetChild(0).gameObject.GetComponent<TowerStats>();
    }
    
    public void AcutalizeChild() // Fixed method name
    {
        if (towerStats != null) towerStats.recalculateStats();
    }

    private void OnDestroy()
    {
        GridBuilding.current.listeTowerCo.Remove(Vector3Int.FloorToInt(transform.position));
    }

    public void HealTower()
    {
        towerStats.Heal();
    }
}

