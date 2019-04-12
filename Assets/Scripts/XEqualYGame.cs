using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XEqualYGame : MonoBehaviour
{
    bool statementIsTrue;
    float timeToAnswer = 0;
    int currentScore = 1000;
    int questionsTillDone = 10;
    int currentQuestion = 1;
    Text xText;
    Text yText;
    ScoreKeeper scoreKeeper;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    List<float> times;
    List<int> scores;
    GameObject endText;
    GameObject game;
    GameObject next;

    private void Start()
    {
        xText = GameObject.FindGameObjectWithTag("XText").GetComponent<Text>();
        yText = GameObject.FindGameObjectWithTag("YText").GetComponent<Text>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        endText = GameObject.FindGameObjectWithTag("EndScreen");
        game = GameObject.FindGameObjectWithTag("Game");
        next = GameObject.FindGameObjectWithTag("Next");
        next.SetActive(false);
        endText.SetActive(false);
        CreateStatement();
        times = new List<float>();
        scores = new List<int>();
    }
    private void Update()
    {
        timeToAnswer += Time.deltaTime;
    }

    public void True()
    {
        if (statementIsTrue)
        {
            times.Add(timeToAnswer);
            int roundScore = (int)(200 * scoreKeeper.logicLevel - (timeToAnswer * 5));
            if (roundScore < 10)
            {
                roundScore = 10;
            }
            scores.Add(roundScore);
            currentScore += roundScore;
            if(currentQuestion >= questionsTillDone)
            {
                EndGame();
            }
            else
            {
                currentQuestion++;
                CreateStatement();
                timeToAnswer = 0;
            }
        }
        else
        {
            times.Add(timeToAnswer);
            int roundScore = 0;
            scores.Add(roundScore);
            currentScore -= roundScore;
            if (currentQuestion >= questionsTillDone)
            {
                EndGame();
            }
            else
            {
                currentQuestion++;
                CreateStatement();
                timeToAnswer = 0;
            }
        }
    }

    public void False()
    {
        if (!statementIsTrue)
        {
            times.Add(timeToAnswer);
            int roundScore = (int)(200 * scoreKeeper.logicLevel - (timeToAnswer * 5));
            if (roundScore < 10)
            {
                roundScore = 10;
            }
            scores.Add(roundScore);
            currentScore += roundScore;
            if (currentQuestion >= questionsTillDone)
            {
                EndGame();
            }
            else
            {
                currentQuestion++;
                CreateStatement();
                timeToAnswer = 0;
            }
        }
        else
        {
            times.Add(timeToAnswer);
            int roundScore = 0;
            scores.Add(roundScore);
            currentScore += roundScore;
            if (currentQuestion >= questionsTillDone)
            {
                EndGame();
            }
            else
            {
                currentQuestion++;
                CreateStatement();
                timeToAnswer = 0;
            }
        }
    }

    public void NextExercise()
    {
        int totalScore = 0;
        for(int i = 0; i < scores.Count; i++)
        {
            totalScore += scores[i];
        }
        scoreKeeper.logicPoints += totalScore;
        if(scoreKeeper.logicPoints >= scoreKeeper.pointsRequiredForLevel[scoreKeeper.logicLevel + 1])
        {
            scoreKeeper.logicLevel++;
        }
        saveLoader.SaveGameData();
        sessionManager.ContinueSession();
    }

    void EndGame()
    {
        game.SetActive(false);
        endText.SetActive(true);
        next.SetActive(true);
        for(int i = 0; i < times.Count; i++)
        {
            endText.GetComponent<Text>().text += "Round " + (i + 1).ToString() + " : ";
            if(scores[i] == 0)
            {
                endText.GetComponent<Text>().text += "Wrong answer | " + " Time : " + times[i].ToString("0.00") + "\n";
            }
            else
            {
                endText.GetComponent<Text>().text += "Correct answer | " + "Time : " + times[i].ToString("0.00") + "\n";
            }
        }

        endText.GetComponent<Text>().text += "\n Great Job, keep trying to \n improve times and scores!";
    }

    void CreateStatement()
    {
        switch(scoreKeeper.logicLevel)
        {
            case 0:
                int x = Random.Range(1, 11);
                int y = Random.Range(1, 11);
                int modifier = Random.Range(0, 2);
                string modifierString = "";
                int correctAnswer = 0;
                int z = 0;
                if(modifier == 0)
                {
                    modifierString = "+";
                    correctAnswer = x + y;
                }
                else
                {
                    modifierString = "-";
                    correctAnswer = x - y;
                }
                int trueOrFalse = Random.Range(0, 2);
                if(trueOrFalse == 0)
                {
                    z = correctAnswer;
                    statementIsTrue = true;
                }
                else
                {
                    z = correctAnswer + Random.Range(-2, 2);
                    statementIsTrue = false;
                }
                xText.text = x.ToString() + " " + modifierString + " " + y.ToString();
                yText.text = z.ToString();
                break;

            case 1:
                int a = Random.Range(1, 11);
                int b = Random.Range(1, 11);
                int modifier2 = Random.Range(0, 2);
                string modifierString2 = "";
                int correctAnswer2 = 0;
                int c = Random.Range(1,11);
                int d = 0;
                if (modifier2 == 0)
                {
                    modifierString2 = "+";
                    correctAnswer2 = (a + b) + c;
                }
                else
                {
                    modifierString2 = "-";
                    correctAnswer2 = (a - b) + c;
                }
                int trueOrFalse2 = Random.Range(0, 2);
                if (trueOrFalse2 == 0)
                {
                    d = correctAnswer2;
                    statementIsTrue = true;
                }
                else
                {
                    d = correctAnswer2 + Random.Range(-2, 2);
                    statementIsTrue = false;
                }
                xText.text = "(" + a.ToString() + " " + modifierString2 + " " + b.ToString() + ")" + " + " + c.ToString();
                yText.text = d.ToString();
                break;
            case 2:
                int a2 = Random.Range(1, 11);
                int b2 = Random.Range(1, 11);
                int modifier3 = Random.Range(0, 2);
                string modifierString3 = "";
                int correctAnswer3 = 0;
                int c2 = Random.Range(1, 11);
                int d2 = 0;
                if (modifier3 == 0)
                {
                    modifierString3 = "+";
                    correctAnswer3 = (a2 + b2) + c2;
                }
                else
                {
                    modifierString3 = "-";
                    correctAnswer3 = (a2 - b2) + c2;
                }
                int trueOrFalse3 = Random.Range(0, 2);
                if (trueOrFalse3 == 0)
                {
                    d2 = correctAnswer3;
                    statementIsTrue = true;
                }
                else
                {
                    d2 = correctAnswer3 + Random.Range(-2, 2);
                    statementIsTrue = false;
                }
                xText.text = "(" + a2.ToString() + " " + modifierString3 + " " + b2.ToString() + ")" + " + " + c2.ToString();
                yText.text = d2.ToString();
                break;
            case 3:
                float x3 = Random.Range(1, 11);
                float y3 = Random.Range(1, 11);
                int modifier4 = Random.Range(0, 2);
                string modifierString4 = "";
                float correctAnswer4 = 0;
                float z3 = 0;
                if (modifier4 == 0)
                {
                    modifierString4 = "*";
                    correctAnswer4 = x3 * y3;
                }
                else
                {
                    modifierString4 = "/";
                    correctAnswer4 = x3 / y3;
                }
                int trueOrFalse4 = Random.Range(0, 2);
                if (trueOrFalse4 == 0)
                {
                    z3 = correctAnswer4;
                    statementIsTrue = true;
                }
                else
                {
                    z3 = correctAnswer4 + Random.Range(-5, 5);
                    statementIsTrue = false;
                }
                xText.text = x3.ToString() + " " + modifierString4 + " " + y3.ToString();
                yText.text = z3.ToString();
                break;
            case 4:
                float x4 = Random.Range(1, 11);
                float y4 = Random.Range(1, 11);
                int modifier5 = Random.Range(0, 2);
                string modifierString5 = "";
                float correctAnswer5 = 0;
                float z4 = 0;
                float n4 = Random.Range(1, 11);
                if (modifier5 == 0)
                {
                    modifierString5 = "*";
                    correctAnswer5 = (x4 * y4) * n4;
                }
                else
                {
                    modifierString5 = "/";
                    correctAnswer5 = (x4 / y4) * n4;
                }
                int trueOrFalse5 = Random.Range(0, 2);
                if (trueOrFalse5 == 0)
                {
                    z4 = correctAnswer5;
                    statementIsTrue = true;
                }
                else
                {
                    z4 = correctAnswer5 + Random.Range(-5, 5);
                    statementIsTrue = false;
                }
                xText.text = "(" + x4.ToString() + " " + modifierString5 + " " + y4.ToString() + ")" + " * " + n4;
                yText.text = z4.ToString();
                break;
            default:
            float x5 = Random.Range(1, 11);
            float y5 = Random.Range(1, 11);
            int modifier6 = Random.Range(0, 2);
            string modifierString6 = "";
            float correctAnswer6 = 0;
            float z5 = 0;
            float n5 = Random.Range(1, 11);
            if (modifier6 == 0)
            {
                modifierString6 = "*";
                correctAnswer6 = (x5 * y5) * n5;
            }
            else
            {
                modifierString6 = "/";
                correctAnswer6 = (x5 / y5) * n5;
            }
            int trueOrFalse6 = Random.Range(0, 2);
            if (trueOrFalse6 == 0)
            {
                z5 = correctAnswer6;
                statementIsTrue = true;
            }
            else
            {
                z5 = correctAnswer6 + Random.Range(-5, 5);
                statementIsTrue = false;
            }
            int numberForDividing = Random.Range(0,2);
            string z5String = "";

            if (numberForDividing == 0)
            {
                z5String = (z5 / 2).ToString() + "*" + " 2";
            }
            else
            {
                z5String = (z5 / 4).ToString() + "*" + " 4";
            }
            xText.text = "(" + x5.ToString() + " " + modifierString6 + " " + y5.ToString() + ")" + " * " + n5;
            yText.text = z5String;
            break;
        }
    }
}
