using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{
    public GameObject grid;//temporaire
    public int towerType;//temporaire
    public float radius;
    public float rotationSpeed;
    public float dammage = 1f;
    public float bulletSpeed = 10f;
    public float cadence = 2f;
    public List<int> ameliorations = new List<int>();


    void Start()
    {
        grid = FindObjectOfType<GridBuilding>().gameObject;//temporaire
        towerType = grid.GetComponent<GridBuilding>().elementTour;//temporaire
    }
    
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
