using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipExtented : MonoBehaviour
{
    public static TooltipExtented TooltipExtentedInstance { get; private set;  }
    
    [SerializeField] private RectTransform canvasRectTransform;
    
    private TextMeshProUGUI ameliorationsTextExtented;
    private TextMeshProUGUI pvTextExtented;
    private TextMeshProUGUI degatsTextExtented;
    private TextMeshProUGUI cadenceTextExtented;
    private TextMeshProUGUI porteeTextExtented;
    private TextMeshProUGUI surrFireTextExtented;
    private TextMeshProUGUI surrWaterTextExtented;
    private TextMeshProUGUI surrEarthTextExtented;
    

    private Image element1;
    private Image element2;
    private Image element3;
    private List<Image> listImage = new List<Image>();

    public Sprite feuSprite;
    public Sprite waterSprite;
    public Sprite earthSprite;
        
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    
    private void Awake()
    {
        TooltipExtentedInstance = this;
        backgroundRectTransform = transform.Find("BackgroundComplet").GetComponent<RectTransform>();
        
        ameliorationsTextExtented = transform.Find("AmeliorationText").GetComponent<TextMeshProUGUI>();
        pvTextExtented = transform.Find("PvText").GetComponent<TextMeshProUGUI>();
        degatsTextExtented = transform.Find("DegatsText").GetComponent<TextMeshProUGUI>();
        cadenceTextExtented = transform.Find("CadenceText").GetComponent<TextMeshProUGUI>();
        porteeTextExtented = transform.Find("PorteeText").GetComponent<TextMeshProUGUI>();
        surrFireTextExtented = transform.Find("SurroundingsFireText").GetComponent<TextMeshProUGUI>();
        surrWaterTextExtented = transform.Find("SurroundingsWaterText").GetComponent<TextMeshProUGUI>();
        surrEarthTextExtented = transform.Find("SurroundingsEarthText").GetComponent<TextMeshProUGUI>();

        element1 = transform.Find("Element1").GetComponent<Image>();
        element2 = transform.Find("Element2").GetComponent<Image>();
        element3 = transform.Find("Element3").GetComponent<Image>();
        rectTransform = transform.GetComponent<RectTransform>();
        
        listImage.Add(element1);
        listImage.Add(element2);
        listImage.Add(element3);
        
        
        
        HideTooltip();
    }
    

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
        
    }

    private void ShowToolTip(int ameliorations, List<int>element, string pv, int degats, float cadence, float portee,  int surrFire, int surrWater, int surrEarth)
    {
        gameObject.SetActive(true);
        ameliorationsTextExtented.SetText(ameliorations.ToString());
        pvTextExtented.SetText(pv);
        degatsTextExtented.SetText(""+ degats);
        cadenceTextExtented.SetText(""+ cadence);
        porteeTextExtented.SetText(""+ portee);
        surrEarthTextExtented.SetText("" + surrEarth);
        surrFireTextExtented.SetText("" + surrFire);
        surrWaterTextExtented.SetText("" + surrWater);

        for (int i = 0; i < listImage.Count; i++)
        {
            listImage[i].gameObject.SetActive(i < ameliorations);
            if (i < ameliorations && element.Count > i)
            {
                switch (element[i])
                {
                    case 1:
                        listImage[i].sprite = feuSprite;
                        break;
                    case 2:
                        listImage[i].sprite = waterSprite;
                        break;
                    case 3:
                        listImage[i].sprite = earthSprite;
                        break;
                    default:
                        listImage[i].sprite = null;
                        break;
                }
            }
        }
        
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowToolTip_Static(int ameliorations, List<int>element, string pv, float degats, float cadence, float portee, int surrFire, int surrWater, int surrEarth)
    {
        TooltipExtentedInstance.ShowToolTip(ameliorations, element,  pv, Mathf.FloorToInt(degats),  cadence,  portee, surrFire, surrWater, surrEarth);
    }

    public static void HideTooltip_Static()
    {
        TooltipExtentedInstance.HideTooltip();
    }
    
}

