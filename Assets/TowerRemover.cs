using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRemover : MonoBehaviour
{
    private bool isDragging = true;
    private GameObject overlayRemover;
    private void Update()
    {
        if (isDragging && (overlayRemover != null))
        { 
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            overlayRemover.transform.position = new Vector3(SnapToGrid(mousePosition).x + 0.5f, SnapToGrid(mousePosition).y + 0.5f, mousePosition.z);

            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int pos = (Vector3Int.FloorToInt(SnapToGrid(mousePosition)));
                if(GridBuilding.current.listeTowerCo.ContainsKey(pos))
                {
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

    private Vector3 SnapToGrid(Vector3 position)
    {
       
        Vector3Int cellPosition = GridBuilding.current.gridLayout.WorldToCell(position);
        return GridBuilding.current.gridLayout.CellToWorld(cellPosition);
    }

    public void CreateOverlayRemover(GameObject overlayRemoverGameObject)
    {
        overlayRemover = Instantiate(overlayRemoverGameObject, Input.mousePosition, Quaternion.identity);
    }
}
