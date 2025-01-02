using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateThroughMenuPages : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>();
    private int index = 0;
    [SerializeField]private bool loop = false;
    public void OnEnable()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        pages[index].SetActive(true);
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
