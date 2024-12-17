using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulConvertingScript : MonoBehaviour
{
    public RessourcesManager RM;
    public Slider soulAmount;

    public GameObject[] recipieDisplay;

    public List<int> recipie = new List<int>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        float convertAmount = soulAmount.value;

        if (recipie.Count == 3)
        {
            for (int i = 0; i < recipie.Count - 1; i++)
            {
                if (recipie[i] == 1) RM.fireSoul -= convertAmount;
                if (recipie[i] == 2) RM.waterSoul -= convertAmount;
                if (recipie[i] == 3) RM.plantSoul -= convertAmount;
            }
            if (recipie[2] == 1) RM.fireSoul += convertAmount;
            if (recipie[2] == 2) RM.waterSoul += convertAmount;
            if (recipie[2] == 3) RM.plantSoul += convertAmount;
            
            ResetRecipie();
        }
        else
        {
            UIManager.UIM.DisplayAlert("not enough indication to convert");
            AudioManager.AM.PlaySfx(AudioManager.AM.alertDisplay);
        }
    }
}
 