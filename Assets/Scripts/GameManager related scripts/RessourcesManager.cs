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
    
    [Header("ressources")]
    public int wood;
    public int stone;

    [Header("souls ressources")] 
    public float fireSoul;
    public float waterSoul;
    public float plantSoul;
}
