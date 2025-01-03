using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    private Grid g_grid;

    private void Start()
    {
        g_grid = Grid.FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3Int cp = g_grid.LocalToCell(transform.localPosition);
        transform.localPosition = g_grid.GetCellCenterLocal(cp);
    }
}
