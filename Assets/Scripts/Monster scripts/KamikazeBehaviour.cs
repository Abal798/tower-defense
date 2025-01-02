using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBehaviour : MonsterMouvementBehaviours
{
    public bool objectifTargeted = false;
    public Vector3Int nearestTower;

    private void Start()
    {
        if (gridLayout == null)
        {
            GameObject gridObject = GameObject.FindGameObjectWithTag("grid");
            if (gridObject == null)
            {
                Debug.LogError("No object with tag 'grid' found in the scene!");
                return;
            }

            gridLayout = gridObject.GetComponent<Grid>();
            if (gridLayout == null)
            {
                Debug.LogError("The object with tag 'grid' does not have a Grid component!");
                return;
            }
        }
        UpdateObjectif();
    }

    private void UpdateObjectif()
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

    private void Update()
    {
        if (!GridBuilding.current.listeTowerCo.ContainsKey(objectif) || GridBuilding.current.listeTowerCo[objectif] == null)
        {
            UpdateObjectif();
        }
    }
}