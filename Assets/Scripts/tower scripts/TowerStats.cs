using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TowerStats : MonoBehaviour
{
    [Header("stats")]
    
    public float rotationSpeed;
    public List<int> ameliorations = new List<int>();
    public float bulletSpeed = 10f;
    
    public float basicCadence = 1f;
    public float cadence;
    
    public float basicDammage = 1f;
    public float damages;
    
    public float basicHealth = 10;
    public float maxHealth;
    private float previousMaxHealth;
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

    [Header("a renseigner par les prog")] 
    public GameObject upgradeParticules;

    public GameObject healthBar;
    
    


    void Start()
    {
        maxHealth = basicHealth;
        previousMaxHealth = basicHealth;
        health = maxHealth;
        CalculateSurroundings();
        recalculateStats();
        Building.UpdatePathfinding.AddListener(CalculateSurroundings); //attention a cette ligne , elle fait recalculate stats a chaque fois qu'une nouvelle toure est posée, c'est chiant pour les visuels
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
        maxHealth = basicHealth + ((waterLifeBonus * nbrOfWaterInsuflation) * Mathf.Pow(earthEffectFour, nbrOfEarthInsuflation)) * Mathf.Pow(earthEffectFive, earthSurrounding);
        health += maxHealth - previousMaxHealth;
        previousMaxHealth = maxHealth;
        
        statsHasBeenRecalculated.Invoke();


        if (maxHealth != health)
        {
            healthBar.SetActive(true);
        }
        else
        {
            healthBar.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        Building.UpdatePathfinding.RemoveListener(CalculateSurroundings);
    }

    public void CalculateSurroundings()
    {
        fireSurrounding = 0;
        waterSurrouding = 0;
        earthSurrounding = 0;
        
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

    public void LaunchUpgradeEffects()
    {
        GameObject newParticules = Instantiate(upgradeParticules, transform.position, new Quaternion(-0.707106829f,0,0,0.707106829f));
        Destroy(newParticules, 0.5f);
        gameObject.transform.GetChild(0).GetComponent<TowerSpriteAppearence>().UpdateSize(0.5f, 1.2f, transform);
    }
    
}
