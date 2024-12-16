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
    public float dureeDeLaPartie = 0f;
    public int nombreDeVague;
    public int nombreDameRestants;
    public int nombreDameTotal;
    public int nombreDeSortsPlaces;
    public int nombreDinsuflationsTotal;
    
    public TextMeshProUGUI numberOfFireTilesDisplay;
    public TextMeshProUGUI numberOfWaterTilesDisplay;
    public TextMeshProUGUI numberOfEarthTilesDisplay;
    public TextMeshProUGUI numberOfTowerDisplay;
    public TextMeshProUGUI dureeDeLaPartieDisplay;
    public TextMeshProUGUI nombreDeVagueDisplay;
    public TextMeshProUGUI nombreDameRestantsDisplay;
    public TextMeshProUGUI nombreDameTotalDisplay;
    public TextMeshProUGUI nombreDeSortsPlacesDisplay;
    public TextMeshProUGUI nombreDinsuflationsTotalDisplay;

    private void Awake()
    {
        EGS = this;
    }

    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            dureeDeLaPartie += Time.deltaTime;
        }
    }

    public void displayGameStats()
    {
        numberOfTower = GridBuilding.current.listeTowerCo.Count;
        
        numberOfFireTilesDisplay.text = "" + numberOfFireTiles;
        numberOfWaterTilesDisplay.text = "" + numberOfWaterTiles;
        numberOfEarthTilesDisplay.text = "" + numberOfEarthTiles;
        numberOfTowerDisplay.text = "" + numberOfTower;
        dureeDeLaPartieDisplay.text = "" + dureeDeLaPartie;
        nombreDeVagueDisplay.text = "" + nombreDeVague;
        nombreDameRestantsDisplay.text = "" + nombreDameRestants;
        nombreDameTotalDisplay.text = "" + nombreDameTotal;
        nombreDeSortsPlacesDisplay.text = "" + nombreDeSortsPlaces;
        nombreDinsuflationsTotalDisplay.text = "" + nombreDinsuflationsTotal;
    }
}
