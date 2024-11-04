using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterMouvementBehaviours : MonoBehaviour
{
    public MonsterStats MStats;
    
    public List<Vector3Int> deplacements; // Path positions in grid space
    public Pathfinding pathfinding;
    public float speed;
    public GridLayout gridLayout;
    

    private List<Vector3> worldPositions; // Path positions in world space


    void Awake()
    {
        speed = MStats.speed;
    }

    private void Start()
    {
        gridLayout = GameObject.FindGameObjectWithTag("grid").GetComponent<Grid>();
        pathfinding = GetComponent<Pathfinding>();
        
        Building.UpdatePathfinding.AddListener(UpdatePathfinding);
        
        Vector3Int startCellPosition = gridLayout.WorldToCell(transform.position);
        
        deplacements = pathfinding.PathfindingCalculation(startCellPosition, new Vector3Int(0, 0, 0), false);
        
        worldPositions = new List<Vector3>();
        foreach (Vector3Int cellPosition in deplacements)
        {
            worldPositions.Add(gridLayout.CellToWorld(cellPosition) + gridLayout.cellSize / 2);
        }
        
        //pathfinding.Debuging(true, startCellPosition, new Vector3Int(0, 0, 0));
    }

    void Update()
    {
        if (worldPositions == null || worldPositions.Count == 0) return;
        
        float positionTolerance = 0.7f; 
        
        if (Vector3.Distance(transform.position, worldPositions[0]) < positionTolerance)
        {
            Debug.Log("Reached the next point!");
        
            // Move to the next waypoint if available
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

    void UpdatePathfinding()
    {
        Debug.Log("AHHHHHHHHHHHHHHHHHHHH");
        Vector3Int startCellPosition = gridLayout.WorldToCell(transform.position);
        deplacements =  pathfinding.PathfindingCalculation(startCellPosition, new Vector3Int(0, 0, 0), false);
        worldPositions = new List<Vector3>();
        foreach (Vector3Int cellPosition in deplacements)
        {
            worldPositions.Add(gridLayout.CellToWorld(cellPosition) + gridLayout.cellSize / 2);
        }
    }
    
    private void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
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



    IEnumerator SlowEffect(float time, float slowAmount)
    {
        float originalSpeed = speed;
        speed = slowAmount * speed;
        yield return new WaitForSeconds(4f); 
        speed = originalSpeed;
    }
}