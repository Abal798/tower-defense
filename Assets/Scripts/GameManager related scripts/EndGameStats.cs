using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameStats : MonoBehaviour
{
    public static EndGameStats EGS;
    
    public int numberOfFireTiles;
    public int numberOfWaterTiles;
    public int numberOfEarthTiles;
    public int numberOfTower;
    public int nombreDeVague;
    public float nombreDameRestants;
    public int nombreDeSortsPlaces;
    public int nombreDinsuflationsTotal;
    
    public TextMeshProUGUI numberOfFireTilesDisplay;
    public TextMeshProUGUI numberOfWaterTilesDisplay;
    public TextMeshProUGUI numberOfEarthTilesDisplay;
    public TextMeshProUGUI numberOfTowerDisplay;
    public TextMeshProUGUI nombreDeVagueDisplay;
    public TextMeshProUGUI nombreDameRestantsDisplay;
    public TextMeshProUGUI nombreDeSortsPlacesDisplay;
    public TextMeshProUGUI nombreDinsuflationsTotalDisplay;

    private void Awake()
    {
        EGS = this;
        Debug.Log("awaken");
    }
    

    public void DisplayGameStats()
    {
        numberOfTower = GridBuilding.current.listeTowerCo.Count;
        
        numberOfFireTilesDisplay.text = "" + numberOfFireTiles;
        numberOfWaterTilesDisplay.text = "" + numberOfWaterTiles;
        numberOfEarthTilesDisplay.text = "" + numberOfEarthTiles;
        numberOfTowerDisplay.text = "" + numberOfTower;
        nombreDeVagueDisplay.text = "" + nombreDeVague;
        nombreDameRestantsDisplay.text = "" + nombreDameRestants;
        nombreDeSortsPlacesDisplay.text = "" + nombreDeSortsPlaces;
        nombreDinsuflationsTotalDisplay.text = "" + nombreDinsuflationsTotal;
    }
}
