using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager UIM;
    public TextMeshProUGUI alertDisplayText;
    public float alertDuration = 5f;

    private void Awake()
    {
        UIM = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        alertDisplayText.gameObject.SetActive(false);
    }

    public void DisplayAlert(string textToDisplay)
    {
        alertDisplayText.text = textToDisplay;
        alertDisplayText.gameObject.SetActive(true);
        StartCoroutine(TextIsDisplayed());
    }

    private IEnumerator TextIsDisplayed()
    {
        yield return new WaitForSeconds(alertDuration);
        alertDisplayText.gameObject.SetActive(false);
    }

}
