using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    private bool isDragging = true;

    #region Build Methods

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuilding.current.gridLayout.LocalToCell((transform.position));
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuilding.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuilding.current.gridLayout.LocalToCell((transform.position));
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        Placed = true; 
        isDragging = false;
        GridBuilding.current.TakeArea(areaTemp);
        
    }

    #endregion
    

    private void Update()
    {
        if (isDragging)
        {
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;  
            
            SnapToGrid(mousePosition);
            SnapToGrid(mousePosition);
        }
    }

    private void SnapToGrid(Vector3 position)
    {
       
        Vector3Int cellPosition = GridBuilding.current.gridLayout.WorldToCell(position);

        
        transform.position = GridBuilding.current.gridLayout.CellToWorld(cellPosition);
    }

    
    private void SnapToGrid()
    {
        Vector3Int cellPosition = GridBuilding.current.gridLayout.WorldToCell(transform.position);
        transform.position = GridBuilding.current.gridLayout.CellToWorld(cellPosition);
    }
}
