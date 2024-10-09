using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFireType : MonoBehaviour
{
    public bool IsSelected = false;
    public int MyType;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            IsSelected = true;
        }
    }
}
