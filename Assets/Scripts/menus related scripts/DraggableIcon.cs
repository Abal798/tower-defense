using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;                // La référence au Canvas
    private RectTransform rectTransform; // Pour manipuler la position
    private CanvasGroup canvasGroup;     // Pour gérer la transparence ou les interactions
    private Transform originalParent;    // Stocke le parent original

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>(); // Trouve le Canvas parent
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Désactive les interactions avec les raycasts pour éviter des conflits
        canvasGroup.blocksRaycasts = false;

        // Stocke le parent original
        originalParent = transform.parent;

        // Amène l'objet au-dessus des autres pour faciliter le drag
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Déplace l'icône en fonction du pointeur de la souris
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Réactive les interactions avec les raycasts
        canvasGroup.blocksRaycasts = true;

        // Si l'objet n'est pas placé dans un slot, retourne à son parent d'origine
        if (transform.parent == canvas.transform)
        {
            transform.SetParent(originalParent);
            rectTransform.anchoredPosition = Vector2.zero; // Réinitialise la position locale
        }
    }
}