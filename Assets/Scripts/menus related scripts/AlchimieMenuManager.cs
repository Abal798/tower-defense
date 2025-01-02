using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchimieMenuManager : MonoBehaviour
{
    public SpellsBrewingScripts SB;

    public GameObject ingamePanel;
    public GameObject alchimiePanel;

    public Animation openMenu;
    
    // Start is called before the first frame update
    private void Awake()
    {
        alchimiePanel = gameObject;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    
    public void QuitAlchimiePanel()
    {
        SB.ResetRecipie();
        ingamePanel.SetActive(true);
        alchimiePanel.SetActive(false);
        MenuManager.activePanel = ingamePanel;
    }

    private void OnEnable()
    {
        openMenu.Play();
    }
}
