using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TowerStats : MonoBehaviour
{
    private RessourcesManager RM;
    
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
    private float fireEffectOne = 10;
    private float waterEffectOne = 0.05f;
    private float earthEffectFour = 1.4f;
    private float earthEffectThree = 1;
    
    
    
    
    private float fireEffectTwo = 1.1f;
    private float waterEffectTwo = 1.0015f;
    private float earthEffectFive = 1.1f;
    
    
    public UnityEvent statsHasBeenRecalculated;

    [Header("a renseigner par les prog")] 
    public GameObject upgradeParticules;

    public GameObject healthBar;
    
    


    void Start()
    {
        RM = FindObjectOfType<RessourcesManager>();
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
        
        fireEffectOne = RM.fireEffectOne;
        waterEffectOne = RM.waterEffectOne;
        earthEffectFour = RM.earthEffectFour;
        earthEffectThree = RM.earthEffectThree;
        fireEffectTwo = RM.fireEffectTwo;
        waterEffectTwo = RM.waterEffectTwo;
        earthEffectFive = RM.earthEffectFive;
        
        damages = (basicDammage + (fireEffectOne * nbrOfFireInsuflation)) * Mathf.Pow(fireEffectTwo, fireSurrounding);
        if (damages < basicDammage + (fireEffectOne * nbrOfFireInsuflation)) damages = basicDammage + (fireEffectOne * nbrOfFireInsuflation);
        cadence = (basicCadence - (waterEffectOne * nbrOfWaterInsuflation))*(Mathf.Pow(waterEffectTwo, waterSurrouding));
        if (cadence > basicCadence - (waterEffectOne * nbrOfWaterInsuflation)) cadence = (basicCadence - (waterEffectOne * nbrOfWaterInsuflation));
        radius = basicRadius + (earthEffectThree * nbrOfEarthInsuflation);
        maxHealth = (basicHealth + ((Mathf.Pow(earthEffectFour, nbrOfEarthInsuflation)))) * (Mathf.Pow(earthEffectFive, earthSurrounding))-1;
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

    public void Heal()
    {
        health = maxHealth;
        healthBar.SetActive(false);
    }

    
    
}
