using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateThroughMenuPages : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>();
    private int index = 0;
    public bool loop = false;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        pages[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        if (index < pages.Count - 1)
        {
            pages[index].SetActive(false);
            index++;
            pages[index].SetActive(true);
        }
        else if(loop)
        {
            pages[index].SetActive(false);
            index = 0;
            pages[index].SetActive(true);
        }
    }
    
    public void PreviousPage()
    {
        if (index > 0)
        {
            pages[index].SetActive(false);
            index--;
            pages[index].SetActive(true);
        }
        else if(loop)
        {
            pages[index].SetActive(false);
            index = pages.Count - 1;
            pages[index].SetActive(true);
        }
    }
}
