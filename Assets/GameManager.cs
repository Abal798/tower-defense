using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isInFstMode = false;
    public float accelerationFactor = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    

    // Update is called once per frame
    public void ChangeTimeScale()
    {
        isInFstMode = !isInFstMode;
        if (isInFstMode)
        {
            Time.timeScale = accelerationFactor;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
