using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public static PathfindingManager PathfindingManagerInstance;
    private List<List<Vector3Int>> paths = new List<List<Vector3Int>>();
    private List<GameObject> monstres;
    public Pathfinding pathfinding;

    private void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
    }

    public void PathAddToList(List<Vector3Int> path, GameObject monstre)
    {
        paths.Add(path);
        monstres.Add(monstre);
    }

    public List<Vector3Int> GetPath(Vector3Int startCellPosition, GameObject monstre)
    {
        List<Vector3Int> path = pathfinding.PathfindingCalculation(startCellPosition, new Vector3Int(0, 0, 0), false);
        PathAddToList(path, monstre);

        return path;
    }
}
