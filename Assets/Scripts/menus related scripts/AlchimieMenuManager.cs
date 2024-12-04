using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchimieMenuManager : MonoBehaviour
{
    public SpellsBrewingScripts SB;

    public GameObject ingamePanel;
    public GameObject alchimiePanel;
    
    // Start is called before the first frame update
    private void Awake()
    {
        alchimiePanel = gameObject;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                QuitAlchimiePanel();
            }
    }
    
    public void QuitAlchimiePanel()
    {
        AudioManager.AM.PlaySfx(AudioManager.AM.buttonClick);
        SB.ResetRecipie();
        ingamePanel.SetActive(true);
        alchimiePanel.SetActive(false);
    }
}
