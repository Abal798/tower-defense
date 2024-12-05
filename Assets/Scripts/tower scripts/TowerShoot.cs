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

    [Header("automatique , ne pas toucher")]
    public GameObject target;
    public bool targetDetected = false;
    public float detectionRadius;
    public float rotationSpeed;
    public float dammage;
    public float bulletSpeed;
    public float cadence;
    
    
    void Start()
    {
        UpdateStats();
        StartCoroutine(ShootAtInterval());
    }

    public void UpdateStats()
    {
        detectionRadius = TS.radius;
        rotationSpeed = TS.rotationSpeed;
        dammage = TS.damages;
        bulletSpeed = TS.bulletSpeed;
        cadence = TS.cadence;
        detectionRadius = TS.radius;
    }
    
    void Update()
    {

        FindClosestTargetWithCircleCast();
    
        if (targetDetected && (target != null))
        {
            RotateTowardsTarget();
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
            Vector3 originalSize = newBullet.transform.localScale;
            Vector3 targetScale = originalSize * (Mathf.Log(dammage * 0.07f) * 0.5f);
            newBullet.transform.localScale = targetScale;
            newBullet.GetComponent<BulletBehaviour>().bulletElements = TS.ameliorations;
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
            yield return new WaitForSeconds(cadence);
        }
    }
    
}
