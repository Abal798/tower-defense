using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;
    
    public GameObject[] paragraphs;
    
    public bool monsterSwarmerEncountered = false;
    public bool monsterGiantEncountered = false;
    

    private void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        paragraphs[0].gameObject.SetActive(monsterSwarmerEncountered); 
        paragraphs[1].gameObject.SetActive(monsterGiantEncountered); 
    }
    
    
}
