using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    
    private Dictionary<Vector3Int, int> tileTheoricalDistanceToEnd = new Dictionary<Vector3Int, int>();

    [SerializeField]private List<TilesToVisitClass> tilesToVisit;
    
    [SerializeField]private List<TilesToVisitClass> debugList;
    
    //first vector is current cell position || second vector is last cell position (the one you need to go to return to start)
    private Dictionary<Vector3Int, Vector3Int> visitedTiles = new Dictionary<Vector3Int, Vector3Int>();

    public List<Vector3Int> resultTempo = new List<Vector3Int>();
    public List<Vector3Int> Final = new List<Vector3Int>();
    
    [Header("Debugging variables")]
    public GameObject txtDebugging;

    /*
    private void Start()
    {
        Final  = PathfindingCalculation(new Vector3Int(0, 0, 0), new Vector3Int(4, -3, 0), true);
        Debuging(true,new Vector3Int(0, 0, 0), new Vector3Int(4, -3, 0));
        Debug.Log(resultTempo = PathfindingCalculation(new Vector3Int(0, 0, 0), new Vector3Int(4, -3, 0), false));
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("DebugText");
            foreach(GameObject go in gos)
                Destroy(go);
            Debuging(true,new Vector3Int(0, 0, 0), new Vector3Int(4, -3, 0));
           
        }
    }
    */
    public void Debuging(bool started,Vector3Int startPos, Vector3Int endPos)
    {
        if (started)
        {
            List<Vector3Int> pathfindingResult = PathfindingCalculation(startPos, endPos, true);
            
            foreach (Vector3Int cellPosition in TilemapManager.TilemapInstance.tilemap.cellBounds.allPositionsWithin)
            {
                if (TilemapManager.TilemapInstance.tilemap.HasTile(cellPosition))
                {
                    string text = "";
                    foreach (TilesToVisitClass tiles in debugList)
                    {
                        if (tiles.currentTile == cellPosition)
                        {
                            text = "\nP :" + tiles.priority;
                        }
                    }
                    GameObject newText = Instantiate(txtDebugging, TilemapManager.TilemapInstance.tilemap.GetCellCenterWorld(cellPosition), quaternion.Euler(0,0,0), transform);
                    newText.GetComponent<TextMeshPro>().text = "(" + cellPosition.x + ";" + cellPosition.y + ")\nD : " + tileTheoricalDistanceToEnd[cellPosition] + text;
                }
            }
            for(int i = 0; i < pathfindingResult.Count; i++)
            {
                if (i == 0 || i == pathfindingResult.Count - 1)
                {
                    TilemapManager.TilemapInstance.overlayTilemap.SetTile(pathfindingResult[i],
                        TilemapManager.TilemapInstance.overlayTileStartend);
                    
                }
                else
                {
                    TilemapManager.TilemapInstance.overlayTilemap.SetTile(pathfindingResult[i],
                        TilemapManager.TilemapInstance.overlayTilePath);
                    
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
    
    public void TileDistanceToLastTile(Vector3Int endTilePos)
    {
        foreach (Vector3Int cellPosition in TilemapManager.TilemapInstance.tilemap.cellBounds.allPositionsWithin)
        {
            if (TilemapManager.TilemapInstance.tilemap.HasTile(cellPosition))
            {
                int manhattanDistanceToA = Mathf.Abs(endTilePos.x - cellPosition.x) + Mathf.Abs(endTilePos.y - cellPosition.y);

                if (!tileTheoricalDistanceToEnd.ContainsKey(cellPosition))
                {
                    tileTheoricalDistanceToEnd.Add(cellPosition, manhattanDistanceToA);
                }
            }
        }
    }

    void AddNeighborToVisit(Vector3Int currentCell, bool doColor)
    {
        InsertTileInToVisit(currentCell + new Vector3Int(0, 1, 0), currentCell, tilesToVisit[0].priority, doColor);
        InsertTileInToVisit(currentCell + new Vector3Int(1, 0, 0), currentCell, tilesToVisit[0].priority, doColor);
        InsertTileInToVisit(currentCell + new Vector3Int(0, -1, 0), currentCell, tilesToVisit[0].priority, doColor);
        InsertTileInToVisit(currentCell + new Vector3Int(-1, 0, 0), currentCell, tilesToVisit[0].priority, doColor);
        InsertTileInToVisit(currentCell + new Vector3Int(-1, 1, 0), currentCell, tilesToVisit[0].priority, doColor);
        InsertTileInToVisit(currentCell + new Vector3Int(1, 1, 0), currentCell, tilesToVisit[0].priority, doColor);
        InsertTileInToVisit(currentCell + new Vector3Int(-1, -1, 0), currentCell, tilesToVisit[0].priority, doColor);
        InsertTileInToVisit(currentCell + new Vector3Int(1, -1, 0), currentCell, tilesToVisit[0].priority, doColor);
    }

    bool TileInToVisitList(Vector3Int cellToCheck)
    {
        foreach (TilesToVisitClass tileToVisit in tilesToVisit)
        {
            if (tileToVisit.currentTile == cellToCheck)
            {
                return true;
            }
        }
        return false;
    }
    void RemoveTileFromToVisit(Vector3Int cellToCheck)
    {
        foreach (TilesToVisitClass tileToVisit in tilesToVisit)
        {
            if (tileToVisit.currentTile == cellToCheck)
            {
                    tilesToVisit.Remove(tileToVisit);
                    break;
            }
        }
    }

    void InsertTileInToVisit(Vector3Int cellToInsert, Vector3Int parentCell, int parentWalkingCost, bool doColor)
    {
        if(TilemapManager.TilemapInstance.tilemap.HasTile(cellToInsert) && !(visitedTiles.ContainsKey(cellToInsert)) && !TileInToVisitList(cellToInsert))
        {
            TilesToVisitClass insertValue = new TilesToVisitClass
            {
                currentTile = cellToInsert,
                lastTile = parentCell,
                priority =  tileTheoricalDistanceToEnd[cellToInsert] + TilemapManager.TilemapInstance.GetTileData(cellToInsert).walkingCost + parentWalkingCost
            };

            if (doColor)
            {
                TilemapManager.TilemapInstance.overlayTilemap.SetTile(cellToInsert,
                    TilemapManager.TilemapInstance.overlayTileToVisit);
            }

            if (tilesToVisit.Count == 0)
            {
                tilesToVisit.Add(insertValue);
                debugList.Add(insertValue);
                //Debug.Log("00");
            }
            else
            {
                bool addToEnd = true;
                for (int i = 0; i < tilesToVisit.Count; i++)
                {
                    if (insertValue.priority < tilesToVisit[i].priority)
                    {
                        tilesToVisit.Insert(i, insertValue);
                        
                        debugList.Add(insertValue);
                        //Debug.Log("22");
                        addToEnd = false;
                        break;
                    }
                }

                if (addToEnd)
                {
                    tilesToVisit.Add(insertValue);
                    debugList.Add(insertValue);
                    //Debug.Log("22");
                }
            }
        }
    }

    void AddTileToVisited(Vector3Int tileToAdd, Vector3Int tileToAddPrevious, bool doColor)
    {
        visitedTiles.Add(tileToAdd, tileToAddPrevious);
        if (doColor)
        {
            TilemapManager.TilemapInstance.overlayTilemap.SetTile(tileToAdd,
                TilemapManager.TilemapInstance.overlayTileVisited);
        }
    }

    public List<Vector3Int> PathfindingCalculation(Vector3Int startPos, Vector3Int endPos, bool doColor)
    {
        //reset list and dictionnary
        tilesToVisit.Clear();
        visitedTiles.Clear();
        tileTheoricalDistanceToEnd.Clear();
        
        TileDistanceToLastTile(endPos);
        
        
        //value is -10 because it's the first Tile
        InsertTileInToVisit(startPos, new Vector3Int(-10,-10,-10), 0, doColor);
        while ((tilesToVisit.Count > 0) && (tilesToVisit[0].currentTile != endPos))
        {
            AddNeighborToVisit(tilesToVisit[0].currentTile, doColor);
            AddTileToVisited(tilesToVisit[0].currentTile, tilesToVisit[0].lastTile, doColor);
            RemoveTileFromToVisit(tilesToVisit[0].currentTile);
        }
        
        if (tilesToVisit.Count > 0 && tilesToVisit[0].currentTile == endPos)
        {
            AddTileToVisited(endPos, tilesToVisit[0].lastTile, doColor);
        }

        Vector3Int previousTile = endPos;
        List<Vector3Int> returnList = new List<Vector3Int>();
        
        while (previousTile != new Vector3Int(-10, -10, -10))
        {
            returnList.Insert(0, previousTile);
            previousTile = visitedTiles[previousTile];
        }

        return returnList;
    }
    
    
}

[Serializable]
public class TilesToVisitClass
{
    public Vector3Int currentTile;
    public Vector3Int lastTile;
    public int realWalkingCost;
    public int priority;
}
