using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    private bool isDragging = true;
    public GameObject tower;
    private TowerStats temp;
    
    public static UnityEvent UpdatePathfinding = new UnityEvent();

    #region Build Methods

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuilding.current.gridLayout.LocalToCell((transform.position));
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuilding.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place(int element)
    {
        
        Vector3Int positionInt = GridBuilding.current.gridLayout.LocalToCell((transform.position));
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true; 
        isDragging = false;
        GridBuilding.current.TakeArea(areaTemp);
        temp = Instantiate(tower, transform.position, Quaternion.identity).GetComponentInChildren<TowerStats>();
        temp.ameliorations.Add(element);
        
        UpdatePathfinding.Invoke();
        Destroy(gameObject);
        
    }

    public void Upgrade(int element)
    {
        
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mouseWorldPosition, 0.001f);

        foreach (var collider in colliders)
        {
            Debug.Log(collider.gameObject.transform.position);
            TowerStats towerStats = collider.GetComponent<TowerStats>();
            if (towerStats != null && towerStats.ameliorations.Count < 3)
            {
                towerStats.ameliorations.Add(element);
                Placed = true;
                isDragging = false;
                towerStats.recalculateStats();
                Destroy(gameObject);
            }
        }
        
        
        
        
        
        


    }

    #endregion
    


    private void Update()
    {
        if (isDragging)
        {
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            SnapToGrid(mousePosition);
        }
    }

    private void SnapToGrid(Vector3 position)
    {
       
        Vector3Int cellPosition = GridBuilding.current.gridLayout.WorldToCell(position);
        transform.position = GridBuilding.current.gridLayout.CellToWorld(cellPosition);
    }
}
