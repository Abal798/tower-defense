using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulConverterMenuManager : MonoBehaviour
{
    public Animation openPanel;
    
    public RessourcesManager RM;
    public Slider soulAmountSlider;
    public TextMeshProUGUI soulAmountDisplay;

    public TextMeshProUGUI fireSoulAmount;
    public TextMeshProUGUI waterSoulAmount;
    public TextMeshProUGUI earthSoulAmount;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        soulAmountSlider.value = 0;
        openPanel.Play();
    }

    // Update is called once per frame
    void Update()
    {
        fireSoulAmount.text = "" + RM.fireSoul;
        waterSoulAmount.text = "" + RM.waterSoul;
        earthSoulAmount.text = "" + RM.plantSoul;
        soulAmountDisplay.text = "" + soulAmountSlider.value;
    }
}
