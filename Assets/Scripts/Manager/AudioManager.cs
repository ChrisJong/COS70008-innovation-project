using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio players components.
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    // Singleton instance.
    public static AudioManager Instance = null;

    // Start is called before the first frame update
    void Start()
    {
        // If there is not already an instance of SoundManager, set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip? clip = null)
    {
        if(clip != null)
        {
            MusicSource.clip = clip;
        }
        MusicSource.Play();
    }

    // Play a clip for sound effect
    public void PlaySoundEffect(AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }
}
