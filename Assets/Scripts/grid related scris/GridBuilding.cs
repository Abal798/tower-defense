using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuilding : MonoBehaviour
{
    public static GridBuilding current;
    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private Building temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;
    
    #region Unity Methods

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        string titlePath = "Tiles/";
        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(titlePath + "white"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(titlePath + "green"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(titlePath + "red"));
        tileBases.Add(TileType.Grey, Resources.Load<TileBase>(titlePath + "grey"));
        
        Debug.Log("Empty Tile: " + (tileBases[TileType.Empty] == null ? "null" : "Loaded"));
        Debug.Log("Green Tile: " + (tileBases[TileType.Green] == null ? "Failed to Load" : "Loaded"));
        Debug.Log("Red Tile: " + (tileBases[TileType.Red] == null ? "Failed to Load" : "Loaded"));
        Debug.Log("White Tile: " + (tileBases[TileType.White] == null ? "Failed to Load" : "Loaded"));
        Debug.Log("Grey Tile: " + (tileBases[TileType.Grey] == null ? "Failed to Load" : "Loaded"));
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
                    temp.Place();
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

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
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
        elementTour = 1;
        InitializeWithBuilding(building);
    }
    
    public void PreInitializeEau(GameObject building)
    {
        elementTour = 2;
        InitializeWithBuilding(building);
    }
    
    public void PreInitializeTerre(GameObject building)
    {
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
        temp = Instantiate(building, Vector3.zero, Quaternion.identity).GetComponent<Building>();
        temp.GetComponentInChildren<TowerStats>().towerType = elementTour;
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

        
        TileBase[] baseArray = GetTilesBlock(buildingArea, MainTilemap);

        
        TileBase[] tileArray = new TileBase[baseArray.Length];

        

        
        for (int i = 0; i < baseArray.Length; i++)
        {
            
            if (baseArray[i] == tileBases[TileType.White])
            {
               
                tileArray[i] = tileBases[TileType.Green];
            }
            else
            {
                
                tileArray[i] = tileBases[TileType.Red];
                
            }
        }

        
        TempTilemap.SetTilesBlock(buildingArea, tileArray);
        
        prevArea = buildingArea;
    }


    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, MainTilemap);
        foreach (var b in baseArray)
        {
            if (b != tileBases[TileType.White])
            {
                Debug.Log("Can't place here");
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

    #endregion
}

public enum TileType
{
    Empty,
    White,
    Green, 
    Red,
    Grey
}
