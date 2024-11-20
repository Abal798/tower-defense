using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarycentricDiagram : MonoBehaviour
{
    public RectTransform pointA;
    public RectTransform pointB;
    public RectTransform pointC;
    public RectTransform pointMarker;
    public int influenceFactor = 20;


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
            int minValue = Mathf.Min(weightA, weightB, weightC);
            weightA = (weightA - minValue )+ influenceFactor;
            weightB = (weightB - minValue )+ influenceFactor;
            weightC = (weightC - minValue )+ influenceFactor;
            
            totalWeight = weightA + weightB + weightC;
            
            float a = (float)weightA / totalWeight;
            float b = (float)weightB / totalWeight;
            float c = (float)weightC / totalWeight;

            Vector3 barycentricPosition = a * pointA.anchoredPosition + b * pointB.anchoredPosition + c * pointC.anchoredPosition;
            pointMarker.anchoredPosition = barycentricPosition;

        }
        else
        {
            Debug.Log("Total weight is zero; unable to calculate position.");
        }
    }
}
