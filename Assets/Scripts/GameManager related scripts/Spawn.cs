using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spawn : MonoBehaviour
{
    public GameObject victoryPanel;
    
    public int numberOfMonsterOne;
    public int numberOfMonsterTwo;
    public GameObject monsterTypeOne;
    public GameObject monsterTypeTwo;
    public RessourcesManager RM;
    public float chanceDeSpawnElementaireEntreZeroEtUn;
    public int vagueDapparitionDesElementaires;

    public int endWaveNumber;
    public int[] monsterOneWaveList;
    public int[] monsterTwoWaveList;

    public List<GameObject> monstersAlive;

    public float basicWaveDuration;

    private int ennemyToSpawn;
    private float timeBetweenSpawn;

    public float squareSize = 50;

    public UnityEvent waveLaunched;


    private void Start()
    {
        victoryPanel.SetActive(false);
    }

    private void FixedUpdate()
    {
        monstersAlive.RemoveAll(monster => monster == null);
        if(RM.wave >= endWaveNumber && monstersAlive.Count == 0) victoryPanel.SetActive(true);
    }

    public void ButtonFonctionLaunchWave()
    {
        CleanMonstersAlive();
        RM.wave++;
        waveLaunched.Invoke();
        EndGameStats.EGS.nombreDeVague = RM.wave;

        if (RM.wave < monsterOneWaveList.Length)
        {
            numberOfMonsterOne = monsterOneWaveList[RM.wave];
            numberOfMonsterTwo = monsterTwoWaveList[RM.wave];
            
            //if(numberOfMonsterOne > 0) BookManager.instance.monsterSwarmerEncountered = true;
            //if(numberOfMonsterTwo > 0) BookManager.instance.monsterGiantEncountered = true;
        }
        
        
        if(RM.wave > endWaveNumber && monstersAlive.Count == 0) victoryPanel.SetActive(true);
        
        StartCoroutine(LaunchWave());
        
    }

    private IEnumerator LaunchWave()
    {
        ennemyToSpawn = numberOfMonsterOne + numberOfMonsterTwo;
        float waveDuration = basicWaveDuration + 4 * RM.wave;
        timeBetweenSpawn = waveDuration / ennemyToSpawn;

        // Spawn des monstres de type un avec intervalle de temps
        for (int i = 0; i < ennemyToSpawn; i++)
        {
            if (Random.Range(1, ennemyToSpawn+1) <= numberOfMonsterOne)
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
                
                monstersAlive.Add(newMonster);
                
            }
            else
            {
                BookManager.instance.monsterGiantEncountered = true;
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
                monstersAlive.Add(newMonster);
            }
            ennemyToSpawn--;
            yield return new WaitForSeconds(timeBetweenSpawn); // Pause entre chaque apparition
            
        }
        
    }

    private Vector3 GetRandomPositionOnSquareEdge()
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

    private int GetMonsterType()
    {
        return Random.Range(0, Mathf.Round(1 / chanceDeSpawnElementaireEntreZeroEtUn)) == 1
            ? Random.Range(1, 4)
            : 0;
    }
    
    private void CleanMonstersAlive()
    {
        monstersAlive.RemoveAll(monster => monster == null);
    }
}
