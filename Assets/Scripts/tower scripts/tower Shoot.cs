using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerShoot : MonoBehaviour
{
    [Header("stats modifiables")]
    public float detectionRadius = 10f;
    public float rotationSpeed = 10f;
    
    [Header("a renseigner")]
    public GameObject bullet;
    public LayerMask ennemyLayer;
    public KeyCode towerShootKey;
    
    [Header("automatique , ne pas toucher")]
    public GameObject target;
    private bool targetDetected = false;

    void Update()
    {
        FindClosestTargetWithCircleCast();
        
        if (targetDetected && (target != null))
        {
            RotateTowardsTarget();
        }

        if (Input.GetKeyDown(towerShootKey))
        {
            Shoot();
        }
    }
    void FindClosestTargetWithCircleCast()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, ennemyLayer);
        
        if (hitColliders.Length == 0)
        {
            targetDetected = false;
            target = null;
            return;
        }

        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        
        foreach (Collider2D hitCollider in hitColliders)
        {
            float distanceToTarget = Vector2.Distance(transform.position, hitCollider.transform.position);
            if (distanceToTarget < shortestDistance)
            {
                shortestDistance = distanceToTarget;
                nearestTarget = hitCollider.gameObject;
            }
        }
        
        if (nearestTarget != null)
        {
            target = nearestTarget;
            targetDetected = true;
        }
    }
    
    void RotateTowardsTarget()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    void Shoot()
    {
        if (target != null)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<BulletBehaviour>().target = target;
            target.GetComponent<MonsterDeathBehaviour>().incomingDamage +=
                newBullet.GetComponent<BulletBehaviour>().dammage;
        }
    }
}
