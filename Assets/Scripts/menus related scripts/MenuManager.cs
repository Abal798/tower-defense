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
    public Spawn spawn;
    
    public Button speedSimulationButton;
    public GameObject ingamePanel;
    public GameObject alchimiePanel;
    public GameObject keyRebindingPanel;
    public GameObject gameStatsPanel;
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject SoulConverterPanel;
    public GameObject bookPanel;
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
    
    public bool isPaused;

    private void Awake()
    {
        if (keyRebinder == null) keyRebinder = transform.GetChild(2).gameObject.GetComponent<KeyRebinding>();
    }


    private void Start()
    {
        ingamePanel.SetActive(true);
        SoulConverterPanel.SetActive(false);
        alchimiePanel.SetActive(false);
        keyRebindingPanel.SetActive(false);
        gameStatsPanel.SetActive(false);
        pausePanel.SetActive(false);
        bookPanel.SetActive(false);
        activePanel = ingamePanel;
    }

    private void Update()
    {
        fireSoulIngameDisplay.text = "" + RM.fireSoul;
        waterSoulIngameDisplay.text = "" + RM.waterSoul;
        earthSoulIngameDisplay.text = "" + RM.plantSoul;

        fireTowerPriceDisplay.text = "" + RM.GetTowerPrice(1,RM.nbrOfFireTower);
        waterTowerPriceDisplay.text = "" + RM.GetTowerPrice(1,RM.nbrOfWaterTower);
        earthTowerPriceDisplay.text = "" + RM.GetTowerPrice(1,RM.nbrOfEarthTower);
        
        if(RM.GetTowerPrice(1,RM.nbrOfFireTower) > RM.fireSoul)fireTowerPriceDisplay.color = Color.red;
        else fireTowerPriceDisplay.color = Color.white;
        if(RM.GetTowerPrice(1,RM.nbrOfWaterTower) > RM.waterSoul)waterTowerPriceDisplay.color = Color.red;
        else waterTowerPriceDisplay.color = Color.white;
        if(RM.GetTowerPrice(1,RM.nbrOfEarthTower) > RM.plantSoul)earthTowerPriceDisplay.color = Color.red;
        else earthTowerPriceDisplay.color = Color.white;
        
        
        
        waveDisplay.text = "Vague : " + RM.wave;

        if (ingamePanel.activeSelf && TutorialBehaviour.isInTutorial == false)
        {
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutAlchimiePanel")))
            {
                GoToAlchimiePanel();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutLaunchWave")) && spawn.monstersAlive.Count == 0)
            {
                FindObjectOfType<Spawn>().ButtonFonctionLaunchWave();
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

            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("shortcutTimeSpeed")))
            {
                FastButtonPressed(speedSimulationButton);
            }
            
        }

        if (activePanel == ingamePanel)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    ResumeGame();

                }
                else
                {
                    PauseGame();

                }
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                GoToBookPanel();
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
        }

        if (activePanel == bookPanel)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitThisPanel(bookPanel);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            OpenStats();
        }



        if (Input.GetMouseButtonDown(1) && activePanel == ingamePanel)
        {
            foreach (var tower in GridBuilding.current.listeTowerCo)
            {
                tower.Value.GetComponent<ActualizeChild>().UnSelectTower();
            }
        }
        
    }

    public void OpenStats()
    {
        gameStatsPanel.SetActive(!gameStatsPanel.activeSelf);
        EndGameStats.EGS.DisplayGameStats();
    }

    public void GoToAlchimiePanel()
    {
        if (alchimiePanel.activeSelf == false)
        {
            alchimiePanel.GetComponent<SpellsBrewingScripts>().ResetRecipie();
            alchimiePanel.SetActive(true);
            SoulConverterPanel.SetActive(false);
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
            alchimiePanel.SetActive(false);
            SoulConverterPanel.SetActive(true);
            activePanel = SoulConverterPanel;
        }
        else
        {
            SoulConverterPanel.GetComponent<SoulConvertingScript>().ResetRecipie();
            SoulConverterPanel.SetActive(false);
            activePanel = ingamePanel;
        }
        
    }
    
    public void GoToBookPanel()
    {
        ingamePanel.SetActive(false);
        pausePanel.SetActive(false);
        bookPanel.SetActive(true);
        activePanel = bookPanel;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
        activePanel = settingsPanel;
    }

    public void QuitSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
        activePanel = pausePanel;
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void QuitThisPanel(GameObject panel)
    {
        panel.SetActive(false);
        ingamePanel.SetActive(true);
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

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        ingamePanel.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
        activePanel = pausePanel;
    }

    public void ResumeGame()
    {
        QuitThisPanel(pausePanel);
        if (TutorialBehaviour.isInTutorial == false) Time.timeScale = (gameManager.isInFstMode) ? gameManager.accelerationFactor : 1;
        else
        {
            Time.timeScale = TutorialBehaviour.tutorialTimeScale;
        }
        isPaused = false;
        activePanel = ingamePanel;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene(0);
        
    }

    public void TimeScaleActive()
    {
        Time.timeScale = gameManager.isInFstMode ? gameManager.accelerationFactor : 1;
    }

}
