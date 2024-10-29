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

    public void ResetRecipie()
    {
        if (brewedSpell.Count > 0)
        {
            for (int i = 0; i < brewedSpell.Count; i++)
            {
                if (brewedSpell[i] == 1)
                {
                    RM.fireSoul += 10;
                }
                
                else if (brewedSpell[i] == 2)
                {
                    RM.waterSoul += 10;
                }

                else
                {
                    RM.plantSoul += 10;
                }
                
            }
            brewedSpell.Clear();
        }
    }

    public void SaveSpell()
    {
        if (brewedSpell.Count > 1)
        {
            if (RM.spellSlotOne.Count == 0)
            {
                for (int i = 0; i < brewedSpell.Count; i++)
                {
                    RM.spellSlotOne.Add(brewedSpell[i]);
                }
                brewedSpell.Clear();
            }
            
            else if (RM.spellSlotTwo.Count == 0)
            {
                for (int i = 0; i < brewedSpell.Count; i++)
                {
                    RM.spellSlotTwo.Add(brewedSpell[i]);
                }
                brewedSpell.Clear();
            }
            
            else if (RM.spellSlotThree.Count == 0)
            {
                for (int i = 0; i < brewedSpell.Count; i++)
                {
                    RM.spellSlotThree.Add(brewedSpell[i]);
                }
                brewedSpell.Clear();
            }

            else
            {
                Debug.Log("aucun slot de libre");
            }
        }
        else
        {
            Debug.Log("sort ne contient pas assez d'element");
        }
    }
}
