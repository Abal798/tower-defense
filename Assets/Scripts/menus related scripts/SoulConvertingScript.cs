using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoulConvertingScript : MonoBehaviour
{
    public RessourcesManager RM;
    public Slider soulAmount;

    public GameObject[] recipieDisplay;

    public List<int> recipie = new List<int>();
    
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void AddIngredient(int ingredient)
    {
        if (recipie.Count <= 3)
        {
            recipie.Add(ingredient);
            DisplayNewIngredient(ingredient);
        }
        else
        {
            UIManager.UIM.DisplayAlert("recipie already complete");
            AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
        }

        if (recipie.Count < 3)
        {
            float minValue = getSoulAmountWithType(recipie[0]);
            foreach (var souls in recipie)
            {
                if (getSoulAmountWithType(souls) < minValue) minValue = getSoulAmountWithType(souls);
            }

            soulAmount.maxValue = minValue;
        }
        
    }

    public void DisplayNewIngredient(int ingredient)
    {
        recipieDisplay[recipie.Count-1].transform.GetChild(ingredient).gameObject.SetActive(true);
    }

    public void ResetRecipie()
    {
        recipie.Clear();
        foreach (var ingredient in recipieDisplay)
        {
            for (int i = 1; i < 4; i++)
            {
                ingredient.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void Convert()
    {
        if (recipie.Count != 3)
        {
            UIManager.UIM.DisplayAlert("Not enough ingredients to convert");
            AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
            return;
        }

        float convertAmount = soulAmount.value;
        if (recipie[0] == recipie[1] && getSoulAmountWithType(recipie[0]) < convertAmount * 2)
        {
            UIManager.UIM.DisplayAlert("Not enough soul amounts for conversion");
            AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
            return;
        }

        for (int i = 0; i < recipie.Count - 1; i++)
        {
            UpdateSoulAmount(recipie[i], -convertAmount);
        }

        UpdateSoulAmount(recipie[2], convertAmount);
        ResetRecipie();
    }

    private void UpdateSoulAmount(int type, float amount)
    {
        if (type == 1) RM.fireSoul += amount;
        else if (type == 2) RM.waterSoul += amount;
        else if (type == 3) RM.plantSoul += amount;
    }


    public float getSoulAmountWithType(int type)
    {
        if (type == 1) return RM.fireSoul;
        if (type == 2) return RM.waterSoul;
        return RM.plantSoul;
    }
}
 