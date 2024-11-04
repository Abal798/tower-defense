using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public RessourcesManager RM;
    
    public GameObject ingamePanel;
    public GameObject alchimiePanel;
    
    public TextMeshProUGUI fireSoulIngameDisplay;
    public TextMeshProUGUI WaterSoulIngameDisplay;
    public TextMeshProUGUI EarthSoulIngameDisplay;
    void Start()
    {
        ingamePanel.SetActive(true);
        alchimiePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        fireSoulIngameDisplay.text = RM.fireSoul.ToString();
        WaterSoulIngameDisplay.text = RM.waterSoul.ToString();
        EarthSoulIngameDisplay.text = RM.plantSoul.ToString();

    }

    public void GoToAlchimiePanel()
    {
        ingamePanel.SetActive(false);
        alchimiePanel.SetActive(true);
    }
}
