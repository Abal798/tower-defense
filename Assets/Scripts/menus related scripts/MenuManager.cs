using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public RessourcesManager RM;
    
    public GameObject ingamePanel;
    public GameObject alchimiePanel;
    public GameObject IconFeu;
    public GameObject IconEau;
    public GameObject IconTerre;

    public TextMeshProUGUI waveDisplay;
    public TextMeshProUGUI fireSoulIngameDisplay;
    public TextMeshProUGUI waterSoulIngameDisplay;
    public TextMeshProUGUI earthSoulIngameDisplay;
    
    public TextMeshProUGUI fireTowerPriceDisplay;
    public TextMeshProUGUI waterTowerPriceDisplay;
    public TextMeshProUGUI earthTowerPriceDisplay;

    public KeyCode shortcutAlchimiePanel;
    public KeyCode shortcutFireTower;
    public KeyCode shortcutWaterTower;
    public KeyCode shortcutEarthTower;
    public KeyCode shortcutLaunchWave;
    
    void Start()
    {
        ingamePanel.SetActive(true);
        alchimiePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        fireSoulIngameDisplay.text = "" + RM.fireSoul;
        waterSoulIngameDisplay.text = "" + RM.waterSoul;
        earthSoulIngameDisplay.text = "" + RM.plantSoul;

        fireTowerPriceDisplay.text = "" + RM.fireTowerPrice;
        waterTowerPriceDisplay.text = "" + RM.waterTowerPrice;
        earthTowerPriceDisplay.text = "" + RM.earthTowerPrice;
        
        
        waveDisplay.text = "wave : " + RM.wave;

        if (ingamePanel.activeSelf)
        {
            if (Input.GetKeyDown(shortcutAlchimiePanel))
            {
                GoToAlchimiePanel();
            }
            if (Input.GetKeyDown(shortcutLaunchWave))
            {
                FindObjectOfType<Spawn>().ButtonFonctionLaunchWave();
            }
            if (Input.GetKeyDown(shortcutFireTower))
            {
                GridBuilding.current.PreInitializeFeu(IconFeu);
            }
            if (Input.GetKeyDown(shortcutWaterTower))
            {
                GridBuilding.current.PreInitializeFeu(IconEau);
            }
            if (Input.GetKeyDown(shortcutEarthTower))
            {
                GridBuilding.current.PreInitializeFeu(IconTerre);
            }
        }
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
