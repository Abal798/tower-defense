using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActualizeChild : MonoBehaviour
{
    public TowerStats towerStats;

    private void Start()
    {
        towerStats = transform.GetChild(0).gameObject.GetComponent<TowerStats>();
    }

    private void OnMouseOver()
    {
        Debug.Log("fonctionne po");
        Tooltip.ShowToolTip_Static("C'est une tour <color=#ff0000>(11 septembre lol)</color>\nses stats dont :\n" + towerStats.damages + " de dégats\n" + towerStats.health + " pvs\n elle a actuellement " + towerStats.ameliorations.Count + " améliorations\n Ce sont : " + towerStats.ameliorations[0]);
    }

    private void OnMouseExit()
    {
        Tooltip.HideTooltip_Static();
    }

    public void AcutalizeChild()
    {
        Debug.Log("AcutalizeChild");
        towerStats.recalculateStats();
    }
}
