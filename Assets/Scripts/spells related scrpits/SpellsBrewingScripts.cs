using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellsBrewingScripts : MonoBehaviour
{
    public RessourcesManager RM;
    public UIManager UIM;
    public KeyRebinding keyRebinder;
    
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

    public GameObject currentSpellShape;
    
    private void Awake()
    {
        if (RM == null) RM = FindObjectOfType<RessourcesManager>();
        if (UIM == null) UIM = FindObjectOfType<UIManager>();
        if (keyRebinder == null) keyRebinder = FindObjectOfType<KeyRebinding>();
    }
    

    private void Start()
    {
        fireDosePrice = RM.basicFireDosePrice;
        waterDosePrice = RM.basicWaterDosePrice;
        earthDosePrice = RM.basicEarthDosePrice;
    }


    private void Update()
    {
        fireSoulDisplay.text = "fire : " + RM.fireSoul.ToString();
        waterSoulDisplay.text = "water : " + RM.waterSoul.ToString();
        earthSoulDisplay.text = "earth : " + RM.plantSoul.ToString();

        firePriceDisplay.text = "price : " + fireDosePrice.ToString();
        waterPriceDisplay.text = "price : " + waterDosePrice.ToString();
        earthPriceDisplay.text = "price : " + earthDosePrice.ToString();

        if (MenuManager.activePanel == gameObject)
        {
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("fireIngredientKey")))
            {
                FireButtonSelected();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("waterIngredientKey")))
            {
                WaterButtonSelected();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("earthIngredientKey")))
            {
                EarthButtonSelected();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("saveSpellKey")))
            {
                SaveSpell();
            }
            if (Input.GetKeyDown(keyRebinder.GetKeyForAction("resetRecipieKey")))
            {
                ResetRecipie();
            }
        }
    }

    public void FireButtonSelected()
    {
        if (brewedSpell.Count < 2)
        {
            if(RM.fireSoul > fireDosePrice)
            {
                AudioManager.AM.PlaySfx(AudioManager.AM.addFireIngredient);
                fireDoseUtilisation++;
                RM.fireSoul -= fireDosePrice;
                brewedSpell.Add(1);
                recipieDisplayAdd(0);
                RecalculateSpellPrice();
            }
            else
            {
                AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
                UIM.DisplayAlert("cannot afford this");
            }
        }
        else
        {
            AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
            UIM.DisplayAlert("3 is the maximum ingredient you can fit");
        }
    }

    public void WaterButtonSelected()
    {
        if (brewedSpell.Count < 2)
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
                AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
                UIM.DisplayAlert("cannot afford this");
            }
        }
        else
        {
            AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
            UIM.DisplayAlert("3 is the maximum ingredient you can fit");
        }
    }

    public void EarthButtonSelected()
    {
        if (brewedSpell.Count < 2)
        {
            if(RM.plantSoul > earthDosePrice)
            {
                AudioManager.AM.PlaySfx(AudioManager.AM.addEarthIngredient);
                earthDoseUtilisation++;
                RM.plantSoul -= earthDosePrice;
                brewedSpell.Add(3);
                recipieDisplayAdd(2);
                RecalculateSpellPrice();
            }
            else
            {
                AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
                UIM.DisplayAlert("cannot afford this");
            }
        }
        else
        {
            AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
            UIM.DisplayAlert("only two ingredients can fit");
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
                    RM.fireSoul += fireDosePrice - RM.spellAugmentationPriceFactor;
                    fireDoseUtilisation--;
                    RecalculateSpellPrice();
                }
                
                else if (brewedSpell[i] == 2)
                {
                    RM.waterSoul +=  waterDosePrice - RM.spellAugmentationPriceFactor;
                    waterDoseUtilisation--;
                    RecalculateSpellPrice();
                }

                else
                {
                    RM.plantSoul += earthDosePrice - RM.spellAugmentationPriceFactor;
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
                AudioManager.AM.PlaySfx(AudioManager.AM.cook);
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
                AudioManager.AM.PlaySfx(AudioManager.AM.cook);
                
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
                AudioManager.AM.PlaySfx(AudioManager.AM.cook);
            }

            else
            {
                AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
                UIM.DisplayAlert("pas de slot libre");
            }
        }
        else
        {
            AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
            UIM.DisplayAlert("sort ne contient pas assez d'element");
        }
    }

    private void recipieDisplayAdd(int i)
    {
        if (brewedSpell.Count == 1)
        {
            ingredientOne[i].SetActive(true);
            currentSpellShape = ingredientOne[i + 3];
            currentSpellShape.GetComponent<Image>().color = Color.grey;
            currentSpellShape.SetActive(true);
        }
        else if(brewedSpell.Count == 2)
        {
            ingredientTwo[i].SetActive(true);
        
            if (i == 0)
            {
                currentSpellShape.GetComponent<Image>().color = Color.red;
            }
            else if (i == 1)
            {
                currentSpellShape.GetComponent<Image>().color = Color.blue;
            }
            else if (i == 2)
            {
                currentSpellShape.GetComponent<Image>().color = Color.green;
            }
                
        
            
        }
        else if(brewedSpell.Count == 3)
        {
            ingredientThree[i].SetActive(true);
        }
    }

    private void RecipieDisplayReset()
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

        currentSpellShape = null;
    }

    private void RecalculateSpellPrice()
    {
        fireDosePrice = Mathf.RoundToInt((fireDoseUtilisation * RM.spellAugmentationPriceFactor) + RM.basicFireDosePrice);
        waterDosePrice = Mathf.RoundToInt((waterDoseUtilisation * RM.spellAugmentationPriceFactor) + RM.basicWaterDosePrice);
        earthDosePrice = Mathf.RoundToInt((earthDoseUtilisation * RM.spellAugmentationPriceFactor) + RM.basicEarthDosePrice);
    }
}