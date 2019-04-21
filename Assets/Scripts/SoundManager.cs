using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager soundManager = null;
    public static float musicVolume = 0.2f;
    public static float sfxVolume = 0.4f;
    public static bool musicMuted = false;
    public static bool sfxMuted = false;    
    AudioSource audi;
    [SerializeField]
    AudioClip[] songs;
    int currentSong = 0;

    // Use this for initialization
    void Start()
    {
        if (soundManager == null)
        {
            soundManager = this; DontDestroyOnLoad(gameObject);
        }

        else if (soundManager != null)
        {
            Destroy(gameObject);
        }

        audi = GetComponent<AudioSource>();
        audi.clip = songs[currentSong];
        audi.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (audi.isPlaying == false)
        {
            if (currentSong == songs.Length - 1)
            {
                currentSong = 0;
                audi.clip = songs[currentSong];
                audi.Play();
            }
            else
            {
                currentSong++;
                audi.clip = songs[currentSong];
                audi.Play();
            }
        }

        if(audi.mute != musicMuted)
        {
            audi.mute = musicMuted;
        }

        if (audi.volume != musicVolume)
        {
            audi.volume = musicVolume;
        }
    }
}
