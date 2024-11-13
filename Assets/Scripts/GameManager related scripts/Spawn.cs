using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
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

    public void ButtonFonctionLaunchWave()
    {
        RM.wave++;
        if (RM.wave < 20)
        {
            numberOfMonsterOne = Mathf.CeilToInt(Mathf.Pow(RM.wave, 2) + 5 * RM.wave + 10);
        }
        else
        {
            numberOfMonsterOne = Mathf.CeilToInt(Mathf.Pow(RM.wave, 2) * 0.25f + 430);
        }
        
        StartCoroutine(LaunchWave());
        
    }

    private IEnumerator LaunchWave()
    {
        ennemyToSpawn = numberOfMonsterOne;
        float waveDuration = basicWaveDuration + 2 * RM.wave;
        timeBetweenSpawn = waveDuration / numberOfMonsterOne;

        // Spawn des monstres de type un avec intervalle de temps
        for (int i = 0; i < numberOfMonsterOne; i++)
        {
            ennemyToSpawn--;
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

            yield return new WaitForSeconds(timeBetweenSpawn); // Pause entre chaque apparition
        }

        // Spawn des monstres de type deux
        for (int i = 0; i < numberOfMonsterTwo; i++)
        {
            GameObject newMonster = Instantiate(monsterTypeTwo, GetRandomPositionOnSquareEdge(), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = RM;
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
