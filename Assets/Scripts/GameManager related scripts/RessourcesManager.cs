using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourcesManager : MonoBehaviour
{
    [Header("game stats")] 
    public float chrono;
    public float wave;
    
    [Header("player stats")] 
    public float health;
    
    [Header("spells")]
    public List<int> spellSlotOne= new List<int>();
    public List<int> spellSlotTwo = new List<int>();
    public List<int> spellSlotThree= new List<int>();
    
    [Header("souls ressources")] 
    public float fireSoul;
    public float waterSoul;
    public float plantSoul;
}
