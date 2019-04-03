using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatDoesThisWordMean : MonoBehaviour
{
    int maxRounds = 10;
    int currentRound = 0;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    [SerializeField]
    Text wordText;
    [SerializeField]
    Text button1Text;
    [SerializeField]
    Text button2Text;
    [SerializeField]
    Text button3Text;
    [SerializeField]
    Text button4Text;
    Color originalColor;
    int currentAnswerId;
    [SerializeField]
    GameObject game;
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    Text endscreenText;
    
    List<int> scores;
    bool canAnswer = true;
    [SerializeField]
    WordToDefinition[] wordToDefinitions;

    List<string> correctAnswers;

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        endScreen.SetActive(false);
        game.SetActive(true);
        originalColor = button1Text.color;
        scores = new List<int>();
        correctAnswers = new List<string>();
        SetupRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Guess(int id)
    {
        if (canAnswer)
        {
            canAnswer = false;
            switch (currentAnswerId)
            {
                case 0:
                    button1Text.color = Color.green; button2Text.color = Color.red; button3Text.color = Color.red; button4Text.color = Color.red;
                    break;
                case 1:
                    button1Text.color = Color.red; button2Text.color = Color.green; button3Text.color = Color.red; button4Text.color = Color.red;
                    break;
                case 2:
                    button1Text.color = Color.red; button2Text.color = Color.red; button3Text.color = Color.green; button4Text.color = Color.red;
                    break;
                case 3:
                    button1Text.color = Color.red; button2Text.color = Color.red; button3Text.color = Color.red; button4Text.color = Color.green;
                    break;
            }

            if (id == currentAnswerId)
            {
                scoreKeeper.languagePoints += 100;
                scores.Add(100);
                if (scoreKeeper.languagePoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.languageLevel + 1])
                {
                    scoreKeeper.languageLevel++;
                }
                saveLoader.SaveGameData();
            }
            else { scores.Add(0); }
            Invoke("StartNewRound", 1);
        }
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    void EndGame()
    {
        endScreen.SetActive(true);
        game.SetActive(false);
        endscreenText.text = "";
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] == 0)
            {
                endscreenText.text += "Round " + i + " | Wrong | correct answer : " + correctAnswers[i] + "\n";
            }
            else
            {
                endscreenText.text += "Round " + i + " | Correct | " + scores[i] + " points" + "\n";
            }
        }
        endscreenText.text += "\n" + " Well Done, Keep Improving!";
    }

    void StartNewRound()
    {
        if (currentRound >= 10)
        {
            EndGame();
        }
        else
        {
            button1Text.color = originalColor; button2Text.color = originalColor;
            button3Text.color = originalColor; button4Text.color = originalColor;
            currentRound++;
            canAnswer = true;
            SetupRound();
        }
    }

    void SetupRound()
    {
        int wordIndex = Random.Range(0, wordToDefinitions.Length);
        wordText.text = wordToDefinitions[wordIndex].words;
        currentAnswerId = Random.Range(0, 4);
        correctAnswers.Add(wordToDefinitions[wordIndex].definitions);
        switch (currentAnswerId)
        {
            case 0:
                button1Text.text = wordToDefinitions[wordIndex].definitions;
                int randomIndex11 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex11 == wordIndex)
                {
                    randomIndex11 = Random.Range(0, wordToDefinitions.Length);
                }
                button2Text.text = wordToDefinitions[randomIndex11].definitions;
                int randomIndex12 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex12 == randomIndex11 || randomIndex12 == wordIndex)
                {
                    randomIndex12 = Random.Range(0, wordToDefinitions.Length);
                }
                button3Text.text = wordToDefinitions[randomIndex12].definitions;
                int randomIndex13 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex13 == wordIndex || randomIndex13 == randomIndex12 || randomIndex13 == randomIndex11)
                {
                    randomIndex13 = Random.Range(0, wordToDefinitions.Length);
                }
                button4Text.text = wordToDefinitions[randomIndex13].definitions;
                break;
            case 1:
                button2Text.text = wordToDefinitions[wordIndex].definitions;
                int randomIndex21 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex21 == wordIndex)
                {
                    randomIndex21 = Random.Range(0, wordToDefinitions.Length);
                }
                button1Text.text = wordToDefinitions[randomIndex21].definitions;
                int randomIndex22 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex22 == wordIndex || randomIndex22 == randomIndex21)
                {
                    randomIndex22 = Random.Range(0, wordToDefinitions.Length);
                }
                button3Text.text = wordToDefinitions[randomIndex22].definitions;
                int randomIndex23 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex23 == wordIndex || randomIndex23 == randomIndex22 || randomIndex23 == randomIndex21)
                {
                    randomIndex23 = Random.Range(0, wordToDefinitions.Length);
                }
                button4Text.text = wordToDefinitions[randomIndex23].definitions;
                break;
            case 2:
                button3Text.text = wordToDefinitions[wordIndex].definitions;
                int randomIndex31 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex31 == wordIndex)
                {
                    randomIndex31 = Random.Range(0, wordToDefinitions.Length);
                }
                button1Text.text = wordToDefinitions[randomIndex31].definitions;
                int randomIndex32 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex32 == wordIndex || randomIndex32 == randomIndex31)
                {
                    randomIndex32 = Random.Range(0, wordToDefinitions.Length);
                }
                button2Text.text = wordToDefinitions[randomIndex32].definitions;
                int randomIndex33 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex33 == wordIndex || randomIndex33 == randomIndex32 || randomIndex33 == randomIndex31)
                {
                    randomIndex33 = Random.Range(0, wordToDefinitions.Length);
                }
                button4Text.text = wordToDefinitions[randomIndex33].definitions;
                break;
            case 3:
                button4Text.text = wordToDefinitions[wordIndex].definitions;
                int randomIndex41 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex41 == wordIndex)
                {
                    randomIndex41 = Random.Range(0, wordToDefinitions.Length);
                }
                button1Text.text = wordToDefinitions[randomIndex41].definitions;
                int randomIndex42 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex42 == wordIndex || randomIndex42 == randomIndex41)
                {
                    randomIndex42 = Random.Range(0, wordToDefinitions.Length);
                }
                button3Text.text = wordToDefinitions[randomIndex42].definitions;
                int randomIndex43 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex43 == wordIndex || randomIndex43 == randomIndex42 || randomIndex43 == randomIndex41)
                {
                    randomIndex43 = Random.Range(0, wordToDefinitions.Length);
                }
                button2Text.text = wordToDefinitions[randomIndex43].definitions;
                break;
        }

    }

    [System.Serializable]
    struct WordToDefinition
    {
        public string words;
        public string definitions;
    }
}
