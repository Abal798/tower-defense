using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KamikazeBehaviour : MonsterMouvementBehaviours
{

    public bool objectifTargeted = false;
    public Vector3Int nearestTower;

// Start is called before the first frame update
    void Start()
    {
        UpdateObjectif();
    }

    void UpdateObjectif()
    {
        float minDistance = Mathf.Infinity;
        
        foreach (var tower in GridBuilding.current.listeTowerCo)
        {
            float sqrDistance = (transform.position - (Vector3)tower.Key).sqrMagnitude;
            
            if (sqrDistance < minDistance)
            {
                minDistance = sqrDistance;
                nearestTower = tower.Key;
            }
        }

        if (nearestTower != null)
        {
            objectif = nearestTower;
            objectifTargeted = true;
        }
        else
        {
            objectifTargeted = false;
            objectif = new Vector3Int(0, 0, 0);
        }
        
        UpdatePathfinding();
    }

    void Update()
    {
        if (GridBuilding.current.listeTowerCo[objectif] == null)
        {
            UpdateObjectif();
        }
    }
    
}
