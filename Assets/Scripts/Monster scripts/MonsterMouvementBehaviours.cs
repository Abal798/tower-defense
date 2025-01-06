using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMouvementBehaviours : MonoBehaviour
{
    public MonsterStats MStats;
    public List<Vector3Int> deplacements;
    public HierarchicalPathfinding hierarchicalPathfinding;
    public Pathfinding pathfinding;
    public float speed;
    public GridLayout gridLayout;
    public Vector3Int objectif = Vector3Int.zero;
    
    public float towerDetectionRadius = 5f; 

    private List<Vector3> worldPositions;
    private GameObject targetTower;  // The tower the monster is currently moving towards
    private float lastTowerCheckTime = 0f;
    private float towerProximityThreshold = 0.7f;  // Tolerance distance to consider the tower "reached"
    private float checkTowerInterval = 1f;  // How often to check for nearby towers

    private void Start()
    {
        speed = MStats.speed;

        // Ensure grid layout and pathfinding are initialized
        if (gridLayout == null)
        {
            GameObject gridObject = GameObject.FindGameObjectWithTag("grid");
            if (gridObject == null)
            {
                Debug.LogError("No object with tag 'grid' found in the scene!");
                return;
            }

            gridLayout = gridObject.GetComponent<Grid>();
            if (gridLayout == null)
            {
                Debug.LogError("The object with tag 'grid' does not have a Grid component!");
                return;
            }
        }

        hierarchicalPathfinding = GetComponent<HierarchicalPathfinding>();
        if (hierarchicalPathfinding == null)
        {
            Debug.LogError("No HierarchicalPathfinding component found on this GameObject!");
            return;
        }

        pathfinding = GetComponent<Pathfinding>();
        if (pathfinding == null)
        {
            Debug.LogError("No Pathfinding component found on this GameObject!");
            return;
        }

        Building.UpdatePathfinding.AddListener(UpdatePathfinding);
        CalculatePath();
    }

    private void Update()
    {
        if (Time.time - lastTowerCheckTime >= checkTowerInterval)
        {
            FindNearestTower();
            lastTowerCheckTime = Time.time;
        }

        // If a tower is the target, move towards it
        if (targetTower != null)
        {
            MoveTowardsTower();
            return;  // Skip normal pathfinding while moving towards the tower
        }

        // Normal movement behavior
        if (worldPositions == null || worldPositions.Count == 0) return;

        float positionTolerance = 0.7f;

        if (Vector3.Distance(transform.position, worldPositions[0]) < positionTolerance)
        {
            if (worldPositions.Count > 1)
            {
                worldPositions.RemoveAt(0);
            }
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, worldPositions[0], step);
        }
    }

    public void UpdatePathfinding()
    {
        CalculatePath();
    }

    private void CalculatePath()
    {
        Vector3Int startCellPosition = gridLayout.WorldToCell(transform.position);
        int startRegion = hierarchicalPathfinding.CalculateRegionID(startCellPosition.x, startCellPosition.y);
        int targetRegion = hierarchicalPathfinding.CalculateRegionID(objectif.x, objectif.y);

        List<int> highLevelPath = hierarchicalPathfinding.HighLevelPath(startRegion, targetRegion);

        deplacements = new List<Vector3Int>();

        foreach (int region in highLevelPath)
        {
            List<Vector3Int> refinedPath = RefinePathWithinRegion(startCellPosition, objectif);
            deplacements.AddRange(refinedPath);

            if (deplacements.Contains(objectif))
                break;
        }

        worldPositions = new List<Vector3>();
        foreach (Vector3Int cellPosition in deplacements)
        {
            worldPositions.Add(gridLayout.CellToWorld(cellPosition) + gridLayout.cellSize / 2);
        }
    }

    private List<Vector3Int> RefinePathWithinRegion(Vector3Int start, Vector3Int end)
    {
        return pathfinding.PathfindingCalculation(start, end, false);
    }

    private void FindNearestTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

        GameObject nearestTower = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject tower in towers)
        {
            float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
            if (distanceToTower < closestDistance && distanceToTower <= towerDetectionRadius)
            {
                closestDistance = distanceToTower;
                nearestTower = tower;
            }
        }

        if (nearestTower != null)
        {
            targetTower = nearestTower;
        }
        else
        {
            targetTower = null;  
        }
    }

    private void MoveTowardsTower()
    {
        if (targetTower != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetTower.transform.position, step);

            // If the monster is close enough to the tower, stop and destroy it (or remove it)
            if (Vector3.Distance(transform.position, targetTower.transform.position) < towerProximityThreshold)
            {
                CalculatePath();  // Recalculate path to the original target (if necessary)
            }
        }
    }

    private void OnDestroy()
    {
        Building.UpdatePathfinding.RemoveListener(UpdatePathfinding);
    }
}
