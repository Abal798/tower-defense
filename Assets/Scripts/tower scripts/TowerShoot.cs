using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    [Header("stats modifiables")]
    public float detectionRadius = 10f;
    public float rotationSpeed = 10f;
    public float dammage = 1f;
    public float bulletSpeed = 10f;

    [Header("a renseigner, les GD pas toucher")]
    public GameObject towerSprite;
    public GameObject basicBullet;

    public GameObject fireBullet;
    public GameObject iceBullet;
    public GameObject electricBullet;
    public LayerMask ennemyLayer;
    public KeyCode towerShootKey;
    public TowerType TType;

    [Header("automatique , ne pas toucher")]
    public GameObject towerTypeBullet;
    public GameObject target;
    private bool targetDetected = false;
    private int actualType;

    void Start()
    {
        towerTypeBullet = null;
    }

    void Update()
    {
        if(towerTypeBullet != null)
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
    }

    void FixedUpdate()
    {
        if (TType.powerType != actualType)
        {
            if (TType.powerType == 1)
            {
                actualType = 1;
                towerTypeBullet = fireBullet;
            }

            if (TType.powerType == 2)
            {
                actualType = 2;
                towerTypeBullet = electricBullet;
            }

            if (TType.powerType == 3)
            {
                actualType = 3;
                towerTypeBullet = iceBullet;
            }
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
        Vector2 direction = (target.transform.position - towerSprite.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        towerSprite.transform.rotation = Quaternion.Slerp(towerSprite.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
            GameObject newBullet = Instantiate(towerTypeBullet, transform.position, transform.rotation);
            newBullet.GetComponent<BulletBehaviour>().target = target;
            newBullet.GetComponent<BulletBehaviour>().dammage = dammage;
            newBullet.GetComponent<BulletBehaviour>().bulletSpeed = bulletSpeed;
            
            target.GetComponent<MonsterDeathBehaviour>().incomingDamage +=
                newBullet.GetComponent<BulletBehaviour>().dammage;
        }
    }
}
