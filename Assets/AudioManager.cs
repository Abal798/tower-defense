using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;
    public RessourcesManager RM;



    [Header("parametres")] public int waveToChangeMusic;
    
    [Header("audio sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource menusSoundsSource;
    
    
    [Header("----------Audio Clip----------")]
    [Header("----------SFX----------")]
    [Header("ennemis")]
    public AudioClip enemyDie;

    public AudioClip kamikazeTouchTower;
    
    [Header("menus")]
    public AudioClip buttonClick;
    public AudioClip alertDisplay;
    public AudioClip pageTurn;
    public List<AudioClip> menusSounds;

    [Header("spells")] 
    public AudioClip addFireIngredient;
    public AudioClip addEarthIngredient;
    public AudioClip addWaterIngredient;
    public AudioClip fireSpellplacing;
    public AudioClip waterSpellplacing;
    public AudioClip earthSpellplacing;
    public AudioClip cook;
    
    [Header("tower")]
    public AudioClip towerSpawn;
    public AudioClip towerSelect;
    public AudioClip towerDeath;
    public AudioClip towerUpgrade;

    [Header("----------musiques----------")]
    public AudioClip musicMenu;
    public AudioClip musicWaveBeginning;
    public AudioClip musicWaveEnd;
    
    private void Awake()
    {
        AM = this;
    }

    private void Start()
    {
        menusSounds = new List<AudioClip> { buttonClick, pageTurn};
        PlayMusic();
    }
    
    private void Update()
    {
        if (musicSource.clip == musicWaveBeginning && RM.wave >= waveToChangeMusic)
        {
            musicSource.clip = musicWaveEnd;
            musicSource.Play();
        }
        else if (musicSource.clip == musicWaveEnd && RM.wave < waveToChangeMusic)
        {
            musicSource.clip = musicWaveBeginning;
            musicSource.Play();
        }
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

    public void PlayMusic()
    {
        if (RM.wave < waveToChangeMusic)
        {
            musicSource.clip = musicWaveBeginning;
        }
        else
        {
            musicSource.clip = musicWaveEnd;
        }

        musicSource.loop = true; // Enable looping
        musicSource.Play(); // Start playing
    }
}
