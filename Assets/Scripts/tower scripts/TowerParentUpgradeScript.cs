using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerParentUpgradeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MustUpgradeChild()
    {
        transform.GetChild(0).gameObject.GetComponent<TowerStats>().recalculateStats();
    }
}
