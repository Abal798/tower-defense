using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject ingamePanel;
    public GameObject alchimiePanel;
    void Start()
    {
        ingamePanel.SetActive(true);
        alchimiePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void QuitAlchimiePanel()
    {
        ingamePanel.SetActive(true);
        alchimiePanel.SetActive(false);
    }

    public void GoToAlchimiePanel()
    {
        ingamePanel.SetActive(false);
        alchimiePanel.SetActive(true);
    }
}
