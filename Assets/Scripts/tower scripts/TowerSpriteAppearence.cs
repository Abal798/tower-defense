using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpriteAppearence : MonoBehaviour
{
    private Vector3 originalSize;
    private Vector3 targetSize;
    private bool isChangingSize = false;
    

    public void UpdateSize(float duration,float scaleFactor, Transform objectToChange)
    {
        if (!isChangingSize)
        {
            StartCoroutine(ScaleOverTime(duration, scaleFactor, objectToChange));
        }
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
}
