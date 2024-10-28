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

    public string brewedSpell = "";
    
    
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

    public void FireButtonSelected()
    {
        if (brewedSpell.Length < 3)
        {
            RM.fireSoul -= 10;
            brewedSpell += "F";
        }
        
    }

    public void WaterButtonSelected()
    {
        if (brewedSpell.Length < 3)
        {
            RM.waterSoul -= 10;
            brewedSpell += "W";
        }
    }

    public void EarthButtonSelected()
    {
        if (brewedSpell.Length < 3)
        {
            RM.plantSoul -= 10;
            brewedSpell += "E";
        }
    }


}
