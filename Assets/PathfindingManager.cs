using System;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    public delegate void PathResponseDelegate(List<Vector3Int> path);
    public delegate void PathStaleDelegate();

    
    private Queue<PathRequest> requestQueue = new Queue<PathRequest>();
    private List<PathRequest> activeRequests = new List<PathRequest>();
    
    [SerializeField] private int maxPathsPerFrame = 5;

    public void RequestPath(Vector3Int start, Vector3Int end, PathResponseDelegate onPathFound, PathStaleDelegate onPathStale)
    {
        PathRequest newRequest = new PathRequest(start, end, onPathFound, onPathStale);
        requestQueue.Enqueue(newRequest);
    }

    private void Update()
    {
        int pathsProcessed = 0;

        while (requestQueue.Count > 0 && pathsProcessed < maxPathsPerFrame)
        {
            PathRequest request = requestQueue.Dequeue();
            List<Vector3Int> path = CalculatePath(request.Start, request.End);
            
            if (path != null && path.Count > 0)
            {
                request.OnPathFound(path);
                activeRequests.Add(request);
            }
            else
            {
                request.OnPathFound(null); // No path found
            }

            pathsProcessed++;
        }
    }

    public void NotifyGridChange(Vector3Int gridLocation)
    {
        foreach (var request in activeRequests)
        {
            if (IsNearby(request.Start, gridLocation) || IsNearby(request.End, gridLocation))
            {
                request.OnPathStale();
            }
        }
    }

    private bool IsNearby(Vector3Int position, Vector3Int gridLocation)
    {
        return Vector3Int.Distance(position, gridLocation) < 5; // Adjust the threshold as needed
    }

    private List<Vector3Int> CalculatePath(Vector3Int start, Vector3Int end)
    {
        // Replace this with your pathfinding logic that returns an array of Vector3Int
        return PathfindingAlgorithm(start, end);
    }

    private List<Vector3Int> PathfindingAlgorithm(Vector3Int start, Vector3Int end)
    {
        // Dummy implementation for pathfinding
        return new List<Vector3Int> { start, end }; // Replace with actual logic
    }

    private class PathRequest
    {
        public Vector3Int Start;
        public Vector3Int End;
        public PathResponseDelegate OnPathFound;
        public PathStaleDelegate OnPathStale;

        public PathRequest(Vector3Int start, Vector3Int end, PathResponseDelegate onPathFound, PathStaleDelegate onPathStale)
        {
            Start = start;
            End = end;
            OnPathFound = onPathFound;
            OnPathStale = onPathStale;
        }
    }
}
