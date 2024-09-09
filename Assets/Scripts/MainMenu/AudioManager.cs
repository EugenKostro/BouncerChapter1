using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;  // Referenca na AudioSource

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  // Sprječava uništavanje između scena
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;  // Postavljanje glasnoće
    }
}