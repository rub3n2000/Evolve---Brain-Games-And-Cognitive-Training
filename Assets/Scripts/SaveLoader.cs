using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoader : MonoBehaviour
{
    public static SaveLoader saveLoader = null;

    ScoreKeeper scoreKeeper;

    SoundManager soundManager;

    bool appJustLoaded = true;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = GetComponent<ScoreKeeper>();
        soundManager = GetComponent<SoundManager>();

        if(saveLoader == null)
        {
            saveLoader = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(this);
        }
        else if(saveLoader != null && saveLoader != this)
        {
            Destroy(this);
        }

        if (File.Exists(Application.persistentDataPath + "/saveFile.json") && appJustLoaded == true)
        {
            LoadGameData();
            appJustLoaded = false;
        }

        else if (!File.Exists(Application.persistentDataPath + "/saveFile.json") && appJustLoaded == true)
        {
            appJustLoaded = false;
            SaveGameData();
        }
    }

    #region Public Methods
    public void SaveGameData()
    {
        SaveData saveData = new SaveData();
        saveData.reactionLevel = scoreKeeper.reactionLevel;
        saveData.logicLevel = scoreKeeper.logicLevel;
        saveData.memoryLevel = scoreKeeper.memoryLevel;
        saveData.concentrationLevel = scoreKeeper.concentrationLevel;
        saveData.languageLevel = scoreKeeper.languageLevel;
        saveData.multitaskingLevel = scoreKeeper.multitaskingLevel;
        saveData.reactionPoints = scoreKeeper.reactionPoints;
        saveData.logicPoints = scoreKeeper.logicPoints;
        saveData.memoryPoints = scoreKeeper.memoryPoints;
        saveData.concentrationPoints = scoreKeeper.concentrationPoints;
        saveData.languagePoints = scoreKeeper.languagePoints;
        saveData.multitaskingPoints = scoreKeeper.multitaskingPoints;
        saveData.musicVolume = SoundManager.musicVolume;
        saveData.sfxVolume = SoundManager.sfxVolume;
        saveData.musicMuted = SoundManager.musicMuted;
        saveData.sfxMuted = SoundManager.sfxMuted;

        string saveDataString = JsonUtility.ToJson(saveData);

        using (StreamWriter stream = new StreamWriter(Application.persistentDataPath + "/saveFile.json"))
        {
            stream.Write(saveDataString);
        }
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";
        string jsonString = File.ReadAllText(path);
        SaveData savedData = JsonUtility.FromJson<SaveData>(jsonString);
        scoreKeeper.reactionLevel = savedData.reactionLevel;
        scoreKeeper.logicLevel = savedData.logicLevel;
        scoreKeeper.memoryLevel = savedData.memoryLevel;
        scoreKeeper.concentrationLevel = savedData.concentrationLevel;
        scoreKeeper.languageLevel = savedData.languageLevel;
        scoreKeeper.multitaskingLevel = savedData.multitaskingLevel;
        scoreKeeper.reactionPoints = savedData.reactionPoints;
        scoreKeeper.logicPoints = savedData.logicPoints;
        scoreKeeper.memoryPoints = savedData.memoryPoints;
        scoreKeeper.concentrationPoints = savedData.concentrationPoints;
        scoreKeeper.languagePoints = savedData.languagePoints;
        scoreKeeper.multitaskingPoints = savedData.multitaskingPoints;
        SoundManager.musicVolume = savedData.musicVolume;
        SoundManager.sfxVolume = savedData.sfxVolume;
        SoundManager.musicMuted = savedData.musicMuted;
        SoundManager.sfxMuted = savedData.sfxMuted;
    }
    #endregion
}
[System.Serializable]
public class SaveData
{
    public int reactionLevel;
    public int logicLevel;
    public int memoryLevel;
    public int concentrationLevel;
    public int languageLevel;
    public int multitaskingLevel;

    public int reactionPoints;
    public int logicPoints;
    public int memoryPoints;
    public int concentrationPoints;
    public int languagePoints;
    public int multitaskingPoints;

    public float musicVolume;
    public float sfxVolume;

    public bool musicMuted;
    public bool sfxMuted;
}