using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public static Tooltip TooltipInstance { get; private set;  }
    
    [SerializeField] private RectTransform canvasRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private void Awake()
    {
        TooltipInstance = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        rectTransform = transform.GetComponent<RectTransform>();
        
        HideTooltip();
    }

    private void SetText(string tooltipString)
    {
        textMeshPro.SetText(tooltipString);
        textMeshPro.ForceMeshUpdate();
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + paddingSize;
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

    private void ShowToolTip(string tooltipText)
    {
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowToolTip_Static(string tooltipText)
    {
        TooltipInstance.ShowToolTip(tooltipText);
    }

    public static void HideTooltip_Static()
    {
        TooltipInstance.HideTooltip();
    }
    
}
