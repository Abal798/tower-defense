using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI TooltipUIInstance { get; private set;  }
    
    [SerializeField] private RectTransform canvasRectTransform;
    
    private TextMeshProUGUI textUI;
    private TextMeshProUGUI titreUI;
    
        
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    
    private void Awake()
    {
        TooltipUIInstance = this;
        backgroundRectTransform = transform.Find("BackgroundComplet").GetComponent<RectTransform>();
        
        textUI = transform.Find("TextUI").GetComponent<TextMeshProUGUI>();
        titreUI = transform.Find("TitreUI").GetComponent<TextMeshProUGUI>();
        
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

    private void ShowToolTip(string title, string text)
    {
        gameObject.SetActive(true);
        textUI.SetText(text);
        titreUI.SetText(title);
        
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowToolTip_Static(string title, string text)
    {
        TooltipUIInstance.ShowToolTip(title, text);
    }

    public static void HideTooltip_Static()
    {
        TooltipUIInstance.HideTooltip();
    }
}
