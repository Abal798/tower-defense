using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainMenuPanel;
    public GameObject keyRebindingPanel;
    public GameObject titreJeu;

    public void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    public void GoToKeyPanel()
    {
        settingsPanel.SetActive(false);
        keyRebindingPanel.SetActive(true);
        titreJeu.SetActive(false);
    }

    public void LeaveKeyPanel()
    {
        settingsPanel.SetActive(true);
        keyRebindingPanel.SetActive(false);
        titreJeu.SetActive(true);
    }
}
