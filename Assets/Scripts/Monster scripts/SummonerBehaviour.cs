using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBehaviour : MonoBehaviour
{
    
    public GameObject summonersFlag;
    public int numberOfMonstersSumoned = 10;
    
    private void OnDestroy()
    { 
        GameObject newFlag = Instantiate(summonersFlag, transform.position, Quaternion.identity);
        newFlag.GetComponent<SummonersFlagBehaviour>().numberOfMonstersToSpawn = numberOfMonstersSumoned;
    }
}
