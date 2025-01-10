using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TooltipComplet : MonoBehaviour
{
    public static TooltipComplet TooltipCompletInstance { get; private set;  }
    
    [SerializeField] private RectTransform canvasRectTransform;
    
    private TextMeshProUGUI ameliorationsText;
    private TextMeshProUGUI pvText;
    private TextMeshProUGUI degatsText;
    private TextMeshProUGUI cadenceText;
    private TextMeshProUGUI porteeText;

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
        TooltipCompletInstance = this;
        backgroundRectTransform = transform.Find("BackgroundComplet").GetComponent<RectTransform>();
        
        ameliorationsText = transform.Find("AmeliorationText").GetComponent<TextMeshProUGUI>();
        pvText = transform.Find("PvText").GetComponent<TextMeshProUGUI>();
        degatsText = transform.Find("DegatsText").GetComponent<TextMeshProUGUI>();
        cadenceText = transform.Find("CadenceText").GetComponent<TextMeshProUGUI>();
        porteeText = transform.Find("PorteeText").GetComponent<TextMeshProUGUI>();

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

    private void ShowToolTip(int ameliorations, List<int>element, string pv, int degats, float cadence, float portee)
    {
        gameObject.SetActive(true);
        ameliorationsText.SetText(ameliorations.ToString());
        pvText.SetText(pv);
        degatsText.SetText(""+ degats);
        cadenceText.SetText(""+ cadence.ToString("F2") + " s");
        porteeText.SetText(""+ portee);

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

    public static void ShowToolTip_Static(int ameliorations, List<int>element, string pv, float degats, float cadence, float portee)
    {
        TooltipCompletInstance.ShowToolTip(ameliorations, element,  pv, Mathf.FloorToInt(degats),  cadence,  portee);
    }

    public static void HideTooltip_Static()
    {
        TooltipCompletInstance.HideTooltip();
    }
    
}

