using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    private bool isDragging = true;
    public GameObject tower;
    private TowerStats temp;

    public RessourcesManager RM;
    public UIManager UIM;
    
    public static UnityEvent UpdatePathfinding = new UnityEvent();

    #region Build Methods

    private void Awake()
    {
        RM = FindObjectOfType<RessourcesManager>();
        UIM = FindObjectOfType<UIManager>();
    }


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
        GridBuilding.current.listeTowerCo.Add(area.position,temp.gameObject);
        temp.ameliorations.Add(element);
        Profiler.BeginSample("FollowChief");
        
        UpdatePathfinding.Invoke();
        
        Profiler.EndSample();
        Destroy(gameObject);
        
    }

    public void Upgrade(int element)
    {
        
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mouseWorldPosition, 0.001f);

        foreach (var collider in colliders)
        {
            Debug.Log(collider.gameObject.transform.position + "gmalaubidou" + Time.timeScale );
            TowerStats towerStats = collider.GetComponent<TowerStats>();
            if (towerStats != null && towerStats.ameliorations.Count < 3)
            {
                bool canUpgrade = false;
                
                if (towerStats.ameliorations.Count == 1)
                {
                    canUpgrade = Afford(element, towerStats.priceTwo);
                }
                else if (towerStats.ameliorations.Count == 2)
                {
                    canUpgrade = Afford(element, towerStats.priceThree);
                }
                
                Debug.Log("je peux ameliorer" + canUpgrade);

                if (canUpgrade)
                {
                    towerStats.ameliorations.Add(element);
                    Placed = true;
                    isDragging = false;
                    towerStats.recalculateStats();
                    Destroy(gameObject);
                }
                
                UIM.DisplayAlert("cannot upgrade");
                
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
    
    public bool Afford(int element, int price)
    {
        if (element == 1)
        {
            if (RM.fireSoul >= price)
            {
                RM.fireSoul -= price;
                return true;
            }
            else
            {
                UIM.DisplayAlert("cannot afford this");
                return false;
            }
                
        }
        if (element == 2)
        {
            if (RM.waterSoul >= price)
            {
                RM.waterSoul -= price;
                return true;
            }
            else
            {
                UIM.DisplayAlert("cannot afford this");
                return false;
            }
        }
        else
        {
            if (RM.plantSoul >= price)
            {
                RM.plantSoul -= price;
                return true;
            }
            else
            {
                UIM.DisplayAlert("cannot afford this");
                return false;
            }
        }
    }
}
