using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isInFstMode = false;
    public float accelerationFactor = 3f;
    public Sprite[] fastForwardSprites;
    
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    

    // Update is called once per frame
    public void ChangeTimeScale(Button fastForwardButton)
    {
        isInFstMode = !isInFstMode;
        if (isInFstMode)
        {
            fastForwardButton.image.sprite = fastForwardSprites[1];
            Time.timeScale = accelerationFactor;
        }
        else
        {
            fastForwardButton.image.sprite = fastForwardSprites[0];
            Time.timeScale = 1f;
        }
    }
}
