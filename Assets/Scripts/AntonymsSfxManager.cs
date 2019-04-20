using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntonymsSfxManager : MonoBehaviour
{
    [SerializeField]
    AudioClip wrong;
    [SerializeField]
    AudioClip correct;
    AudioSource audi;

    // Start is called before the first frame update
    void Start()
    {
        audi = GetComponent<AudioSource>();
        audi.mute = SoundManager.sfxMuted;
        audi.volume = SoundManager.sfxVolume;
    }

    public void PlayAudio(bool _correct)
    {
        if(_correct)
        {
            audi.clip = correct;
            audi.Play();
        }
        else
        {
            audi.clip = wrong;
            audi.Play();
        }
    }

   
}
