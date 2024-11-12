using UnityEngine;
using UnityEngine.EventSystems;

public class UISlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // Vérifie si l'objet déplacé est un icône avec le script DraggableIcon
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null && draggedObject.GetComponent<DraggableIcon>())
        {
            // Change le parent de l'icône pour qu'il corresponde au slot
            draggedObject.transform.SetParent(transform);

            // Réinitialise la position locale dans le slot
            draggedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}