using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInfoOnHover : MonoBehaviour
{
    public string infoText = "Informations sur cet objet"; // Texte à afficher
    private UIManager infoPanelManager;

    private void Start()
    {
        infoPanelManager = FindObjectOfType<UIManager>(); // Référence au InfoPanelManager
    }

    private void OnMouseEnter()
    {
        Debug.Log("la sourie est au dessus de moi");
        if (infoPanelManager != null)
        {
            infoPanelManager.ShowInfo(infoText, transform.position);
        }
    }

    private void OnMouseExit()
    {
        if (infoPanelManager != null)
        {
            infoPanelManager.HideInfo();
        }
    }
}