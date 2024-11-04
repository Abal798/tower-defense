using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TowerStats : MonoBehaviour
{
    public float radius;
    public float rotationSpeed;
    public List<int> ameliorations = new List<int>();
    public float bulletSpeed = 10f;
    
    public float basicCadence = 1f;
    public float cadence;
    
    public float basicDammage = 1f;
    public float damages;
    
    public float basicHealth = 1;
    public float health;
    
    public int fireSurrounding;
    public int waterSurrouding;
    public int earthSurrounding;
    

    [Header("a renseigner par les GD")] 
    public float FireEffect = 10;
    public float waterEffectOne = 5;
    public float waterEffectTwo = 0.05f;
    public float waterLifeBonus = 5;

    public UnityEvent statsHasBeenRecalculated;
    


    void Start()
    {
      
        recalculateStats();
        CalculateSurroundings();
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

        Debug.Log(ameliorations.Count);

        for (int i = 0; i < ameliorations.Count; i++)
        {
            Debug.Log("amelioration[" + i + "] = " + ameliorations[i]);

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

        damages = basicDammage + FireEffect * nbrOfFireInsuflation +
                  waterEffectOne * nbrOfWaterInsuflation; //formule incomplete , manque l'influence du terrain
        health = basicHealth +
                 waterLifeBonus *
                 nbrOfWaterInsuflation; //incomplet , manque l'effet de terre , trop impréci pour l'instant
        cadence = basicCadence - waterEffectTwo * nbrOfWaterInsuflation;

        statsHasBeenRecalculated.Invoke();

        Debug.Log("fire :" + nbrOfFireInsuflation);
        Debug.Log("water :" + nbrOfWaterInsuflation);
        Debug.Log("earth :" + nbrOfEarthInsuflation);

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
            if (GridBuilding.current.MainTilemap.GetTile(surroundingTiles[i]) == GridBuilding.tileBases[TileType.Red])
            {
                fireSurrounding++;
            }
            else if (GridBuilding.current.MainTilemap.GetTile(surroundingTiles[i]) == GridBuilding.tileBases[TileType.Grey])
            {
                waterSurrouding++;
            }
            else if (GridBuilding.current.MainTilemap.GetTile(surroundingTiles[i]) == GridBuilding.tileBases[TileType.Green])
            {
                earthSurrounding++;
            }
        }
        
    }
    
}
