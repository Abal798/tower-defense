using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellsBrewingScripts : MonoBehaviour
{
    public RessourcesManager RM;
    
    public TextMeshProUGUI fireSoulDisplay;
    public TextMeshProUGUI waterSoulDisplay;
    public TextMeshProUGUI earthSoulDisplay;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireSoulDisplay.text = "fire : " + RM.fireSoul.ToString();
        waterSoulDisplay.text = "water : " + RM.waterSoul.ToString();
        earthSoulDisplay.text = "earth : " + RM.plantSoul.ToString();
    }


}
