using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HierarchicalPathfinding : MonoBehaviour
{
    private int[,] regionMap; // Maps each tile to a region ID
    private Dictionary<int, List<int>> regionGraph; // Adjacency list for high-level regions

    [SerializeField] private int regionSize = 10; // Size of each region (e.g., 10x10 tiles)

    private void Start()
    {
        GenerateRegionMap();
        BuildRegionGraph();
    }

    private void GenerateRegionMap()
    {
        Tilemap tilemap = TilemapManager.TilemapInstance.tilemap;

        Vector3Int boundsMin = tilemap.cellBounds.min;
        Vector3Int boundsMax = tilemap.cellBounds.max;

        int width = boundsMax.x - boundsMin.x;
        int height = boundsMax.y - boundsMin.y;

        regionMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                regionMap[x, y] = CalculateRegionID(boundsMin.x + x, boundsMin.y + y);
            }
        }
    }

    private void BuildRegionGraph()
    {
        regionGraph = new Dictionary<int, List<int>>();

        // Analyze regions for adjacency
        for (int x = 0; x < regionMap.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < regionMap.GetLength(1) - 1; y++)
            {
                int currentRegion = regionMap[x, y];
                AddEdge(currentRegion, regionMap[x + 1, y]);
                AddEdge(currentRegion, regionMap[x, y + 1]);
            }
        }
    }

    private void AddEdge(int regionA, int regionB)
    {
        if (regionA == regionB) return;

        if (!regionGraph.ContainsKey(regionA)) regionGraph[regionA] = new List<int>();
        if (!regionGraph[regionA].Contains(regionB)) regionGraph[regionA].Add(regionB);

        if (!regionGraph.ContainsKey(regionB)) regionGraph[regionB] = new List<int>();
        if (!regionGraph[regionB].Contains(regionA)) regionGraph[regionB].Add(regionA);
    }

    public int CalculateRegionID(int x, int y)
    {
        return (x / regionSize) + (y / regionSize) * 1000; // Unique region ID
    }

    public List<int> HighLevelPath(int startRegion, int targetRegion)
    {
        return AStarRegionPathfinding(startRegion, targetRegion);
    }

    private List<int> AStarRegionPathfinding(int startRegion, int targetRegion)
{
    // Check if regionGraph is initialized
    if (regionGraph == null)
    {
        Debug.LogError("Region Graph is not initialized!");
        return new List<int>();  // Return an empty path if the graph is null
    }

    Debug.Log("Starting A* Pathfinding from region " + startRegion + " to region " + targetRegion);

    // Check if the current region exists in the regionGraph
    if (!regionGraph.ContainsKey(startRegion))
    {
        Debug.LogError($"Start region {startRegion} not found in the region graph!");
        return new List<int>();  // Return an empty path if the region is missing
    }

    Dictionary<int, int> cameFrom = new Dictionary<int, int>();
    Dictionary<int, int> costSoFar = new Dictionary<int, int>();

    PriorityQueue<int> frontier = new PriorityQueue<int>();
    frontier.Enqueue(startRegion, 0);

    cameFrom[startRegion] = startRegion;
    costSoFar[startRegion] = 0;

    while (frontier.Count > 0)
    {
        int current = frontier.Dequeue();

        // Debugging current region
        Debug.Log($"Processing region: {current}");

        if (current == targetRegion)
        {
            Debug.Log("Reached target region: " + targetRegion);
            break;
        }

        // Check if the current region has neighbors in the graph
        if (!regionGraph.ContainsKey(current))
        {
            Debug.LogError($"Current region {current} not found in the region graph!");
            continue;
        }

        foreach (int next in regionGraph[current])
        {
            int newCost = costSoFar[current] + 1; // All edges have equal cost
            if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
            {
                costSoFar[next] = newCost;
                int priority = newCost + Mathf.Abs(next - targetRegion);
                frontier.Enqueue(next, priority);
                cameFrom[next] = current;
            }
        }
    }

    List<int> path = new List<int>();
    int step = targetRegion;

    // Track the path while moving backwards
    while (step != startRegion)
    {
        path.Add(step);
        step = cameFrom[step];
    }

    path.Add(startRegion);
    path.Reverse();

    return path;
}

}

