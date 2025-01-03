using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakingDamagesEffect : MonoBehaviour
{
    
    public float destroyTime = 0.5f;
    public Vector3 offset = new Vector3(0f, 1f, 0f);
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }
}
