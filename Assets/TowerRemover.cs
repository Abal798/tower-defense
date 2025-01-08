using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerRemover : MonoBehaviour
{
    
    private bool isDragging = true;
    private GameObject overlayRemover;
    public float soulRecycling;
    public RessourcesManager RM;

    private void Update()
    {
        if (isDragging && (overlayRemover != null))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            overlayRemover.transform.position = new Vector3(SnapToGrid(mousePosition).x + 0.5f,
                SnapToGrid(mousePosition).y + 0.5f, mousePosition.z);

            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int pos = (Vector3Int.FloorToInt(SnapToGrid(mousePosition)));
                if (GridBuilding.current.listeTowerCo.ContainsKey(pos))
                {
                    foreach (int ameliorationType in GridBuilding.current.listeTowerCo[pos].transform.GetChild(0).gameObject.GetComponent<TowerStats>().ameliorations)
                    {
                        if (ameliorationType == 1) RM.fireSoul += soulRecycling;
                        else if (ameliorationType == 2) RM.waterSoul += soulRecycling;
                        else if (ameliorationType == 3) RM.plantSoul += soulRecycling;
                    }
                    
                    
                    Destroy(GridBuilding.current.listeTowerCo[pos]);
                    GridBuilding.current.listeTowerCo.Remove(pos);
                    GridBuilding.current.MainTilemap.SetTile(pos, GridBuilding.tileBases[TileType.Grass]);

                }

                Destroy(overlayRemover);

            }


            if (Input.GetMouseButtonDown(1))
            {
                Destroy(overlayRemover);
            }

        }

    }

    public void CreateOverlayRemover(GameObject overlayRemoverGameObject)
    {
        overlayRemover = Instantiate(overlayRemoverGameObject, Input.mousePosition, Quaternion.identity);

    }
    private Vector3 SnapToGrid(Vector3 position)
    {
       
        Vector3Int cellPosition = GridBuilding.current.gridLayout.WorldToCell(position);
        return GridBuilding.current.gridLayout.CellToWorld(cellPosition);
    }
    
   
}
