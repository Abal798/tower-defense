using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RessourcesManager RM;
    
    public GameObject ingamePanel;
    public GameObject alchimiePanel;
    public GameObject keyRebindingPanel;
    public GameObject gameStatsPanel;
    public GameObject pausePanel;
    public GameObject SoulConverterPanel;
    public GameObject IconFeu;
    public GameObject IconEau;
    public GameObject IconTerre;
    public static GameObject activePanel;

    public TextMeshProUGUI waveDisplay;
    public TextMeshProUGUI fireSoulIngameDisplay;
    public TextMeshProUGUI waterSoulIngameDisplay;
    public TextMeshProUGUI earthSoulIngameDisplay;
    
    public TextMeshProUGUI fireTowerPriceDisplay;
    public TextMeshProUGUI waterTowerPriceDisplay;
    public TextMeshProUGUI earthTowerPriceDisplay;

    public KeyRebinding keyRebinder;
    public GameManager gameManager;

    private void Awake()
    {
        if (keyRebinder == null) keyRebinder = transform.GetChild(2).gameObject.GetComponent<KeyRebinding>();
    }


    void Start()
    {
        ingamePanel.SetActive(true);
        SoulConverterPanel.SetActive(false);
        alchimiePanel.SetActive(false);
        keyRebindingPanel.SetActive(false);
        gameStatsPanel.SetActive(false);
        pausePanel.SetActive(false);
        activePanel = ingamePanel;
    }
    
    void Update()
    {
        //Debug.Log("active panel " + activePanel);
        
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

            if (Input.GetKeyDown(KeyCode.S))
            {
                gameStatsPanel.SetActive(!gameStatsPanel.activeSelf);
                EndGameStats.EGS.DisplayGameStats();
            }
        }

        if (activePanel != ingamePanel)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ingamePanel.SetActive(true);
                alchimiePanel.SetActive(false);
                keyRebindingPanel.SetActive(false);
                pausePanel.SetActive(false);
                activePanel = ingamePanel;
            }
        }

        if (Input.GetMouseButtonDown(1) && activePanel == ingamePanel)
        {
            foreach (var tower in GridBuilding.current.listeTowerCo)
            {
                tower.Value.GetComponent<TowerRemover>().UnSelectTower();
            }
        }
    }

    public void GoToAlchimiePanel()
    {
        if (alchimiePanel.activeSelf == false)
        {
            alchimiePanel.SetActive(true);
            activePanel = alchimiePanel;
        }
        else
        {
            alchimiePanel.SetActive(false);
            activePanel = ingamePanel;
        }
        
    }
    
    public void GoToSoulConverterPanel()
    {
        if (SoulConverterPanel.activeSelf == false)
        {
            SoulConverterPanel.SetActive(true);
            activePanel = SoulConverterPanel;
        }
        else
        {
            SoulConverterPanel.SetActive(false);
            activePanel = ingamePanel;
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void QuitThisPanel(GameObject panel)
    {
        panel.SetActive(true);
        keyRebindingPanel.SetActive(false);
        activePanel = ingamePanel;
    }

    public void GoToKeyPanel()
    {
        ingamePanel.SetActive(false);
        pausePanel.SetActive(false);
        keyRebindingPanel.SetActive(true);
        activePanel = keyRebindingPanel;
    }

    public void FastButtonPressed(Button button)
    {
        gameManager.ChangeTimeScale(button);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
