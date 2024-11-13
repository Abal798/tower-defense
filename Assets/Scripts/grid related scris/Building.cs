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
        RM.UpdateTileNumber(area.position, 1, false);
        GridBuilding.current.TakeArea(areaTemp);
        temp = Instantiate(tower, transform.position, Quaternion.identity).GetComponentInChildren<TowerStats>();
        GridBuilding.current.listeTowerCo.Add(area.position,temp.gameObject);
        temp.ameliorations.Add(element);
        
        
        Profiler.BeginSample("FollowChief");
        
        UpdatePathfinding.Invoke();
        
        Profiler.EndSample();
        Destroy(gameObject);

        if (element == 1)
        {
            RM.nbrOfFireTower++;
        }
        else if (element == 2)
        {
            RM.nbrOfWaterTower++;
        }
        else if (element == 3)
        {
            RM.nbrOfEarthTower++;
        }
        
    }

    public void Upgrade(int element)
    {
        
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = GridBuilding.current.gridLayout.WorldToCell(mouseWorldPosition);
        
        if (GridBuilding.current.listeTowerCo.ContainsKey(cellPos))
        {
            TowerStats towerStats = GridBuilding.current.listeTowerCo[cellPos].GetComponent<TowerStats>();
            bool canUpgrade = false;
                
            if (towerStats.ameliorations.Count == 1)
            {
                if (element == 1)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfFireTower, 1)));
                }
                else if (element == 2)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfWaterTower, 1)));
                }
                else if (element == 3)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfEarthTower, 1)));
                }
                
            }
            else if (towerStats.ameliorations.Count == 2)
            {
                if (element == 1)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfFireTower, 2)));
                }
                else if (element == 2)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfWaterTower, 2)));
                }
                else if (element == 3)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfEarthTower, 2)));
                }
            }
                
            Debug.Log("je peux ameliorer" + canUpgrade);

            if (canUpgrade)
            {
                towerStats.ameliorations.Add(element);
                Placed = true;
                isDragging = false;
                towerStats.LaunchUpgradeEffects();
                towerStats.recalculateStats();
                Destroy(gameObject);
                if (element == 1)
                {
                    RM.nbrOfFireTower++;
                }
                else if (element == 2)
                {
                    RM.nbrOfWaterTower++;
                }
                else if (element == 3)
                {
                    RM.nbrOfEarthTower++;
                }
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
