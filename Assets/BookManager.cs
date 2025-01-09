using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;
    
    public GameObject[] paragraphs;
    public GameObject[] hidedParagraphes;

    private void Awake()
    {
        instance = this;
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
    }
}
