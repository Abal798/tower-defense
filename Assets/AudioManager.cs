using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource MenusSounds;
    
    public AudioClip enemyDie;
    public AudioClip buttonClick;


    private void Awake()
    {
        AM = this;
    }


    public void PlaySfx(AudioClip clip, float volume = 1f)
    {
        SFXSource.volume = volume;
        SFXSource.PlayOneShot(clip);
    }

}
