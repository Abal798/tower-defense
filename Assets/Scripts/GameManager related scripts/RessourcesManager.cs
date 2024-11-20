using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class RessourcesManager : MonoBehaviour
{
    public BarycentricDiagram BD;
    
    [Header("game stats")] 
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

    [Header("tower price")] 
    public float priceScaleFactor;
    public float basicFireTowerPrice = 100f;
    public float basicWaterTowerPrice = 100f;
    public float basicEarthTowerPrice = 100f;
    
    public float fireTowerPrice;
    public float waterTowerPrice;
    public float earthTowerPrice;
    
    public int nbrOfFireTower = 1;
    public int nbrOfWaterTower = 1;
    public int nbrOfEarthTower = 1;

    [Header("tiles effects")] 
    public float fireEffectOne = 10;
    public float waterEffectOne = 0.05f;
    public float earthEffectFour = 1.4f;
    public float earthEffectThree = 1;
    public float fireEffectTwo = 1.5f;
    public float waterEffectTwo = 1.0015f;
    public float earthEffectFive = 1.1f;

    private void Start()
    {
        BD.CalculatePosition(nbrOfFireTile, nbrOfWaterTile, nbrOfEarthTile);

        fireTowerPrice = basicFireTowerPrice;
        waterTowerPrice = basicWaterTowerPrice;
        earthTowerPrice = basicEarthTowerPrice;
        
        
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

        int weightA = nbrOfFireTile;
        int weightB = nbrOfWaterTile;
        int weightC = nbrOfEarthTower;
        
        int minValue = Mathf.Min(weightA, weightB, weightC);
        
        weightA -= minValue ;
        weightB -= minValue ;
        weightC -= minValue ;
        
        BD.CalculatePosition(nbrOfFireTile, nbrOfWaterTile, nbrOfEarthTile);
        
        fireEffectTwo *= 1 / (1 + Mathf.Exp((weightA - 35) / 3.6f));
        waterEffectTwo *= 1 / (1 + Mathf.Exp((weightB - 35) / 3.6f));
        earthEffectFive *= 1 / (1 + Mathf.Exp((weightC - 35) / 3.6f));

        foreach (var tower in GridBuilding.current.listeTowerCo)
        {
            if(tower.Value.gameObject != null)tower.Value.gameObject.GetComponent<ActualizeChild>().AcutalizeChild();
            
        }
        
    }
    
    public float GetTowerPrice(int towerElement, int numberOfTowerPlaced, int numberOfUpgrade)
    {
        float finalPrice = 0;
        if (towerElement == 1)
        {
            fireTowerPrice = basicFireTowerPrice + priceScaleFactor * numberOfTowerPlaced;
            Debug.Log("fTowerPrice" + fireTowerPrice + "number of tower placed" + numberOfTowerPlaced);
            finalPrice = fireTowerPrice;
            if (numberOfUpgrade > 0)
            {
                finalPrice += 10 * numberOfUpgrade;
            }
            return finalPrice;
        }
        if (towerElement == 2)
        {
            waterTowerPrice = basicWaterTowerPrice + priceScaleFactor * numberOfTowerPlaced;
            finalPrice = waterTowerPrice;
            if (numberOfUpgrade > 0)
            {
                finalPrice += 10 * numberOfUpgrade;
            }
            return finalPrice;
        }
        if (towerElement == 3)
        {
            earthTowerPrice = basicEarthTowerPrice + priceScaleFactor * numberOfTowerPlaced;
            finalPrice = earthTowerPrice;
            if (numberOfUpgrade > 0)
            {
                finalPrice += 10 * numberOfUpgrade;
            }
            return finalPrice;
        }

        return 0f;
    }


}
