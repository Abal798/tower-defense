using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerRemover : MonoBehaviour
{

    private void Start()
    {
        if (gameObject.CompareTag("Tower"))
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        
    }

    private void OnMouseDown()
    {
        if (MenuManager.activePanel.name != "IngamePanel") return;  
        if (gameObject.CompareTag("Tower") && GridBuilding.current.tempEmpty())
        {
            SelectTower();
        }
        

        if (!gameObject.CompareTag("Tower")&& GridBuilding.current.tempEmpty())
        {
            GridBuilding.current.MainTilemap.SetTile(Vector3Int.FloorToInt(transform.parent.position), GridBuilding.tileBases[TileType.Grass]);
            GridBuilding.current.listeTowerCo.Remove(Vector3Int.FloorToInt(transform.parent.position));
            Destroy(transform.parent.gameObject);
            
        }
    }

    public void SelectTower()
    {
        transform.GetChild(1).gameObject.SetActive(!transform.GetChild(1).gameObject.activeSelf);
        transform.GetChild(2).gameObject.SetActive(!transform.GetChild(2).gameObject.activeSelf);
    }
    
    public void UnSelectTower()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }
    
    
}
