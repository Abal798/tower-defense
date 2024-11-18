using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    public int numberOfMonsterOne;
    public int numberOfMonsterTwo;
    public GameObject monsterTypeOne;
    public GameObject monsterTypeTwo;
    public RessourcesManager RM;
    public float chanceDeSpawnElementaireEntreZeroEtUn;
    public int vagueDapparitionDesElementaires;

    public float basicWaveDuration;

    private int ennemyToSpawn;
    private float timeBetweenSpawn;

    public float squareSize = 50;

    public UnityEvent onLaunchWave;

    public void ButtonFonctionLaunchWave()
    {
        RM.wave++;
        onLaunchWave.Invoke();
        
        //pour le monstre 1
        if (RM.wave < 6)
        {
            numberOfMonsterOne = Mathf.CeilToInt(Mathf.Pow(RM.wave, 2) + 5 * RM.wave + 10);
        }
        else if (RM.wave > 5 && RM.wave < 9)
        {
            numberOfMonsterOne = Mathf.CeilToInt(Mathf.Pow(RM.wave, 3) - 4 * Mathf.Pow(RM.wave, 2) + 5);
        }
        else if (RM.wave > 9 && RM.wave < 20)
        {
            numberOfMonsterOne =  Mathf.CeilToInt(Mathf.Pow(RM.wave, 2) + 5 * RM.wave + 135);
        }
        else
        {
            numberOfMonsterOne = Mathf.CeilToInt(Mathf.Pow(RM.wave, 2) * 0.25f + 546);
        }
        
        //pour le monstre 2
        if (RM.wave > 3)
        {
            numberOfMonsterTwo = Mathf.CeilToInt(RM.wave - 3);
        }
        
        
        StartCoroutine(LaunchWave());
        
    }

    private IEnumerator LaunchWave()
    {
        ennemyToSpawn = numberOfMonsterOne + numberOfMonsterTwo;
        float waveDuration = basicWaveDuration + 2 * RM.wave;
        timeBetweenSpawn = waveDuration / ennemyToSpawn;

        // Spawn des monstres de type un avec intervalle de temps
        for (int i = 0; i < ennemyToSpawn; i++)
        {
            if (Random.Range(1, ennemyToSpawn+1) < numberOfMonsterOne)
            {
                numberOfMonsterOne--;
                GameObject newMonster = Instantiate(monsterTypeOne, GetRandomPositionOnSquareEdge(), Quaternion.identity);
                newMonster.GetComponent<MonsterDeathBehaviour>().RM = RM;
                if (RM.wave >= vagueDapparitionDesElementaires)
                {
                    newMonster.GetComponent<MonsterStats>().type = GetMonsterType();
                }
                else
                {
                    newMonster.GetComponent<MonsterStats>().type = 0;
                }
                
            }
            else
            {
                numberOfMonsterTwo--;
                GameObject newMonster = Instantiate(monsterTypeTwo, GetRandomPositionOnSquareEdge(), Quaternion.identity);
                newMonster.GetComponent<MonsterDeathBehaviour>().RM = RM;
                if (RM.wave >= vagueDapparitionDesElementaires)
                {
                    newMonster.GetComponent<MonsterStats>().type = GetMonsterType();
                }
                else
                {
                    newMonster.GetComponent<MonsterStats>().type = 0;
                }
                
            }
            ennemyToSpawn--;
            yield return new WaitForSeconds(timeBetweenSpawn); // Pause entre chaque apparition
            
        }
        
    }

    Vector3 GetRandomPositionOnSquareEdge()
    {
        float halfSize = squareSize / 2;
        int side = Random.Range(0, 4);

        return side switch
        {
            0 => new Vector3(Random.Range(-halfSize, halfSize), halfSize, 0),
            1 => new Vector3(Random.Range(-halfSize, halfSize), -halfSize, 0),
            2 => new Vector3(-halfSize, Random.Range(-halfSize, halfSize), 0),
            3 => new Vector3(halfSize, Random.Range(-halfSize, halfSize), 0),
            _ => Vector3.zero
        };
    }

    int GetMonsterType()
    {
        return Random.Range(0, Mathf.Round(1 / chanceDeSpawnElementaireEntreZeroEtUn)) == 1
            ? Random.Range(1, 4)
            : 0;
    }
}
