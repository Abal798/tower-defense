using System;
using System.Collections;
using UnityEngine;

public class ActualizeChild : MonoBehaviour
{
    public TowerStats towerStats;
    
    private void Start()
    {
        towerStats = transform.GetChild(0).gameObject.GetComponent<TowerStats>();
    }
    
    public void AcutalizeChild() // Fixed method name
    {
        if (towerStats != null) towerStats.recalculateStats();
    }

    private void OnDestroy()
    {
        GridBuilding.current.listeTowerCo.Remove(Vector3Int.FloorToInt(transform.position));
    }

    public void HealTower()
    {
        towerStats.Heal();
    }

    private void OnMouseDown()
    {
        if (MenuManager.activePanel.name != "IngamePanel") return;
        if (gameObject.CompareTag("Tower") && GridBuilding.current.tempEmpty())
        {
            SelectTower();
        }
    }

    public void SelectTower()
    {
        transform.GetChild(1).gameObject.SetActive(!transform.GetChild(1).gameObject.activeSelf);
        if (transform.GetChild(1).gameObject.activeSelf) AudioManager.AM.PlaySfx(AudioManager.AM.towerSelect);
    }

    public void UnSelectTower()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
}

