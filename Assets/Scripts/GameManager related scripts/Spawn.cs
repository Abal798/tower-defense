using System.Collections;
using System.Collections.Generic;
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
    

    void Update()
    {
        if (hasToSpawn == true && isAlreadySpawning == false)
        {
            LaunchWave(waveNumber, numberOfMonsterOne,numberOfMonsterTwo);
        }
    }

    void LaunchWave(int waveNumber, int numberOfMonssterOne, int numberOfMonsterTwo);
    {
        
    }
}
