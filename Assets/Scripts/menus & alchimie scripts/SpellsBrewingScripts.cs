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

    public List<int> brewedSpell = new List<int>();
    
    
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
        if (brewedSpell.Count < 3 && RM.fireSoul > 10)
        {
            RM.fireSoul -= 10;
            brewedSpell.Add(1);
        }
        
    }

    public void WaterButtonSelected()
    {
        if (brewedSpell.Count < 3 && RM.waterSoul > 10)
        {
            RM.waterSoul -= 10;
            brewedSpell.Add(2);
        }
    }

    public void EarthButtonSelected()
    {
        if (brewedSpell.Count < 3 && RM.plantSoul > 10)
        {
            RM.plantSoul -= 10;
            brewedSpell.Add(3);
        }
    }


}
