using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager TilemapInstance;
    
    
    [SerializeField]
    public Tilemap tilemap;
    public Tilemap overlayTilemap;

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


    public TileData GetTileData(Vector3Int tilePosition)
    {
        TileBase tile = tilemap.GetTile(tilePosition);

        if (tile == null)
        {
            return null;
        }
 
        else
        {
            return dataFromTiles[tile];
        }
            
        


    }


  


}