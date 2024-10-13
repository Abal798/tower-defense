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
            float y = Random.Range(-1 * SpawnCircleRadius, SpawnCircleRadius);
            y -= y % 1;
            GameObject newMonster = Instantiate(monsterTypeOne, new Vector2(x,y), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = this.gameObject.GetComponent<RessourcesManager>();
        }
        
        for(var i = 0; i < numberOfMonsterTwo; i++)
        {
            float x = Random.Range(-1 * SpawnCircleRadius, SpawnCircleRadius);
            x -= x % 1;
            float y = Random.Range(-1 * SpawnCircleRadius, SpawnCircleRadius);
            y -= y % 1;
            GameObject newMonster = Instantiate(monsterTypeTwo, new Vector2(x,y), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = this.gameObject.GetComponent<RessourcesManager>();
        }

        hasToSpawn = false;
    }
}
