using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMouvementBehaviours : MonoBehaviour
{
    public MonsterStats MStats;
    public List<Vector3Int> deplacements; // Path positions in grid space
    public Pathfinding pathfinding;
    public float speed;
    public GridLayout gridLayout; // Assign in Inspector or find it dynamically
    public Vector3Int objectif = Vector3Int.zero;

    private List<Vector3> worldPositions; // Path positions in world space

    private void Start()
    {
        speed = MStats.speed;

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

        pathfinding = GetComponent<Pathfinding>();
        Building.UpdatePathfinding.AddListener(UpdatePathfinding);

        Vector3Int startCellPosition = gridLayout.WorldToCell(transform.position);
        deplacements = pathfinding.PathfindingCalculation(startCellPosition, objectif, false);

        worldPositions = new List<Vector3>();
        foreach (Vector3Int cellPosition in deplacements)
        {
            worldPositions.Add(gridLayout.CellToWorld(cellPosition) + gridLayout.cellSize / 2);
        }
    }

    private void Update()
    {
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
        Vector3Int startCellPosition = gridLayout.WorldToCell(transform.position);
        deplacements = pathfinding.PathfindingCalculation(startCellPosition, objectif, false);

        worldPositions = new List<Vector3>();
        foreach (Vector3Int cellPosition in deplacements)
        {
            worldPositions.Add(gridLayout.CellToWorld(cellPosition) + gridLayout.cellSize / 2);
        }
    }

    private void OnDestroy()
    {
        Building.UpdatePathfinding.RemoveListener(UpdatePathfinding);
    }

    public void movementSpell(int movementID)
    {
        if (movementID == 1)
        {
            StartCoroutine(SlowEffect(4f, 0.4f));
        }
        else if (movementID == 2)
        {
            StartCoroutine(SlowEffect(4f, 0.2f));
        }
        else if (movementID == 3)
        {
            StartCoroutine(SlowEffect(0.5f, 1f));
        }
    }

    private IEnumerator SlowEffect(float time, float slowAmount)
    {
        float originalSpeed = speed;
        speed = slowAmount * speed;
        yield return new WaitForSeconds(time);
        speed = originalSpeed;
    }
}
