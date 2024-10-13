using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovementBehaviourTemprary : MonoBehaviour
{

    public float monsterSpeed = 1;
    public GameObject target;

    void Update()
    {
        MoveTowardsTarget();
    }
    
    void MoveTowardsTarget()
    {
        Vector3 TargetDirection = new Vector3(transform.position.x - target.transform.position.x,
            transform.position.y - target.transform.position.y, 0).normalized;

        transform.position -= TargetDirection * monsterSpeed * Time.deltaTime;
    }
}
