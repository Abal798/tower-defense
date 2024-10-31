using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMouvementBehaviours : MonoBehaviour
{
    public MonsterStats MS;
    
    public List<Vector3Int> deplacements; // Path positions in grid space
    public Pathfinding pathfinding;
    public float speed;
    public GridLayout gridLayout;

    private List<Vector3> worldPositions; // Path positions in world space


    void Awake()
    {
        speed = MS.speed;
    }

    private void Start()
    {
        gridLayout = GameObject.FindGameObjectWithTag("grid").GetComponent<Grid>();
        pathfinding = GetComponent<Pathfinding>();
        
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
        
        if (Vector3.Distance(transform.position, worldPositions[0]) < 0.1f)
        {
            Debug.Log("Reached the next point!");
            
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
}