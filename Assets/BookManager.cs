using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;
    
    public GameObject[] paragraphs;
    public GameObject[] hidedParagraphes;

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
    }

    public void ShowParagraph(int monsterType)
    {
        monsterType -= 1;
        paragraphs[monsterType].SetActive(true);
        hidedParagraphes[monsterType].SetActive(false);

        if (monsterType == 1) monsterOneSaw = true;
        if (monsterType == 2) monsterTwoSaw = true;
        if (monsterType == 3) monsterThreeSaw = true;
        if (monsterType == 4) monsterFourSaw = true;
        if (monsterType == 5) monsterFiveSaw = true;
        
        PlayerPrefs.SetInt("monsterOneSaw", (monsterOneSaw ? 1 : 0));
        PlayerPrefs.SetInt("monsterTwoSaw", (monsterTwoSaw ? 1 : 0));
        PlayerPrefs.SetInt("monsterThreeSaw", (monsterThreeSaw ? 1 : 0));
        PlayerPrefs.SetInt("monsterFourSaw", (monsterFourSaw ? 1 : 0));
        PlayerPrefs.SetInt("monsterFiveSaw", (monsterFiveSaw ? 1 : 0));
    }
}
