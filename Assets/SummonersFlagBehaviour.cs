using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonersFlagBehaviour : MonoBehaviour
{
    
    public int numberOfMonstersToSpawn = 10;
    public GameObject swarmerPrefab;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void SpawnMonster()
    {
        for (int i = 0; i < numberOfMonstersToSpawn; i++)
        {
            float xoffset = Random.Range(0, 3);
            float yoffset = Random.Range(0, 3);
            GameObject newSwarmer = Instantiate(swarmerPrefab, new Vector3(transform.position.x + xoffset, transform.position.y + yoffset, transform.position.z), Quaternion.identity);
            newSwarmer.GetComponent<MonsterDeathBehaviour>().RM = FindObjectOfType<RessourcesManager>();
            newSwarmer.GetComponent<MonsterStats>().type = 0;
        }
        
        Destroy(this.gameObject);
    }
}
