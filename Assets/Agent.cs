using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private PathfindingManager pather;

    private Vector3Int currentTarget;
    private List<Vector3Int> currentPath;
    private bool pathIsStale = false;

    private void Start()
    {
        pather = FindObjectOfType<PathfindingManager>();
    }

    public void RequestPath(Vector3Int target)
    {
        currentTarget = target;
        pather.RequestPath(Vector3Int.FloorToInt(transform.position) , target, OnPathFound, OnPathStale);
    }

    private void OnPathFound(List<Vector3Int> path)
    {
        if (path != null)
        {
            currentPath = path;
            pathIsStale = false; // Reset stale flag if a new path is found
        }
        else
        {
            // Handle no path found
            Debug.Log("No path found.");
        }
    }

    private void OnPathStale()
    {
        pathIsStale = true;
        Debug.Log("Your path is potentially stale. Requesting a new path.");
        RequestPath(currentTarget); // Optionally request a new path immediately
    }

    private void Update()
    {
        if (pathIsStale)
        {
            // Handle stale path logic, like stopping movement
        }

        // Handle movement along the current path if it's valid
        if (currentPath != null)
        {
            MoveAlongPath();
        }
    }

    private void MoveAlongPath()
    {
        // Implement movement logic using currentPath
    }
}