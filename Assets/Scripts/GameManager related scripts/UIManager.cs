using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    
    public TextMeshProUGUI alertDisplayText;
    public float alertDuration = 5f;

    
    // Start is called before the first frame update
    void Start()
    {
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

}
