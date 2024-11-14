using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricDiagram : MonoBehaviour
{
    public RectTransform pointA;
    public RectTransform pointB;
    public RectTransform pointC;
    public RectTransform pointMarker;


    public void CalculatePosition(int weightA, int weightB, int weightC)
    {
        if (pointA == null || pointB == null || pointC == null || pointMarker == null)
        {
            Debug.Log("One or more required RectTransforms or components are not assigned.");
            return;
        }

        int totalWeight = weightA + weightB + weightC;

        if (totalWeight > 0)
        {
            float a = (float)weightA / totalWeight;
            float b = (float)weightB / totalWeight;
            float c = (float)weightC / totalWeight;

            Vector3 barycentricPosition =
                a * pointA.anchoredPosition + b * pointB.anchoredPosition + c * pointC.anchoredPosition;
            pointMarker.anchoredPosition = barycentricPosition;

        }
        else
        {
            Debug.Log("Total weight is zero; unable to calculate position.");
        }
    }
}
