using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricDiagram : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;
    public Transform pointMarker;
    

    public void CalculatePosition(int weightA, int weightB, int weightC)
    {
        int totalWeight = weightA + weightB + weightC;

        if (totalWeight > 0)
        {
            float a = (float)weightA / totalWeight;
            float b = (float)weightB / totalWeight;
            float c = (float)weightC / totalWeight;
            
            Vector3 barycentricPosition = a * pointA.position + b * pointB.position + c * pointC.position;
            pointMarker.position = barycentricPosition;
        }
    }
}
