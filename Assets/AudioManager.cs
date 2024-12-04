using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource menusSoundsSource;
    
    public AudioClip enemyDie;
    public AudioClip buttonClick;


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
