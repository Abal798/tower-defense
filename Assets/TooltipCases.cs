using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TooltipCases : MonoBehaviour
{
    public static TooltipCases TooltipCasesInstance { get; private set;  }
    
    [SerializeField] private RectTransform canvasRectTransform;
    
    private TextMeshProUGUI textCase;
    
        
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    
    private void Awake()
    {
        TooltipCasesInstance = this;
        backgroundRectTransform = transform.Find("BackgroundComplet").GetComponent<RectTransform>();
        
        textCase = transform.Find("TextCases").GetComponent<TextMeshProUGUI>();
        
        rectTransform = transform.GetComponent<RectTransform>();
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

    private void ShowToolTip(string text)
    {
        gameObject.SetActive(true);
        textCase.SetText(text);
        
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowToolTip_Static(string text)
    {
        TooltipCasesInstance.ShowToolTip(text);
    }

    public static void HideTooltip_Static()
    {
        TooltipCasesInstance.HideTooltip();
    }
    
}

