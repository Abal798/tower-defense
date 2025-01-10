using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;
    
    public GameObject[] paragraphs;
    public GameObject[] hidedParagraphes;
    public GameObject notification;

    public bool monsterOneSaw, monsterTwoSaw, monsterThreeSaw, monsterFourSaw, monsterFiveSaw;

    private void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("monsterOneSaw")) monsterOneSaw = (PlayerPrefs.GetInt("monsterOneSaw") != 0);
        if (PlayerPrefs.HasKey("monsterTwoSaw")) monsterTwoSaw = (PlayerPrefs.GetInt("monsterTwoSaw") != 0);
        if (PlayerPrefs.HasKey("monsterThreeSaw")) monsterThreeSaw = (PlayerPrefs.GetInt("monsterThreeSaw") != 0);
        if (PlayerPrefs.HasKey("monsterFourSaw")) monsterFourSaw = (PlayerPrefs.GetInt("monsterFourSaw") != 0);
        if (PlayerPrefs.HasKey("monsterFiveSaw")) monsterFiveSaw = (PlayerPrefs.GetInt("monsterFiveSaw") != 0);
    }

    public void Start()
    {
        for (int i = 0; i < paragraphs.Length; i++)
        {
            paragraphs[i].SetActive(false);
            hidedParagraphes[i].SetActive(true);
        }
        
        if(monsterOneSaw)  ShowParagraph(1);
        if(monsterTwoSaw)  ShowParagraph(2);
        if(monsterThreeSaw)  ShowParagraph(3);
        if(monsterFourSaw)  ShowParagraph(4);
        if(monsterFiveSaw)  ShowParagraph(5);
        
        notification.SetActive(false);
        
        
    }

    public void ShowParagraph(int monsterType)
    {
        monsterType -= 1;
        paragraphs[monsterType].SetActive(true);
        hidedParagraphes[monsterType].SetActive(false);

        if (monsterType == 1 && monsterOneSaw == false)
        {
            ShowNotification();
            monsterOneSaw = true;
        }
        if (monsterType == 2 && monsterTwoSaw == false)
        {
            ShowNotification();
            monsterTwoSaw = true;
        }
        if (monsterType == 3 && monsterThreeSaw == false)
        {
            ShowNotification();
            monsterThreeSaw = true;
        }
        if (monsterType == 4 && monsterFourSaw == false)
        {
            ShowNotification();
            monsterFourSaw = true;
        }
        if (monsterType == 5 && monsterFiveSaw == false)
        {
            ShowNotification();
            monsterFiveSaw = true;
        }
        
        PlayerPrefs.SetInt("monsterOneSaw", (monsterOneSaw ? 1 : 0));
        PlayerPrefs.SetInt("monsterTwoSaw", (monsterTwoSaw ? 1 : 0));
        PlayerPrefs.SetInt("monsterThreeSaw", (monsterThreeSaw ? 1 : 0));
        PlayerPrefs.SetInt("monsterFourSaw", (monsterFourSaw ? 1 : 0));
        PlayerPrefs.SetInt("monsterFiveSaw", (monsterFiveSaw ? 1 : 0));
    }

    public void ShowNotification()
    {
        notification.SetActive(true);
    }

    public void HideNotification()
    {
        notification.SetActive(false);
    }

    public void ResetBook()
    {
        PlayerPrefs.DeleteKey("monsterOneSaw");
        PlayerPrefs.DeleteKey("monsterTwoSaw");
        PlayerPrefs.DeleteKey("monsterThreeSaw");
        PlayerPrefs.DeleteKey("monsterFourSaw");
        PlayerPrefs.DeleteKey("monsterFiveSaw");

        monsterOneSaw = false;
        monsterTwoSaw = false;
        monsterThreeSaw = false;
        monsterFourSaw = false;
        monsterFiveSaw = false;
        
        for (int i = 0; i < paragraphs.Length; i++)
        {
            paragraphs[i].SetActive(false);
            hidedParagraphes[i].SetActive(true);
        }
    }
}
