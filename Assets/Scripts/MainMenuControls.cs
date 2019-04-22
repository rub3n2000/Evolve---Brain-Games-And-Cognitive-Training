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

    [SerializeField]
    GameObject reactionLevelUpScreen;
    [SerializeField]
    GameObject logicLevelUpScreen;
    [SerializeField]
    GameObject memoryLevelUpScreen;
    [SerializeField]
    GameObject concentrationLevelUpScreen;
    [SerializeField]
    GameObject languageUpScreen;
    [SerializeField]
    GameObject multitaskingLevelUpScreen;

    public void ChangeReactionScreenStatus(bool on)
    {
        reactionLevelUpScreen.SetActive(on);

    }
    public void ChangeLogicScreen(bool on)
    {
        logicLevelUpScreen.SetActive(on);
    }
    public void ChangeMemoryScreen(bool on)
    {
        memoryLevelUpScreen.SetActive(on);
    }
    public void ChangeConcentrationScreen(bool on)
    {
        concentrationLevelUpScreen.SetActive(on);
    }
    public void ChangeLanguageScreen(bool on)
    {
        languageUpScreen.SetActive(on);
    }
    public void ChangeMultiTaskingScreen(bool on)
    {
        multitaskingLevelUpScreen.SetActive(on);
    }

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
        reactionLevelUpScreen.SetActive(false);
        logicLevelUpScreen.SetActive(false);
        memoryLevelUpScreen.SetActive(false);
        concentrationLevelUpScreen.SetActive(false);
        languageUpScreen.SetActive(false);
        multitaskingLevelUpScreen.SetActive(false);
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
