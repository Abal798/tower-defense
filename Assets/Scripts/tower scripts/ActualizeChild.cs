using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActualizeChild : MonoBehaviour
{
    public TowerStats towerStats;
    public float tooltipDelay = 0.5f;
    private Coroutine tooltipCoroutine;
    

    private void Start()
    {
        towerStats = transform.GetChild(0).gameObject.GetComponent<TowerStats>();
    }

    private void OnMouseOver()
    {
        if(GridBuilding.current.tempEmpty() && (tooltipCoroutine == null))
        {
            tooltipCoroutine = StartCoroutine(ShowTooltipWithDelay());
        }
        
        //TooltipComplet.ShowToolTip_Static("C'est une tour <color=#ff0000>(11 septembre lol)</color>\nses stats dont :\n" + towerStats.damages + " de dégats\n" + towerStats.health + " pvs\n elle a actuellement " + towerStats.ameliorations.Count + " améliorations\n Ce sont : " + towerStats.ameliorations[0]);
    }

    private void OnMouseExit()
    {
        TooltipComplet.HideTooltip_Static();
        if (tooltipCoroutine != null)
        {
            StopCoroutine(tooltipCoroutine);
            tooltipCoroutine = null;
        }
    }


    private IEnumerator ShowTooltipWithDelay()
    {
        yield return new WaitForSeconds(tooltipDelay);
        
        TooltipComplet.ShowToolTip_Static(
            towerStats.ameliorations.Count,
            towerStats.ameliorations,
            towerStats.health + "/" + towerStats.maxHealth,
            towerStats.damages,
            towerStats.cadence,
            towerStats.radius
        );
        
        tooltipCoroutine = null;
    }
    
    
    
    public void AcutalizeChild()
    {
        Debug.Log("AcutalizeChild");
        towerStats.recalculateStats();
    }
}
