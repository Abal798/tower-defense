using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoulConvertingScript : MonoBehaviour
{
    public GameObject fireIcon;
    public GameObject waterIcon;
    public GameObject earthIcon;
    
    public GameObject fireSlotParent;
    public GameObject waterSlotParent;
    public GameObject earthSlotParent;

    public GameObject ingredientSlotOne;
    public GameObject ingredientSlotTwo;
    public GameObject resultSlot;

    public TextMeshProUGUI sliderValueDisplay;
    public Slider numberOfIngredient;

    void Start()
    {
        UpdateValueText(numberOfIngredient.value);
        numberOfIngredient.onValueChanged.AddListener(UpdateValueText);
    }
    
    void UpdateValueText(float value)
    {
        sliderValueDisplay.text = Mathf.RoundToInt(value).ToString();
    }
}