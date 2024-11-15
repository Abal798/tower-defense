using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellsBrewingScripts : MonoBehaviour
{
    public RessourcesManager RM;
    public UIManager UIM;
    
    public TextMeshProUGUI fireSoulDisplay;
    public TextMeshProUGUI waterSoulDisplay;
    public TextMeshProUGUI earthSoulDisplay;

    public TextMeshProUGUI firePriceDisplay;
    public TextMeshProUGUI waterPriceDisplay;
    public TextMeshProUGUI earthPriceDisplay;

    public List<int> brewedSpell = new List<int>();

    [Header("display")] 
    public GameObject[] ingredientOne;
    public GameObject[] ingredientTwo;
    public GameObject[] ingredientThree;

    private int fireDosePrice = 60;
    private int waterDosePrice = 60;
    private int earthDosePrice = 60;

    private int fireDoseUtilisation;
    private int waterDoseUtilisation;
    private int earthDoseUtilisation;
    

    private void Start()
    {
        fireDosePrice = RM.basicFireDosePrice;
        waterDosePrice = RM.basicWaterDosePrice;
        earthDosePrice = RM.basicEarthDosePrice;
    }


    void Update()
    {
        fireSoulDisplay.text = "fire : " + RM.fireSoul.ToString();
        waterSoulDisplay.text = "water : " + RM.waterSoul.ToString();
        earthSoulDisplay.text = "earth : " + RM.plantSoul.ToString();

        firePriceDisplay.text = "price : " + fireDosePrice.ToString();
        waterPriceDisplay.text = "price : " + waterDosePrice.ToString();
        earthPriceDisplay.text = "price : " + earthDosePrice.ToString();

        if (gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(1))
            {
                ResetRecipie();
            }
        }
            
    }

    public void FireButtonSelected()
    {
        if (brewedSpell.Count < 3)
        {
            if(RM.fireSoul > fireDosePrice)
            {
                fireDoseUtilisation++;
                RM.fireSoul -= fireDosePrice;
                brewedSpell.Add(1);
                recipieDisplayAdd(0);
                RecalculateSpellPrice();
            }
            else
            {
                UIM.DisplayAlert("cannot afford this");
            }
        }
        else
        {
            UIM.DisplayAlert("3 is the maximum ingredient you can fit");
        }
    }

    public void WaterButtonSelected()
    {
        if (brewedSpell.Count < 3)
        {
            if(RM.waterSoul > waterDosePrice)
            {
                waterDoseUtilisation++;
                RM.waterSoul -= waterDosePrice;
                brewedSpell.Add(2);
                recipieDisplayAdd(1);
                RecalculateSpellPrice();
            }
            else
            {
                UIM.DisplayAlert("cannot afford this");
            }
        }
        else
        {
            UIM.DisplayAlert("3 is the maximum ingredient you can fit");
        }
    }

    public void EarthButtonSelected()
    {
        if (brewedSpell.Count < 3)
        {
            if(RM.plantSoul > earthDosePrice)
            {
                earthDoseUtilisation++;
                RM.plantSoul -= earthDosePrice;
                brewedSpell.Add(3);
                recipieDisplayAdd(2);
                RecalculateSpellPrice();
            }
            else
            {
                UIM.DisplayAlert("cannot afford this");
            }
        }
        else
        {
            UIM.DisplayAlert("3 is the maximum ingredient you can fit");
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
                    RM.fireSoul += fireDosePrice - 1;
                    fireDoseUtilisation--;
                    RecalculateSpellPrice();
                }
                
                else if (brewedSpell[i] == 2)
                {
                    RM.waterSoul +=  waterDosePrice - 1;
                    waterDoseUtilisation--;
                    RecalculateSpellPrice();
                }

                else
                {
                    RM.plantSoul += earthDosePrice - 1;
                    earthDoseUtilisation--;
                    RecalculateSpellPrice();
                }
                
            }
            brewedSpell.Clear();
            RecipieDisplayReset();
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
                RecipieDisplayReset();
                UIM.DisplayAlert("spell saved");
                
            }
            
            else if (RM.spellSlotTwo.Count == 0)
            {
                for (int i = 0; i < brewedSpell.Count; i++)
                {
                    RM.spellSlotTwo.Add(brewedSpell[i]);
                }
                brewedSpell.Clear();
                RecipieDisplayReset();
                UIM.DisplayAlert("spell saved");
                
            }
            
            else if (RM.spellSlotThree.Count == 0)
            {
                for (int i = 0; i < brewedSpell.Count; i++)
                {
                    RM.spellSlotThree.Add(brewedSpell[i]);
                }
                brewedSpell.Clear();
                RecipieDisplayReset();
                UIM.DisplayAlert("spell saved");
                
            }
            

            else
            {
                UIM.DisplayAlert("pas de slot libre");
            }
        }
        else
        {
            UIM.DisplayAlert("sort ne contient pas assez d'element");
        }
    }

    void recipieDisplayAdd(int i)
    {
        if (brewedSpell.Count == 1)
        {
            ingredientOne[i].SetActive(true);
        }
        else if(brewedSpell.Count == 2)
        {
            ingredientTwo[i].SetActive(true);
        }
        else if(brewedSpell.Count == 3)
        {
            ingredientThree[i].SetActive(true);
        }
    }
    
    void RecipieDisplayReset()
    {
        foreach (var VARIABLE in ingredientOne)
        {
            VARIABLE.SetActive(false);
        }
        foreach (var VARIABLE in ingredientTwo)
        {
            VARIABLE.SetActive(false);
        }
        foreach (var VARIABLE in ingredientThree)
        {
            VARIABLE.SetActive(false);
        }
    }

    void RecalculateSpellPrice()
    {
        fireDosePrice = Mathf.RoundToInt((fireDoseUtilisation * RM.spellAugmentationPriceFactor) + RM.basicFireDosePrice);
        waterDosePrice = Mathf.RoundToInt((waterDoseUtilisation * RM.spellAugmentationPriceFactor) + RM.basicWaterDosePrice);
        earthDosePrice = Mathf.RoundToInt((earthDoseUtilisation * RM.spellAugmentationPriceFactor) + RM.basicEarthDosePrice);
    }
}