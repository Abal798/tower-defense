using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public RessourcesManager RM;
    
    public GameObject ingamePanel;
    public GameObject alchimiePanel;

    public TextMeshProUGUI waveDisplay;
    public TextMeshProUGUI fireSoulIngameDisplay;
    public TextMeshProUGUI waterSoulIngameDisplay;
    public TextMeshProUGUI earthSoulIngameDisplay;
    
    public TextMeshProUGUI fireTowerPriceDisplay;
    public TextMeshProUGUI waterTowerPriceDisplay;
    public TextMeshProUGUI earthTowerPriceDisplay;
    void Start()
    {
        ingamePanel.SetActive(true);
        alchimiePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float fireTowerPriceDisplayValue = RM.fireTowerPrice + 10;
        float waterTowerPriceDisplayValue = RM.waterTowerPrice + 10;
        float earthTowerPriceDisplayValue = RM.earthTowerPrice + 10;
        
        fireSoulIngameDisplay.text = "" + RM.fireSoul;
        waterSoulIngameDisplay.text = "" + RM.waterSoul;
        earthSoulIngameDisplay.text = "" + RM.plantSoul;

        fireTowerPriceDisplay.text = "" + fireTowerPriceDisplayValue;
        waterTowerPriceDisplay.text = "" + waterTowerPriceDisplayValue;
        earthTowerPriceDisplay.text = "" + earthTowerPriceDisplayValue;
        
        
        waveDisplay.text = "wave : " + RM.wave;
        
        
    }

    public void GoToAlchimiePanel()
    {
        ingamePanel.SetActive(false);
        alchimiePanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
