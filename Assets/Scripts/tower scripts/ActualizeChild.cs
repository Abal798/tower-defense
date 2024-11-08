using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizeChild : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AcutalizeChild()
    {
        Debug.Log("AcutalizeChild");
        transform.GetChild(0).gameObject.GetComponent<TowerStats>().recalculateStats();
    }
}
