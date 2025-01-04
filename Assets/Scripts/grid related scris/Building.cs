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
    public int elementToPlace = 3;
    

    public RessourcesManager RM;
    public UIManager UIM;
    
    public static UnityEvent UpdatePathfinding = new UnityEvent();


    private void Start()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        if (elementToPlace == 3)
        {
            float radius = (6 + (RM.earthEffectThree ));
            Vector3 radiusDisplayScale = new Vector3(radius * 2, radius * 2, 1);
            transform.GetChild(2).localScale = radiusDisplayScale;
        }
        else
        {
            float radius = 6;
            Vector3 radiusDisplayScale = new Vector3(radius * 2, radius * 2, 1);
            transform.GetChild(2).localScale = radiusDisplayScale;
        }
        
    }

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
        AudioManager.AM.PlaySfx(AudioManager.AM.towerSpawn);
        Vector3Int positionInt = GridBuilding.current.gridLayout.LocalToCell((transform.position));
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true; 
        isDragging = false;
        RM.UpdateTileNumber(area.position, 1, false);
        GridBuilding.current.TakeArea(areaTemp);
        temp = Instantiate(tower, transform.position, Quaternion.identity).GetComponentInChildren<TowerStats>();
        GridBuilding.current.listeTowerCo.Add(area.position,temp.gameObject.transform.parent.gameObject);
        temp.ameliorations.Add(element);
        AudioManager.AM.PlaySfx(AudioManager.AM.towerSpawn);
        
        Profiler.BeginSample("InvokePathfinding");
        
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
            TowerStats towerStats = GridBuilding.current.listeTowerCo[cellPos].transform.GetChild(0).GetComponent<TowerStats>();
            bool canUpgrade = false;
                
            if (towerStats.ameliorations.Count == 1 || towerStats.ameliorations.Count == 2)
            {
                if (element == 1)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfFireTower)));
                }
                else if (element == 2)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfWaterTower)));
                }
                else if (element == 3)
                {
                    canUpgrade = Afford(element, Mathf.FloorToInt(RM.GetTowerPrice(element, RM.nbrOfEarthTower)));
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
                AudioManager.AM.PlaySfx(AudioManager.AM.towerUpgrade);
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

        Vector3 lastpos = transform.position;
        if (lastpos == transform.position)
        {
            Debug.Log("ahhhhhhh");
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = GridBuilding.current.gridLayout.WorldToCell(mouseWorldPosition);

            if (GridBuilding.current.listeTowerCo.ContainsKey(cellPos))
            {
                GameObject towerToPreview = GridBuilding.current.listeTowerCo[cellPos];
                TowerStats towerStatsToPreview = towerToPreview.transform.GetChild(0).GetComponent<TowerStats>();
                if (towerStatsToPreview.ameliorations.Count < 3)
                {
                    int nbrOfEarthInsuflation = 0;
                    for (int i = 0; i < towerStatsToPreview.ameliorations.Count; i++)
                    {
                        if (towerStatsToPreview.ameliorations[i] == 3)
                        {
                            nbrOfEarthInsuflation++;
                        }
                    }
                    if (elementToPlace == 3) nbrOfEarthInsuflation += 1;

                    float radius = (6 + (RM.earthEffectThree * nbrOfEarthInsuflation)) + (RM.earthEffectTwo * towerStatsToPreview.earthSurrounding);
                    Vector3 radiusDisplayScale = new Vector3(radius * 2, radius * 2, 1);
                    transform.GetChild(2).localScale = radiusDisplayScale;
                }
                else
                {
                    transform.GetChild(2).localScale = Vector3.zero;
                }
                
            }
            else
            {
                int earthSurrounding = 0;
        
                List<Vector3Int> surroundingTiles = new List<Vector3Int>
                {
                    cellPos + Vector3Int.up,
                    cellPos + Vector3Int.up + Vector3Int.right,
                    cellPos + Vector3Int.up + Vector3Int.left,
                    cellPos + Vector3Int.down,
                    cellPos + Vector3Int.down + Vector3Int.left,
                    cellPos + Vector3Int.down + Vector3Int.right,
                    cellPos + Vector3Int.left,
                    cellPos + Vector3Int.right,
            
                };
        
                for (int i = 0; i < surroundingTiles.Count; i++)
                {
                    if (GridBuilding.current.MainTilemap.GetTile(surroundingTiles[i]) == GridBuilding.tileBases[TileType.Earth])
                    {
                        earthSurrounding++;
                    }
                }

                int nbrOfEarthInsuflation = 0;
                if (elementToPlace == 3) nbrOfEarthInsuflation++;
                float radius = (6 + (RM.earthEffectThree * nbrOfEarthInsuflation)) + (RM.earthEffectTwo * earthSurrounding);
                Vector3 radiusDisplayScale = new Vector3(radius * 2, radius * 2, 1);
                transform.GetChild(2).localScale = radiusDisplayScale;
            }
            
            
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
                AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
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
                AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
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
                AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
                UIM.DisplayAlert("cannot afford this");
                return false;
            }
        }
    }
}
