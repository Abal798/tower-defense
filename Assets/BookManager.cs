using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager instance;
    
    public GameObject[] paragraphs;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
