using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RessourcesManager : MonoBehaviour
{
    public BarycentricDiagram BD;
    
    [Header("game stats")] 
    public float chrono;
    public float wave;
    public int nbrOfFireTile;
    public int nbrOfWaterTile;
    public int nbrOfEarthTile;
        
    [Header("player stats")] 
    public float health;
    
    [Header("spells")]
    public List<int> spellSlotOne= new List<int>();
    public List<int> spellSlotTwo = new List<int>();
    public List<int> spellSlotThree= new List<int>();

    public int basicFireDosePrice;
    public int basicWaterDosePrice;
    public int basicEarthDosePrice;

    public float spellAugmentationPriceFactor;
    
    
    
    [Header("souls ressources")] 
    public float fireSoul;
    public float waterSoul;
    public float plantSoul;

    [Header("main tilemap")] 
    public Tilemap mainTilemap;

    private void Start()
    {
        BD.CalculatePosition(nbrOfFireTile, nbrOfWaterTile, nbrOfEarthTile);

        for (int i = -20; i < 20; i = i+1)
        {
            for (int j = -20; j < 20; j++)
            {
                UpdateTileNumber(new Vector3Int(i,j,0), 1, true);
            }
        }
    }


    public void UpdateTileNumber(Vector3Int tilePos, int nbrOfTile, bool operation)
    {
        if(mainTilemap.GetTile(tilePos) == GridBuilding.tileBases[TileType.Fire])
        {
            if (operation)
            {
                nbrOfFireTile += nbrOfTile;
            }
            else
            {
                nbrOfFireTile -= nbrOfTile;
            }
        }
        if(mainTilemap.GetTile(tilePos) == GridBuilding.tileBases[TileType.Water])
        {
            if (operation)
            {
                nbrOfWaterTile += nbrOfTile;
            }
            else
            {
                nbrOfWaterTile -= nbrOfTile;
            }
        }
        if(mainTilemap.GetTile(tilePos) == GridBuilding.tileBases[TileType.Earth])
        {
            if (operation)
            {
                nbrOfEarthTile += nbrOfTile;
            }
            else
            {
                nbrOfEarthTile -= nbrOfTile;
            }
        }
        
        BD.CalculatePosition(nbrOfFireTile, nbrOfWaterTile, nbrOfEarthTile);
        
    }
}
