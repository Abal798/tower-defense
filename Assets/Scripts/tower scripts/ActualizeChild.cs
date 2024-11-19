using System;
using System.Collections;
using UnityEngine;

public class ActualizeChild : MonoBehaviour
{
    public TowerStats towerStats;
    public float tooltipDelay = 0.5f;
    private Coroutine tooltipCoroutine;
    private Coroutine extendedTooltipCoroutine;

    private void Start()
    {
        towerStats = transform.GetChild(0).gameObject.GetComponent<TowerStats>();
    }

    private void OnMouseOver()
    {
        // If Shift is pressed, immediately show the extended tooltip
        if (GridBuilding.current.tempEmpty())
        {
            // If the Shift key is held, show the extended tooltip
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                // Cancel any existing tooltip and show the extended tooltip
                StopCurrentTooltipCoroutine();
                TooltipComplet.HideTooltip_Static();
                extendedTooltipCoroutine = StartCoroutine(ShowExtendedTooltip());
            }
            else
            {
                // If Shift is not pressed, show the basic tooltip after a delay
                if (tooltipCoroutine == null)  // Only start the coroutine if it's not already running
                {
                    tooltipCoroutine = StartCoroutine(ShowTooltipWithDelay());
                }
            }
        }
    }

    private void OnMouseExit()
    {
        // Hide tooltips when mouse exits the tower area
        TooltipExtented.HideTooltip_Static();
        TooltipComplet.HideTooltip_Static();
        StopCurrentTooltipCoroutine();
    }

    private void StopCurrentTooltipCoroutine()
    {
        // Cancel any active tooltips
        if (tooltipCoroutine != null)
        {
            StopCoroutine(tooltipCoroutine);
            tooltipCoroutine = null;
        }

        if (extendedTooltipCoroutine != null)
        {
            StopCoroutine(extendedTooltipCoroutine);
            extendedTooltipCoroutine = null;
        }
    }

    private IEnumerator ShowTooltipWithDelay()
    {
        // Wait before showing the basic tooltip
        yield return new WaitForSeconds(tooltipDelay);

        // Only show basic tooltip if Shift is not pressed
        if (!(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            TooltipExtented.HideTooltip_Static();
            TooltipComplet.ShowToolTip_Static(
                towerStats.ameliorations.Count,
                towerStats.ameliorations,
                Mathf.RoundToInt(towerStats.health) + "/" + Mathf.RoundToInt(towerStats.maxHealth),
                towerStats.damages,
                towerStats.cadence,
                towerStats.radius
            );
        }

        tooltipCoroutine = null;
    }

    private IEnumerator ShowExtendedTooltip()
    {
        // Show the extended tooltip immediately
        TooltipExtented.ShowToolTip_Static(
            towerStats.ameliorations.Count,
            towerStats.ameliorations,
            Mathf.RoundToInt(towerStats.health) + "/" + Mathf.RoundToInt(towerStats.maxHealth),
            towerStats.damages,
            towerStats.cadence,
            towerStats.radius,
            towerStats.fireSurrounding,
            towerStats.waterSurrouding,
            towerStats.earthSurrounding
        );

        extendedTooltipCoroutine = null;
        yield break;
    }

    public void AcutalizeChild() // Fixed method name
    {
        towerStats.recalculateStats();
    }

    private void OnDestroy()
    {
        StopCurrentTooltipCoroutine();
        GridBuilding.current.listeTowerCo.Remove(Vector3Int.FloorToInt(transform.position));
    }

    public void HealTower()
    {
        towerStats.Heal();
    }
}

