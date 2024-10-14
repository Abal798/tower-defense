using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool hasToSpawn;
    public bool isAlreadySpawning = false;
    public int numberOfMonsterOne;
    public int numberOfMonsterTwo;
    public GameObject monsterTypeOne;
    public GameObject monsterTypeTwo;
    public RessourcesManager RM;

    public float squareSize = 56;

    public void ButtonFonctionLaunchWave()
    {
        hasToSpawn = true;
    }

    void Update()
    {
        if (hasToSpawn == true && isAlreadySpawning == false)
        {
            isAlreadySpawning = true;
            LaunchWave();
            isAlreadySpawning = false;
            RM.wave++;

        }
        
    }

    public void LaunchWave()
    {
        for(var i = 0; i < numberOfMonsterOne; i++)
        {
            
            GameObject newMonster = Instantiate(monsterTypeOne,  GetRandomPositionOnSquareEdge(), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = this.gameObject.GetComponent<RessourcesManager>();
            newMonster.GetComponent<MonsterMovementBehaviourTemprary>().target = this.gameObject;
            newMonster.GetComponent<MonsterDeathBehaviour>().deathPrticulesParent = this.transform;
        }
        
        for(var i = 0; i < numberOfMonsterTwo; i++)
        {
            
            GameObject newMonster = Instantiate(monsterTypeTwo, GetRandomPositionOnSquareEdge(), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = this.gameObject.GetComponent<RessourcesManager>();
            newMonster.GetComponent<MonsterMovementBehaviourTemprary>().target = this.gameObject;
            newMonster.GetComponent<MonsterDeathBehaviour>().deathPrticulesParent = this.transform;
        }

        hasToSpawn = false;
    }
    
    
    Vector3 GetRandomPositionOnSquareEdge()
    {
        float halfSize = squareSize / 2;
        // Choose a random side: 0 = Top, 1 = Bottom, 2 = Left, 3 = Right
        int side = Random.Range(0, 4);

        Vector3 position = Vector3.zero;
        switch (side)
        {
            case 0: // Top
                position = new Vector3(Random.Range(-halfSize, halfSize), halfSize, 0);
                break;
            case 1: // Bottom
                position = new Vector3(Random.Range(-halfSize, halfSize), -halfSize, 0);
                break;
            case 2: // Left
                position = new Vector3(-halfSize, Random.Range(-halfSize, halfSize), 0);
                break;
            case 3: // Right
                position = new Vector3(halfSize, Random.Range(-halfSize, halfSize), 0);
                break;
        }

        return position;
    }
    
    
}
