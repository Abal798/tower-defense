using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetType : MonoBehaviour
{
    public int MyType;
    public TowerType TType;


    private void OnMouseDown()
    {
        TType.powerType = MyType;
        TType.isSelected = false;
    }
}
