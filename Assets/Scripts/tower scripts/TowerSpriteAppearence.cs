using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpriteAppearence : MonoBehaviour
{
    private Vector3 originalSize;
    private Vector3 targetSize;
    private bool isChangingSize = false;
    
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public int currentSprite;


    public void Start()
    { 
        ChangeSprite();
    }
    
    
    public void UpdateSize(float duration,float scaleFactor, Transform objectToChange)
    {
        if (!isChangingSize)
        {
            StartCoroutine(ScaleOverTime(duration, scaleFactor, objectToChange));
        }
        ChangeSprite();
    }

    IEnumerator ScaleOverTime(float time, float scaleFactor,Transform objectToChange)
    {
        originalSize = objectToChange.localScale;
        targetSize = originalSize * scaleFactor;
        
        isChangingSize = true;
        Vector3 initialScale = objectToChange.localScale;
        Vector3 targetScale = initialScale * scaleFactor;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            objectToChange.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objectToChange.localScale = targetScale;
        isChangingSize = false;
    }

    void ChangeSprite()
    {
        List<int> currentAmeliration = transform.GetComponentInParent<TowerStats>().ameliorations;
        if (currentAmeliration.Count == 1)
        {
            currentSprite = currentAmeliration[0];
        }
        else if (currentAmeliration.Count == 2)
        {
            if (currentAmeliration[0] == 1)
            {
                if(currentAmeliration[1] == 1)
                {
                    currentSprite = 4;
                }
                else if (currentAmeliration[1] == 2)
                {
                    currentSprite = 5;
                }
                else if (currentAmeliration[1] == 3)
                {
                    currentSprite = 6;
                }
            }
            if (currentAmeliration[0] == 2)
            {
                if(currentAmeliration[1] == 1)
                {
                    currentSprite = 5;
                }
                else if (currentAmeliration[1] == 2)
                {
                    currentSprite = 7;
                }
                else if (currentAmeliration[1] == 3)
                {
                    currentSprite = 8;
                }
            }
            if (currentAmeliration[0] == 3)
            {
                if(currentAmeliration[1] == 1)
                {
                    currentSprite = 6;
                }
                else if (currentAmeliration[1] == 2)
                {
                    currentSprite = 8;
                }
                else if (currentAmeliration[1] == 3)
                {
                    currentSprite = 9;
                }
            }
        }
        else if (currentAmeliration.Count == 3)
        {
            if (currentAmeliration[0] == 1)
            {
                if(currentAmeliration[1] == 1)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 16;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite = 17;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 13;
                    }
                }
                else if (currentAmeliration[1] == 2)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 17;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite = 18;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 15;
                    }
                }
                else if (currentAmeliration[1] == 3)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 13;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite =15;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 11;
                    }
                }
            }
            if (currentAmeliration[0] == 2)
            {
                if(currentAmeliration[1] == 1)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 17;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite = 18;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 15;
                    }
                }
                else if (currentAmeliration[1] == 2)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 18;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite = 19;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 15;
                    }
                }
                else if (currentAmeliration[1] == 3)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 15;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite =14;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 12;
                    }
                }
            }
            if (currentAmeliration[0] == 3)
            {
                if(currentAmeliration[1] == 1)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 13;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite = 15;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 11;
                    }
                }
                else if (currentAmeliration[1] == 2)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 15;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite = 14;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 12;
                    }
                }
                else if (currentAmeliration[1] == 3)
                {
                    if(currentAmeliration[2] == 1)
                    {
                        currentSprite = 11;
                    }
                    else if (currentAmeliration[2] == 2)
                    {
                        currentSprite =12;
                    }
                    else if (currentAmeliration[2] == 3)
                    {
                        currentSprite = 10;
                    }
                }
            }
        }
        else
        {
            currentSprite = 0;
        }

        spriteRenderer.sprite = spriteArray[currentSprite];
    }
}
