using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    

    [Header("a renseigner, les GD pas toucher")]
    public GameObject towerSprite;
    public GameObject basicBullet;
    public TowerStats TS;
    
    public LayerMask ennemyLayer;
    public KeyCode towerShootKey;

    [Header("automatique , ne pas toucher")]
    public GameObject target;
    public bool targetDetected = false;
    public int actualType;
    public float detectionRadius;
    public float rotationSpeed;
    public float dammage;
    public float bulletSpeed;
    public float cadence;
    
    
    void Start()
    {
        
        detectionRadius = TS.radius;
        rotationSpeed = TS.rotationSpeed;
        dammage = TS.dammage;
        bulletSpeed = TS.bulletSpeed;
        cadence = TS.cadence;
        StartCoroutine(ShootAtInterval());

    }
    
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

        
        if (TS.towerType != actualType)
        {
            if (TS.towerType == 1)
            {
                actualType = 1;
            }

            if (TS.towerType == 2)
            {
                actualType = 2;
            }

            if (TS.towerType == 3)
            {
                actualType = 3;
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
    
    

    void Shoot()
    {
        if (target != null)
        {
            
            GameObject newBullet = Instantiate(basicBullet, transform.position, transform.rotation);
            if (actualType == 1)
            {
                newBullet.gameObject.tag = "fire";
            }
            else if (actualType == 2)
            {
                newBullet.gameObject.tag = "water";
            }
            else if (actualType == 3)
            {
                newBullet.gameObject.tag = "earth";
            }
            
            newBullet.GetComponent<BulletBehaviour>().target = target;
            newBullet.GetComponent<BulletBehaviour>().dammage = dammage;
            newBullet.GetComponent<BulletBehaviour>().bulletSpeed = bulletSpeed;
            target.GetComponent<MonsterDeathBehaviour>().incomingDamage += dammage;
            
            Destroy(newBullet, 5f);
        }
    }
    
    IEnumerator ShootAtInterval()
    {
        
        while (true)
        {
            if (target != null)
            {
                
                Shoot();
            }
            
            
            yield return new WaitForSeconds(1f / cadence);
        }
    }
    
}
