using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerStats : MonoBehaviour
{
    public int towerType;//temporaire
    public float radius;
    public float rotationSpeed;
    public List<int> ameliorations = new List<int>();
    public float bulletSpeed = 10f;
    
    public float basicCadence = 1f;
    public float cadence;
    
    public float basicDammage = 1f;
    public float damages;
    
    public float basicHealth = 1;
    public float health;

    private int nbrOfFireInsuflation;
    private int nbrOfWaterInsuflation;
    private int nbrOfEarthInsuflation;

    [Header("a renseigner par les GD")] 
    public float FireEffect = 10;
    public float waterEffectOne = 5;
    public float waterEffectTwo = 0.05f;
    public float waterLifeBonus = 5;

    public UnityEvent statsHasBeenRecalculated;
    


    void Start()
    {
        towerType = ameliorations[0];//temporaire
        recalculateStats();
    }
    
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void recalculateStats()
    {
        nbrOfFireInsuflation = 0;
        nbrOfWaterInsuflation = 0;
        nbrOfEarthInsuflation = 0;
        
        for (int i = 0; i < ameliorations.Count; i++)
        {
            if (ameliorations[i] == 1)
            {
                nbrOfFireInsuflation++;
            }
            else if (ameliorations[i] == 2)
            {
                nbrOfWaterInsuflation++;
            }
            else if (ameliorations[i] == 3)
            {
                nbrOfEarthInsuflation++;
            }
        }

        damages = (basicDammage + FireEffect * nbrOfFireInsuflation + waterEffectOne * nbrOfWaterInsuflation);//formule incomplete , manque l'influence du terrain
        health = basicHealth + waterLifeBonus;//incomplet , manque l'effet de terre , trop impréci pour l'instant
        cadence = basicCadence - waterEffectTwo * nbrOfWaterInsuflation;
        
        statsHasBeenRecalculated.Invoke();

    }
}
