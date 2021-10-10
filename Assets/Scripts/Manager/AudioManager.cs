namespace Manager
{
    using UnityEngine;

    using Extension;

    public class AudioManager : SingletonMono<AudioManager>
    {
        // Audio players components.
        public AudioSource EffectsSource;
        public AudioSource MusicSource;

        public override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
        }

        // Play a single clip through the music source.
        public void PlayMusic(AudioClip? clip = null)
        {
            if (clip != null)
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
}