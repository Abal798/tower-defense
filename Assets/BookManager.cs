using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;
    
    public GameObject[] paragraphs;

    public void Start()
    {
        foreach (var paragraphes in paragraphs)
        {
            paragraphes.SetActive(true);
        }
    }
}
