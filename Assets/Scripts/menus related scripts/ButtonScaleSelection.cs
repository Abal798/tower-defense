using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScaleSelection : MonoBehaviour
{
    public void OnPointerEnter()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1f);
    }
    public void OnPointerEnter(Vector3 newScale)
    {
        transform.localScale = newScale;
    }
    public void OnPointerExit()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
