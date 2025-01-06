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
    public int numberOfMonsterThree;
    public int numberOfMonsterFour;
    public GameObject monsterTypeOne;
    public GameObject monsterTypeTwo;
    public GameObject monsterTypeThree;
    public GameObject monsterTypeFour;
    public GameObject monsterTypeBoss;
    public RessourcesManager RM;
    public float chanceDeSpawnElementaireEntreZeroEtUn;
    public int vagueDapparitionDesElementaires;

    public int endWaveNumber;
    public int[] monsterOneWaveList;
    public int[] monsterTwoWaveList;
    public int[] monsterThreeWaveList;
    public int[] monsterFourWaveList;

    public List<GameObject> monstersAlive;

    public float basicWaveDuration;

    private int ennemyToSpawn;
    private float timeBetweenSpawn;

    public float squareSize = 50;


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

        foreach (var tower in GridBuilding.current.listeTowerCo)
        {
            tower.Value.GetComponent<ActualizeChild>().HealTower();
        }
        
        
        
        GameObject[] sumonersFlags = GameObject.FindGameObjectsWithTag("SumonersFlag");
        foreach (GameObject sumonersFlag in sumonersFlags)
        {
            sumonersFlag.GetComponent<SummonersFlagBehaviour>().SpawnMonster();
        }
        EndGameStats.EGS.nombreDeVague = RM.wave;

        if (RM.wave < monsterOneWaveList.Length)
        {
            numberOfMonsterOne = monsterOneWaveList[RM.wave];
            numberOfMonsterTwo = monsterTwoWaveList[RM.wave];
            numberOfMonsterThree = monsterThreeWaveList[RM.wave];
            numberOfMonsterFour = monsterFourWaveList[RM.wave];
            
            //if(numberOfMonsterOne > 0) BookManager.instance.monsterSwarmerEncountered = true;
            //if(numberOfMonsterTwo > 0) BookManager.instance.monsterGiantEncountered = true;
        }
        
        
        if(RM.wave > endWaveNumber && monstersAlive.Count == 0) victoryPanel.SetActive(true);
        
        StartCoroutine(LaunchWave());
        
    }

    private IEnumerator LaunchWave()
    {
        ennemyToSpawn = numberOfMonsterOne + numberOfMonsterTwo + numberOfMonsterThree + numberOfMonsterFour;
        float waveDuration = basicWaveDuration + 4 * RM.wave;
        timeBetweenSpawn = waveDuration / ennemyToSpawn;

        // Créer une liste de tout les monstres à générer
        List<int> monsterOrder = new List<int>();
        for (int i = 0; i < numberOfMonsterOne; i++) monsterOrder.Add(1);
        for (int i = 0; i < numberOfMonsterTwo; i++) monsterOrder.Add(2);
        for (int i = 0; i < numberOfMonsterThree; i++) monsterOrder.Add(3);
        for (int i = 0; i < numberOfMonsterFour; i++) monsterOrder.Add(4);
        
        ShuffleList(monsterOrder);
        
        foreach (int monsterType in monsterOrder)
        {
            GameObject newMonster;

            if (monsterType == 1)
            {
                newMonster = Instantiate(monsterTypeOne, GetRandomPositionOnSquareEdge(), Quaternion.identity);
                newMonster.GetComponent<MonsterDeathBehaviour>().RM = RM;
                newMonster.GetComponent<MonsterStats>().type = 0;
            }
            else if (monsterType == 2)
            {
                newMonster = Instantiate(monsterTypeTwo, GetRandomPositionOnSquareEdge(), Quaternion.identity);
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
            else if(monsterType == 3)
            {
                newMonster = Instantiate(monsterTypeThree, GetRandomPositionOnSquareEdge(), Quaternion.identity);
                newMonster.GetComponent<MonsterDeathBehaviour>().RM = RM;
                newMonster.GetComponent<MonsterStats>().type = 0;
            }

            else //if(monsterType == 4)
            {
                newMonster = Instantiate(monsterTypeFour, GetRandomPositionOnSquareEdge(), Quaternion.identity);
                newMonster.GetComponent<MonsterDeathBehaviour>().RM = RM;
                newMonster.GetComponent<MonsterStats>().type = 0;
            }

            newMonster.GetComponent<MonsterDeathBehaviour>().RM = RM;
            monstersAlive.Add(newMonster);
            
            yield return new WaitForSeconds(timeBetweenSpawn);
        }
        
        if (endWaveNumber - 1 == RM.wave) // arrive en dernier systematiquement
        {
            GameObject newMonster = Instantiate(monsterTypeBoss, GetRandomPositionOnSquareEdge(), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = RM;
            newMonster.GetComponent<MonsterStats>().type = 0;
            monstersAlive.Add(newMonster);
        }
    }
    
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
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
