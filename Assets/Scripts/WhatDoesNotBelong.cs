using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatDoesNotBelong : MonoBehaviour
{
    int maxRounds = 10;
    int currentRound = 0;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
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
    [SerializeField]
    Text timerText;

    List<int> scores;
    List<float> times;
    bool canAnswer = true;
    [SerializeField]
    Riddle[] riddles;

    bool gameIsGoing = false;
    float timer = 0;

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
        times = new List<float>();
        correctAnswers = new List<string>();
        SetupRound();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsGoing)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("0.0");
        }
        
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
                times.Add(timer);
                int scoreToAdd = (int)(100 + (200 - timer * 10));
                scoreKeeper.languagePoints += scoreToAdd;
                scores.Add(scoreToAdd);
                
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
                endscreenText.text += "Wrong answer | " + " Time : " + times[i].ToString("0.00") + "\n";
            }
            else
            {
                endscreenText.text += "Correct answer | " + "Time : " + times[i].ToString("0.00") + "\n";
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
        timer = 0;
        int riddleIndex = Random.Range(0, riddles.Length);
        currentAnswerId = Random.Range(0, 4);
        int firstWrongOne = Random.Range(0, 4);
        while(firstWrongOne == currentAnswerId) { firstWrongOne = Random.Range(0, 4); }
        int secondWrongOne = Random.Range(0, 4);
        while (secondWrongOne == currentAnswerId || secondWrongOne == firstWrongOne) { secondWrongOne = Random.Range(0, 4); }
        int thirdWrongOne = Random.Range(0, 4);
        while (thirdWrongOne == currentAnswerId || thirdWrongOne == firstWrongOne || thirdWrongOne == secondWrongOne) { thirdWrongOne = Random.Range(0, 4); }
        correctAnswers.Add(riddles[riddleIndex].theOneThatDoesntBelong);
        switch(currentAnswerId)
        {
            case 0: button1Text.text = riddles[riddleIndex].theOneThatDoesntBelong; break;
            case 1: button2Text.text = riddles[riddleIndex].theOneThatDoesntBelong; break;
            case 2: button3Text.text = riddles[riddleIndex].theOneThatDoesntBelong; break;
            case 3: button4Text.text = riddles[riddleIndex].theOneThatDoesntBelong; break;
        }
        switch (firstWrongOne)
        {
            case 0: button1Text.text = riddles[riddleIndex].firstThatBelongs; break;
            case 1: button2Text.text = riddles[riddleIndex].firstThatBelongs; break;
            case 2: button3Text.text = riddles[riddleIndex].firstThatBelongs; break;
            case 3: button4Text.text = riddles[riddleIndex].firstThatBelongs; break;
        }
        switch (secondWrongOne)
        {
            case 0: button1Text.text = riddles[riddleIndex].secondThatBelongs; break;
            case 1: button2Text.text = riddles[riddleIndex].secondThatBelongs; break;
            case 2: button3Text.text = riddles[riddleIndex].secondThatBelongs; break;
            case 3: button4Text.text = riddles[riddleIndex].secondThatBelongs; break;
        }
        switch (thirdWrongOne)
        {
            case 0: button1Text.text = riddles[riddleIndex].thirdThatBelongs; break;
            case 1: button2Text.text = riddles[riddleIndex].thirdThatBelongs; break;
            case 2: button3Text.text = riddles[riddleIndex].thirdThatBelongs; break;
            case 3: button4Text.text = riddles[riddleIndex].thirdThatBelongs; break;
        }
        gameIsGoing = true;
    }

    [System.Serializable]
    struct Riddle
    {
        public string theOneThatDoesntBelong;
        public string firstThatBelongs;
        public string secondThatBelongs;
        public string thirdThatBelongs;
    }
}
