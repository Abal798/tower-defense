using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;
    
    [Header("audio sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource menusSoundsSource;
    
    
    [Header("----------Audio Clip----------")]
    [Header("----------SFX----------")]
    [Header("ennemis")]
    public AudioClip enemyDie;
    
    [Header("menus")]
    public AudioClip buttonClick;
    public AudioClip alertDisplay;
    public AudioClip pageTurn;
    public List<AudioClip> menusSounds;

    [Header("spells")] 
    public AudioClip addFireIngredient;
    public AudioClip addEarthIngredient;
    public AudioClip fireSpellplacing;
    public AudioClip waterSpellplacing;
    public AudioClip cook;
    
    [Header("tower")]
    public AudioClip towerSpawn;
    public AudioClip towerSelect;
    public AudioClip towerDeath;
    public AudioClip towerUpgrade;

    private void Awake()
    {
        AM = this;
    }

    private void Start()
    {
        menusSounds = new List<AudioClip> { buttonClick, pageTurn};
    }

    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMenuSound(int i = 0)
    {
        if (i >= 0 && i < menusSounds.Count)
        {
            menusSoundsSource.PlayOneShot(menusSounds[i]);
        }
        else
        {
            Debug.LogWarning("Index invalide pour PlayMenuSound : " + i);
        }
    }
}
