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
    public KeyCode shortcutPotion1;
    public KeyCode shortcutPotion2;
    public KeyCode shortcutPotion3;
    
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
                GridBuilding.current.PreInitializeEau(IconEau);
            }
            if (Input.GetKeyDown(shortcutEarthTower))
            {
                GridBuilding.current.PreInitializeTerre(IconTerre);
            }
            if (Input.GetKeyDown(shortcutPotion1))
            {
                RM.gameObject.GetComponent<SpellPlacingScript>().Spell1();
            }
            if (Input.GetKeyDown(shortcutPotion2))
            {
                RM.gameObject.GetComponent<SpellPlacingScript>().Spell2();
            }
            if (Input.GetKeyDown(shortcutPotion3))
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
}
