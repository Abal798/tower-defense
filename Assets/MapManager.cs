using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager TilemapInstance;
    
    
    [SerializeField]
    public Tilemap tilemap;
    public TileBase overlayTilemap;

    public Tile overlayTileStartend;
    public Tile overlayTilePath;
    public Tile overlayTileToVisit;
    public Tile overlayTileVisited;

    [SerializeField]
    private List<TileData> tileDatas;



    private Dictionary<TileBase, TileData> dataFromTiles;





    private void Awake()
    {
        
        TilemapInstance = this;
        
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
       

    }






    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = tilemap.WorldToCell(mousePosition);

            TileBase clickedTile = tilemap.GetTile(gridPosition);

            tilemap.SetTileFlags(gridPosition, TileFlags.None);
            tilemap.SetColor(gridPosition, Color.black);

            bool buildable = dataFromTiles[clickedTile].buildable;
            print(buildable);






        }
    }



    public float GetTileWalkingCost(Vector2 worldPosition)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile == null)
            return 1f;

        float walkingCost = dataFromTiles[tile].walkingCost;

        return  walkingCost;


    }


    public TileData GetTileData(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);

        if (tile == null)
            return null;
        else
            return dataFromTiles[tile];


    }


  


}