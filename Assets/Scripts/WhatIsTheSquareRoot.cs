using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatIsTheSquareRoot : MonoBehaviour
{
    SessionManager sessionManager;
    ScoreKeeper scoreKeeper;
    SaveLoader saveLoader;
    [SerializeField]
    Text numberQuestionText;
    [SerializeField]
    Text button1Text;
    [SerializeField]
    Text button2Text;
    [SerializeField]
    Text button3Text;
    [SerializeField]
    Text button4Text;
    Color originalColor;
    int correctIndex;
    int currentRound = 1;
    int maxRounds = 10;
    List<int> scores;
    List<float> times;
    float timer = 0;
    bool canAnswer = true;
    int answer;
    [SerializeField]
    GameObject game;
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    Text endscreenText;

    [SerializeField]
    Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        saveLoader = FindObjectOfType<SaveLoader>();
        originalColor = button1Text.color;
        scores = new List<int>();
        times = new List<float>();
        game.SetActive(true);
        endScreen.SetActive(false);
        SetupQuestion();
    }

    void SetupQuestion()
    {
        canAnswer = true;
        button1Text.color = originalColor; button2Text.color = originalColor;
        button3Text.color = originalColor; button4Text.color = originalColor;
        switch(scoreKeeper.logicLevel)
        {
            case 0:
                answer = Random.Range(2, 6);
                break;
            case 1: answer = Random.Range(2, 8);
                break;
            case 2: answer = Random.Range(4, 8);
                break;
            case 3: answer = Random.Range(4, 10);
                break;
            case 4: answer = Random.Range(6, 10);
                break;
            case 5: answer = Random.Range(6, 12);
                break;
            default:
                answer = Random.Range(6, 16);
                break;
        }
        numberQuestionText.text = (answer * answer).ToString();
        correctIndex = Random.Range(0, 4);
        switch(correctIndex)
        {
            case 0:
                button1Text.text = answer.ToString();
                button2Text.text = (answer + Random.Range(-1, 0)).ToString();
                button3Text.text = (answer + Random.Range(-2, -1)).ToString();
                button4Text.text = (answer + Random.Range(1, 2)).ToString();
                break;
            case 1:
                button2Text.text = answer.ToString();
                button1Text.text = (answer + Random.Range(-1, 0)).ToString();
                button3Text.text = (answer + Random.Range(-2, -1)).ToString();
                button4Text.text = (answer + Random.Range(1, 2)).ToString();
                break;
            case 2:
                button3Text.text = answer.ToString();
                button2Text.text = (answer + Random.Range(-1, 0)).ToString();
                button1Text.text = (answer + Random.Range(-2, -1)).ToString();
                button4Text.text = (answer + Random.Range(1, 2)).ToString();
                break;
            case 3:
                button4Text.text = answer.ToString();
                button2Text.text = (answer + Random.Range(-1, 0)).ToString();
                button3Text.text = (answer + Random.Range(-2, -1)).ToString();
                button1Text.text = (answer + Random.Range(1, 2)).ToString();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canAnswer)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("0.0");
        }
    }

    void EndGame()
    {
        game.SetActive(false);
        endScreen.SetActive(true);
        for(int i = 1; i < scores.Count + 1; i++)
        {
            if (scores[i - 1] != 0)
            {
                endscreenText.text += "Round " + i + " | Correct | " + "Time : " + times[i - 1].ToString("0.00") + "s" + "\n";
            }
            else
            {
                endscreenText.text += "Round " + i + " | Incorrect | " + "Time : " + times[i - 1].ToString("0.00") + "s" + "\n";
            }
        }
        endscreenText.text += "Great Job, Keep Trying \n To Improve Though!";
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    public void Answer(int index)
    {
        if (canAnswer)
        {
            canAnswer = false;
            if (correctIndex == index)
            {
                int roundScore = (int)(200 - (5 * timer));
                if(roundScore < 10)
                {
                    roundScore = 10;
                }
                times.Add(timer);
                timer = 0;
                scoreKeeper.logicPoints += roundScore;
                scores.Add(roundScore);
                if (scoreKeeper.logicPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.logicLevel + 1])
                {
                    scoreKeeper.logicLevel++;
                }
                saveLoader.SaveGameData();
            }
            else { scores.Add(0); times.Add(timer); timer = 0; }
            if (currentRound >= maxRounds)
            {
                EndGame();
            }
            else
            {
                currentRound++;
                switch (correctIndex)
                {
                    case 0:
                        button1Text.color = Color.green;
                        button2Text.color = Color.red;
                        button3Text.color = Color.red;
                        button4Text.color = Color.red;
                        break;
                    case 1:
                        button1Text.color = Color.red;
                        button2Text.color = Color.green;
                        button3Text.color = Color.red;
                        button4Text.color = Color.red;
                        break;
                    case 2:
                        button1Text.color = Color.red;
                        button2Text.color = Color.red;
                        button3Text.color = Color.green;
                        button4Text.color = Color.red;
                        break;
                    case 3:
                        button1Text.color = Color.red;
                        button2Text.color = Color.red;
                        button3Text.color = Color.red;
                        button4Text.color = Color.green;
                        break;
                }
                Invoke("SetupQuestion", 1);
            }
        }
    }
}
