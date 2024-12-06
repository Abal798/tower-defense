using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject pausePanel;
    public GameObject ingamePanel;
    public bool isPaused;
    
    void Start()
    {
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        ingamePanel.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        ingamePanel.SetActive(true);
        Time.timeScale = (gameManager.isInFstMode) ? gameManager.accelerationFactor : 1;
        isPaused = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene(0);
    }   
}
