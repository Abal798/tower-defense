using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuilding : MonoBehaviour
{
    public static GridBuilding current;
    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    public UIManager UIM;
    public RessourcesManager RM;
    
    public static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();
    public Dictionary<Vector3Int, GameObject> listeTowerCo = new Dictionary<Vector3Int, GameObject>();

    private Building temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    
    
    
    #region Unity Methods

    private void Awake()
    {
        RM = FindObjectOfType<RessourcesManager>();
        current = this;
    }

    private void Start()
    {
        if(tileBases.Count != 0) return;
        string titlePath = "Tiles/";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(titlePath + "white"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(titlePath + "green"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(titlePath + "red"));
        tileBases.Add(TileType.Grey, Resources.Load<TileBase>(titlePath + "grey"));
        tileBases.Add(TileType.Water, Resources.Load<TileBase>(titlePath + "001"));
        tileBases.Add(TileType.Grass, Resources.Load<TileBase>(titlePath + "003"));
        tileBases.Add(TileType.Earth, Resources.Load<TileBase>(titlePath + "005"));
        tileBases.Add(TileType.Moutain, Resources.Load<TileBase>(titlePath + "004"));
        tileBases.Add(TileType.Fire, Resources.Load<TileBase>(titlePath + "008"));
        tileBases.Add(TileType.Volcano, Resources.Load<TileBase>(titlePath + "009"));
        tileBases.Add(TileType.Base, Resources.Load<TileBase>(titlePath + "BaseTile"));
        
        
        
        Debug.Log("Water Tile: " + (tileBases[TileType.Water] == null ? "null" : "Loaded"));
        Debug.Log("Grass Tile: " + (tileBases[TileType.Grass] == null ? "Failed to Load" : "Loaded"));
        Debug.Log("Earth Tile: " + (tileBases[TileType.Earth] == null ? "Failed to Load" : "Loaded"));
        Debug.Log("Forest Tile: " + (tileBases[TileType.Moutain] == null ? "Failed to Load" : "Loaded"));
        Debug.Log("Fire Tile: " + (tileBases[TileType.Fire] == null ? "Failed to Load" : "Loaded"));
        Debug.Log("Volcano Tile: " + (tileBases[TileType.Volcano] == null ? "Failed to Load" : "Loaded"));
    }

    private void Update()
    {
        if (!temp)
        {
            return;
        }
        
        if (!temp.Placed)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);
            FollowBuilding();

            if (prevPos != cellPos)
            {
                //temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(.5f,
                //.5f, 0f));
                prevPos = cellPos;
                FollowBuilding();
            }
            
            
            if (Input.GetMouseButtonDown(0))
            {
                if (temp.CanBePlaced())
                {
                    int price = 0;
                    if (elementTour == 1)
                    {
                        price = Mathf.FloorToInt(RM.GetTowerPrice(elementTour, RM.nbrOfFireTower));
                        Debug.Log("price" + price);
                    }
                    else if (elementTour == 2)
                    {
                        price = Mathf.FloorToInt(RM.GetTowerPrice(elementTour, RM.nbrOfWaterTower));
                    }
                    else if (elementTour == 3)
                    {
                        price = Mathf.FloorToInt(RM.GetTowerPrice(elementTour, RM.nbrOfEarthTower));
                    }
                    if (temp.Afford(elementTour, price))// ajouter ici le morceau de script qui permet de recuperer le prix de placement d'une tour
                    {
                        EndGameStats.EGS.nombreDinsuflationsTotal++;
                        temp.Place(elementTour);
                    }
                        
                    
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                ClearArea();
                Destroy(temp.gameObject);
            
            }
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }
            
            
           
        }/*
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (temp.CanBePlaced())
            {
                temp.Place();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearArea();
            Destroy(temp.gameObject);
            
        }*/
    }

    #endregion

    #region Tilemap Management
    private static Vector3Int[] GetTilePositions(BoundsInt area)
    {
        Vector3Int[] positions = new Vector3Int[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            positions[counter] = new Vector3Int(v.x, v.y, 0);
            counter++;
        }

        return positions;
    }

    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }


    private static void FillTiles(TileBase[] arr, TileType type)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBases[type];
        }
    }
    #endregion

    #region Building Placement

    private int elementTour;

    public void PreInitializeFeu(GameObject building)
    {
        AudioManager.AM.PlaySfx(AudioManager.AM.buttonClick);
        elementTour = 1;
        InitializeWithBuilding(building);
    }
    
    public void PreInitializeEau(GameObject building)
    {
        AudioManager.AM.PlaySfx(AudioManager.AM.buttonClick);
        elementTour = 2;
        InitializeWithBuilding(building);
    }
    
    public void PreInitializeTerre(GameObject building)
    {
        AudioManager.AM.PlaySfx(AudioManager.AM.buttonClick);
        elementTour = 3;
        InitializeWithBuilding(building);
    }

    public void InitializeWithBuilding(GameObject building)
    {
        if (temp != null)
        {
            if (!temp.Placed)
            {
                return;
            }
            
        }
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = gridLayout.LocalToCell(touchPos);
        temp = Instantiate(building, cellPos, Quaternion.identity).GetComponent<Building>();
        Debug.Log(temp);
        FollowBuilding();
    }
    
    

    private void ClearArea()
    {
        TileBase[] toClear = new TileBase[prevArea.size.x * prevArea.size.y * prevArea.size.z];
        FillTiles(toClear, TileType.Empty);
        TempTilemap.SetTilesBlock(prevArea, toClear);
    }

    private void FollowBuilding()
    {
        // Clear the previous area first
        ClearArea();
        
        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);
        BoundsInt buildingArea = temp.area;

        
        Vector3Int[] posArray = GetTilePositions(buildingArea);

        
        TileBase[] tileArray = new TileBase[posArray.Length];

        

        
        for (int i = 0; i < posArray.Length; i++)
        {
            
            if (TilemapManager.TilemapInstance.GetTileData(posArray[i]).buildable)
            {
               
                tileArray[i] = tileBases[TileType.Green];
                temp.transform.GetChild(0).gameObject.SetActive(false);
            }
            else if (TilemapManager.TilemapInstance.GetTileData(posArray[i]).alreadyBuilt)
            {
                temp.transform.GetChild(0).gameObject.SetActive(false);
                
                Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(mouseWorldPosition, 0.0001f);

                foreach (var collider in colliders)
                {
                    TowerStats towerStats = collider.GetComponent<TowerStats>();
                    if (towerStats != null && towerStats.ameliorations.Count < 3)
                    {
                        temp.transform.GetChild(0).gameObject.SetActive(true);
                        break;
                    }

            
                }
                
            }
            else
            {
                tileArray[i] = tileBases[TileType.Red];
                temp.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        
        TempTilemap.SetTilesBlock(buildingArea, tileArray);
        
        prevArea = buildingArea;
    }


    public bool CanTakeArea(BoundsInt area)
    {
        
        Vector3Int[] baseArray = GetTilePositions(area);
        foreach (var b in baseArray)
        {
            if (!TilemapManager.TilemapInstance.GetTileData(b).buildable)
            {
                if (TilemapManager.TilemapInstance.GetTileData(b).alreadyBuilt)
                {
                    temp.Upgrade(elementTour);
                    return false;
                }
                UIM.DisplayAlert("le batiment ne peut pas etre placé ici");
                return false;
            }
        }

        return true;
    }
    
    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, TempTilemap);
        SetTilesBlock(area, TileType.Grey, MainTilemap);
    }

    public bool tempEmpty()
    {
        return !temp;
    }
    #endregion
}

public enum TileType
{
    Empty,
    White,
    Green, 
    Red,
    Grey,
    Water,
    Grass,
    Earth,
    Moutain,
    Fire,
    Volcano,
    Base
    
}
