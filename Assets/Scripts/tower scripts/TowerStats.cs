using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TowerStats : MonoBehaviour
{
    
    public float rotationSpeed;
    public List<int> ameliorations = new List<int>();
    public float bulletSpeed = 10f;
    
    public float basicCadence = 1f;
    public float cadence;
    
    public float basicDammage = 1f;
    public float damages;
    
    public float basicHealth = 1;
    public float health;

    public float basicRadius = 3f;
    public float radius;
    
    public int fireSurrounding;
    public int waterSurrouding;
    public int earthSurrounding;

    

    [Header("a renseigner par les GD")] 
    public float FireEffectOne = 10;
    public float FireEffectTwo = 1.1f;
    public float waterEffectOne = 5;
    public float waterEffectTwo = 1.0975f;
    public float waterEffectTree = 0.05f;
    public float waterEffectFour = 1.0015f;
    public float waterLifeBonus = 5;
    public float earthEffectThree = 1;
    public float earthEffectFour = 1.4f;
    public float earthEffectFive = 1.1f;
    
    public int priceOne = 30;
    public int priceTwo = 40;
    public int priceThree = 50;

    public UnityEvent statsHasBeenRecalculated;
    


    void Start()
    {
        CalculateSurroundings();
        recalculateStats();
        Building.UpdatePathfinding.AddListener(CalculateSurroundings);
    }
    
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void recalculateStats()
    {
        int nbrOfFireInsuflation = 0;
        int nbrOfWaterInsuflation = 0;
        int nbrOfEarthInsuflation = 0;
        

        for (int i = 0; i < ameliorations.Count; i++)
        {
            if (ameliorations[i] == 1)
            {
                nbrOfFireInsuflation++;
            }
            else if (ameliorations[i] == 2)
            {
                nbrOfWaterInsuflation++;
            }
            else if (ameliorations[i] == 3)
            {
                nbrOfEarthInsuflation++;
            }
        }

        damages = (basicDammage + (FireEffectOne * nbrOfFireInsuflation + waterEffectOne * nbrOfWaterInsuflation)) * (Mathf.Pow(FireEffectTwo, fireSurrounding) * Mathf.Pow(waterEffectTwo, waterSurrouding)); 
        cadence = basicCadence - (waterEffectTree * nbrOfWaterInsuflation)*(Mathf.Pow(waterEffectFour, waterSurrouding));
        radius = basicRadius + (earthEffectThree * nbrOfEarthInsuflation);
        health = basicHealth + ((waterLifeBonus * nbrOfWaterInsuflation) * Mathf.Pow(earthEffectFour, nbrOfEarthInsuflation)) * Mathf.Pow(earthEffectFive, earthSurrounding);

        statsHasBeenRecalculated.Invoke();
        

    }

    private void OnDestroy()
    {
        Building.UpdatePathfinding.RemoveListener(CalculateSurroundings);
    }

    public void CalculateSurroundings()
    {
        Vector3Int cellPos = GridBuilding.current.gridLayout.WorldToCell(transform.position);
        
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
            if (GridBuilding.current.MainTilemap.GetTile(surroundingTiles[i]) == GridBuilding.tileBases[TileType.Fire])
            {
                fireSurrounding++;
            }
            else if (GridBuilding.current.MainTilemap.GetTile(surroundingTiles[i]) == GridBuilding.tileBases[TileType.Water])
            {
                waterSurrouding++;
            }
            else if (GridBuilding.current.MainTilemap.GetTile(surroundingTiles[i]) == GridBuilding.tileBases[TileType.Earth])
            {
                earthSurrounding++;
            }
        }
        
        recalculateStats();
    }
    
}
