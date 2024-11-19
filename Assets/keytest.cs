using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keytest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(FindObjectOfType<KeyRebinding>().GetKeyForAction("test")))
        {
            Debug.Log("Player tested!");
        }
    }
}
