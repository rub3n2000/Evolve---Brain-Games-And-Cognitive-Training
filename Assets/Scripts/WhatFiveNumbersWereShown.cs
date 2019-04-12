using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhatFiveNumbersWereShown : MonoBehaviour
{
    [SerializeField]
    GameObject ButtonObject;
    [SerializeField]
    GameObject game;
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    Text endScreenText;
    [SerializeField]
    Text infoText;
    [SerializeField]
    Text firstNumberText;
    [SerializeField]
    Text secondNumberText;
    [SerializeField]
    Text thirdNumberText;
    [SerializeField]
    Text fourthNumberText;
    [SerializeField]
    Text fifthNumberText;
    SessionManager sessionManager;
    ScoreKeeper scoreKeeper;
    SaveLoader saveLoader;
    List<int> scores;
    float timer = 1;
    bool timeIsGoing = false;
    int currentRound = 1;
    int[] currentRoundNumbers = new int[5];
    int maxRounds = 10;
    float timeAllowed = 1;


    // Start is called before the first frame update
    void Start()
    {
        scores = new List<int>();
        sessionManager = FindObjectOfType<SessionManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        saveLoader = FindObjectOfType<SaveLoader>();
        endScreen.SetActive(false);
        switch(scoreKeeper.memoryLevel)
        {
            case 0: timeAllowed = 5;
                break;
            case 1: timeAllowed = 4;
                break;
            case 2: timeAllowed = 3;
                break;
            case 3: timeAllowed = 2;
                break;
            case 4: timeAllowed = 2;
                break;
            default: timeAllowed = 1;
                break;
        }
        timer = timeAllowed;
        SetupRound();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsGoing)
        {
            timer -= Time.deltaTime;
        }

        if(timer < 0)
        {
            SetupRoundPart2();
        }
    }

    public void Answer(int number)
    {
        if(firstNumberText.text == "")
        {
            firstNumberText.text = number.ToString();
            if(number == currentRoundNumbers[0])
            {
                scores.Add(100);
                scoreKeeper.memoryPoints += 100;
                if(scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel+1])
                { scoreKeeper.memoryLevel++; }
                saveLoader.SaveGameData();
                firstNumberText.color = Color.green;
            }
            else {
                scores.Add(0);
                firstNumberText.color = Color.red;
            }
        }
        else if(secondNumberText.text =="")
        {
            secondNumberText.text = number.ToString();
            if (number == currentRoundNumbers[1])
            {
                scores[currentRound - 1] += 100;
                scoreKeeper.memoryPoints += 100;
                if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
                { scoreKeeper.memoryLevel++; }
                saveLoader.SaveGameData();
                secondNumberText.color = Color.green;
            }
            else
            {
                secondNumberText.color = Color.red;
            }
        }
        else if (thirdNumberText.text == "")
        {
            thirdNumberText.text = number.ToString();
            if (number == currentRoundNumbers[2])
            {
                scores[currentRound - 1] += 100;
                scoreKeeper.memoryPoints += 100;
                if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
                { scoreKeeper.memoryLevel++; }
                saveLoader.SaveGameData();
                thirdNumberText.color = Color.green;
            }
            else
            {
                thirdNumberText.color = Color.red;
            }
        }
        else if (fourthNumberText.text == "")
        {
            fourthNumberText.text = number.ToString();
            if (number == currentRoundNumbers[3])
            {
                scores[currentRound - 1] += 100;
                scoreKeeper.memoryPoints += 100;
                if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
                { scoreKeeper.memoryLevel++; }
                saveLoader.SaveGameData();
                fourthNumberText.color = Color.green;
            }
            else
            {
                fourthNumberText.color = Color.red;
            }
        }
        else if (fifthNumberText.text == "")
        {
            fifthNumberText.text = number.ToString();
            if (number == currentRoundNumbers[4])
            {
                scores[currentRound - 1] += 100;
                scoreKeeper.memoryPoints += 100;
                if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
                { scoreKeeper.memoryLevel++; }
                saveLoader.SaveGameData();
                fifthNumberText.color = Color.green;
            }
            else
            {
                fifthNumberText.color = Color.red;
            }
            if (currentRound >= maxRounds)
            {
                EndGame();
            }
            else
            {
                currentRound++;
                SetupRound();
            }
        }
    }

    void EndGame()
    {
        game.SetActive(false);
        endScreen.SetActive(true);
        endScreenText.text = "";
        endScreenText.text += "100 Points For\n Each Correct Number \n \n";
        for (int i = 1; i < scores.Count + 1; i++)
        {
            endScreenText.text += "Round " + i + " | Score : " + scores[i - 1] + "\n";
        }
        endScreenText.text += "\n";
        endScreenText.text += "Great Job, Keep Trying \n To Improve Though!";
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    void SetupRoundPart2()
    {
        timeIsGoing = false;
        timer = timeAllowed;
        firstNumberText.text = "";
        secondNumberText.text = "";
        thirdNumberText.text = "";
        fourthNumberText.text = "";
        fifthNumberText.text = "";
        ButtonObject.SetActive(true);
        infoText.text = "Input The Numbers You Saw";
    }

    void SetupRound()
    {
        firstNumberText.color = Color.white;
        secondNumberText.color = Color.white;
        thirdNumberText.color = Color.white;
        fourthNumberText.color = Color.white;
        fifthNumberText.color = Color.white;
        infoText.text = "Remember These Numbers";
        ButtonObject.SetActive(false);
        for(int i = 0; i < 5; i++)
        {
            currentRoundNumbers[i] = Random.Range(1, 10);
        }
        firstNumberText.text = currentRoundNumbers[0].ToString();
        secondNumberText.text = currentRoundNumbers[1].ToString();
        thirdNumberText.text = currentRoundNumbers[2].ToString();
        fourthNumberText.text = currentRoundNumbers[3].ToString();
        fifthNumberText.text = currentRoundNumbers[4].ToString();
        timeIsGoing = true;
    }
}
