using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
    [SerializeField]
    GameObject volumeControls;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider sfxSlider;

    SaveLoader saveLoader;

    [SerializeField]
    Text musicButtonText;
    [SerializeField]
    Text sfxButtonText;


    // Start is called before the first frame update
    void Start()
    {
        saveLoader = FindObjectOfType<SaveLoader>();
        if(SoundManager.musicMuted)
        {
            musicButtonText.text = "Unmute";
        }
        if(SoundManager.sfxMuted)
        {
            sfxButtonText.text = "Unmute";
        }
        sfxSlider.value = SoundManager.sfxVolume;
        musicSlider.value = SoundManager.musicVolume;
        volumeControls.SetActive(false);
    }

    public void OpenCloseControls(bool _close)
    {
        if(_close)
        {
            volumeControls.SetActive(true);
        }
        else
        {
            volumeControls.SetActive(false);
        }
    }

    public void ChangeVolume(bool _music)
    {
        if(_music)
        {
            SoundManager.musicVolume = musicSlider.value;
        }
        else
        {
            SoundManager.sfxVolume = sfxSlider.value;
        }
        saveLoader.SaveGameData();
    }

    public void OpenLink()
    {
       Application.OpenURL("https://evolve-brain-games.flycricket.io/privacy.html");
    }

    public void Muter(bool _music)
    {
        if(_music)
        {
            SoundManager.musicMuted = !SoundManager.musicMuted;
            if(SoundManager.musicMuted)
            {
                musicButtonText.text = "Unmute";
            }
            else
            {
                musicButtonText.text = "Mute";
            }
        }
        else
        {
            SoundManager.sfxMuted = !SoundManager.sfxMuted;
            if (SoundManager.sfxMuted)
            {
                sfxButtonText.text = "Unmute";
            }
            else
            {
                sfxButtonText.text = "Mute";
            }
        }
        saveLoader.SaveGameData();
    }

}
