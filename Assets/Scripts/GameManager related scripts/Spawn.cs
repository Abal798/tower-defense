using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool hasToSpawn;
    public bool isAlreadySpawning = false;
    public int waveNumber;
    public int numberOfMonsterOne;
    public int numberOfMonsterTwo;
    public GameObject monsterTypeOne;
    public GameObject monsterTypeTwo;

    public float SpawnCircleRadius;

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
            waveNumber++;

        }
        
    }

    public void LaunchWave()
    {
        for(var i = 0; i < numberOfMonsterOne; i++)
        {
            float x = Random.Range(-1 * SpawnCircleRadius, SpawnCircleRadius);
            x -= x % 1;
            if (x - 10 < 0)
            {
                if (x > 0)
                {
                    x += 10;
                }
                else
                {
                    x -= 10;
                }
            }
            float y = Random.Range(-1 * SpawnCircleRadius, SpawnCircleRadius);
            y -= y % 1;
            if (y - 10 < 0)
            {
                if (y > 0)
                {
                    y += 10;
                }
                else
                {
                    y -= 10;
                }
            }
            GameObject newMonster = Instantiate(monsterTypeOne, new Vector2(x,y), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = this.gameObject.GetComponent<RessourcesManager>();
            newMonster.GetComponent<MonsterMovementBehaviourTemprary>().target = this.gameObject;
        }
        
        for(var i = 0; i < numberOfMonsterTwo; i++)
        {
            float x = Random.Range(-1 * SpawnCircleRadius, SpawnCircleRadius);
            x -= x % 1;
            if (x - 10 < 0)
            {
                if (x > 0)
                {
                    x += 10;
                }
                else
                {
                    x -= 10;
                }
            }
            float y = Random.Range(-1 * SpawnCircleRadius, SpawnCircleRadius);
            y -= y % 1;
            if (y - 10 < 0)
            {
                if (y > 0)
                {
                    y += 10;
                }
                else
                {
                    y -= 10;
                }
            }
            GameObject newMonster = Instantiate(monsterTypeTwo, new Vector2(x,y), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = this.gameObject.GetComponent<RessourcesManager>();
            newMonster.GetComponent<MonsterMovementBehaviourTemprary>().target = this.gameObject;
        }

        hasToSpawn = false;
    }
    
    
}
