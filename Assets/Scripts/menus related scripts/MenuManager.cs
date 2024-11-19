using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public RessourcesManager RM;
    
    public GameObject ingamePanel;
    public GameObject alchimiePanel;
    public GameObject keyRebindingPanel;
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

    public KeyRebinding keyRebinder;

    private void Awake()
    {
        keyRebinder = FindObjectOfType<KeyRebinding>();
    }


    void Start()
    {
        ingamePanel.SetActive(true);
        alchimiePanel.SetActive(false);
        keyRebindingPanel.SetActive(false);
    }
    
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
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutAlchimiePanel")))
            {
                GoToAlchimiePanel();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutLaunchWave")))
            {
                FindObjectOfType<Spawn>().ButtonFonctionLaunchWave();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutFireTower")))
            {
                GridBuilding.current.PreInitializeFeu(IconFeu);
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutWaterTower")))
            {
                GridBuilding.current.PreInitializeEau(IconEau);
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutEarthTower")))
            {
                GridBuilding.current.PreInitializeTerre(IconTerre);
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutPotion1")))
            {
                RM.gameObject.GetComponent<SpellPlacingScript>().Spell1();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutPotion2")))
            {
                RM.gameObject.GetComponent<SpellPlacingScript>().Spell2();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutPotion3")))
            {
                RM.gameObject.GetComponent<SpellPlacingScript>().Spell3();
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

    public void QuitKeyPanel()
    {
        ingamePanel.SetActive(true);
        keyRebindingPanel.SetActive(false);
    }

    public void GoToKeyPanel()
    {
        ingamePanel.SetActive(false);
        keyRebindingPanel.SetActive(true);
    }
}
