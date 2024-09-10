using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;  

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}