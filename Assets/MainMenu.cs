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
    public GameObject tutoChoice;
    public GameObject creditPanel;

    public void Start()
    {
        settingsPanel.SetActive(false);
        tutoChoice.SetActive(false);
        creditPanel.SetActive(false);
    }

    public void PlayGame()
    {
        mainMenuPanel.SetActive(false);
        tutoChoice.SetActive(true);
    }

    public void PlayTuto()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayWithoutTuto()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        mainMenuPanel.SetActive(true);
        creditPanel.SetActive(false);
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
