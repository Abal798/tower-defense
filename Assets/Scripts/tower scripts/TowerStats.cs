using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour
{

    public int towerType;
    public float radius;
    public float rotationSpeed;
    public float dammage = 1f;
    public float bulletSpeed = 10f;
    public float cadence = 2f;
    public List<int> ameliorations = new List<int>();
    

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
