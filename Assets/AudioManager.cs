using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;
    
    [Header("audio sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource menusSoundsSource;
    
    
    [Header("----------Audio Clip----------")]
    public AudioClip enemyDie;
    public AudioClip buttonClick;
    public AudioClip towerSpawn;
    public AudioClip AlertDisplay;


    private void Awake()
    {
        AM = this;
    }


    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayButtonClickSound()
    {
        menusSoundsSource.PlayOneShot(buttonClick);
    }

}
