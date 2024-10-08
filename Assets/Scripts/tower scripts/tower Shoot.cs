using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerShoot : MonoBehaviour
{
    public float detectionRadius = 10f;
    public LayerMask targetLayer;
    public float rotationSpeed = 10f;

    private GameObject target;
    private bool targetDetected = false;

    void Update()
    {
        FindClosestTargetWithCircleCast();

        // Si une cible est détectée, oriente le GameObject vers cette cible
        if (targetDetected && (target != null))
        {
            RotateTowardsTarget();
        }
    }
    void FindClosestTargetWithCircleCast()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, targetLayer);
        
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
        // Calcule la direction vers la cible
        Vector2 direction = (target.transform.position - transform.position).normalized;

        // Calcule l'angle nécessaire pour se tourner vers la cible
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Applique la rotation progressivement vers la cible avec une interpolation
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Pour afficher visuellement le rayon de détection dans l'éditeur Unity
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
