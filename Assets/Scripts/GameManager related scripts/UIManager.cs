using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    
    public TextMeshProUGUI alertDisplayText;
    public float alertDuration = 5f;
    public GameObject infoPanel; 
    private Camera mainCamera;
    public TextMeshProUGUI towerLife;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        infoPanel.SetActive(false);
        alertDisplayText.gameObject.SetActive(false);
    }

    public void DisplayAlert(string textToDisplay)
    {
        alertDisplayText.text = textToDisplay;
        alertDisplayText.gameObject.SetActive(true);
        StartCoroutine(TextIsDisplayed());
    }

    IEnumerator TextIsDisplayed()
    {
        yield return new WaitForSeconds(alertDuration);
        alertDisplayText.gameObject.SetActive(false);
    }
    
    public void ShowInfo(string info, Vector3 position)
    {
        towerLife.text = info;            // Définir le texte d'info
        infoPanel.transform.position = position; // Positionner le panneau à l'écran
        infoPanel.SetActive(true);       // Afficher le panneau d'info
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);      // Masquer le panneau d'info
    }

    
}
