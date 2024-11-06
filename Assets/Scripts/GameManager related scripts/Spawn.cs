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

    public float squareSize = 50;

    public void ButtonFonctionLaunchWave()
    {
        numberOfMonsterOne = Mathf.CeilToInt(2 * Mathf.Sin(RM.wave) + 3 * RM.wave);
        LaunchWave();
        RM.wave++;
    }
    
    public void LaunchWave()
    {
        for(var i = 0; i < numberOfMonsterOne; i++)
        {
            
            GameObject newMonster = Instantiate(monsterTypeOne,  GetRandomPositionOnSquareEdge(), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = this.gameObject.GetComponent<RessourcesManager>();
            if (RM.wave >= vagueDapparitionDesElementaires)
            {
                newMonster.GetComponent<MonsterStats>().type = GetMonsterType();
            }
            else
            {
                newMonster.GetComponent<MonsterStats>().type = 0;
            }
            
        }
        
        for(var i = 0; i < numberOfMonsterTwo; i++)
        {
            
            GameObject newMonster = Instantiate(monsterTypeTwo, GetRandomPositionOnSquareEdge(), Quaternion.identity);
            newMonster.GetComponent<MonsterDeathBehaviour>().RM = this.gameObject.GetComponent<RessourcesManager>();
            
        }
        
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

    int GetMonsterType()
    {
        if (Random.Range(0, Convert.ToInt32(1/chanceDeSpawnElementaireEntreZeroEtUn)) == 1)
        {
            return Random.Range(1, 4);
        }

        return 0;
    }
    
    
}
