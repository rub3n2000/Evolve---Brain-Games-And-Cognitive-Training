using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NthLetterGame : MonoBehaviour
{
    [SerializeField]
    GameObject answerObject;
    [SerializeField]
    GameObject questionObject;
    [SerializeField]
    Text infoText;
    [SerializeField]
    Text questionText;
    SessionManager sessionManager;
    int maxRounds = 10;
    int currentRound;
    List<int> scores;
    ScoreKeeper scoreKeeper;
    string[] words = new string[187] {"about","after","again","air","all","along","also","an","and","another","any","are","around","as",
"at","away","back","be","because","been","before","below","between","both","but","by","came","can","come","could","day","did",
"different","do","does","dont","down","each","end","even","every","few","find","first","for","found","from","get","give","go","good","great",
"had","has","have","he","help","her","here","him","his","home","house","how","I","if","in","into","is","it","its","just","know","large","last",
"left","like","line","little","long","look","made","make","man","many","may","me","men","might","more","most","Mr","must","my","name","never","new",
"next","no","not","now","number","of","off","old","on","one","only","or","other","our","out","over","own","part","people","place","put","read","right",
"said","same","saw","say","see","she","should","show","small","so","some","something","sound","still","such","take","tell","than","that","the","them",
"then","there","these","they","thing","think","this","those","thought","three","through","time","to","together","too","two","under","up","us","use","very",
"want","water","way","we","well","went","were","what","when","where","which","while","who","why","will","with","word","work","world",
"would","write","year","you","your","was"};
    SaveLoader saveLoader;
    float timer = 0;
    bool runClock = true;
    int index;
    string currentString;
    [SerializeField]
    int timeTheyCanSeeSentence = 5;
    [SerializeField]
    Text endText;
    [SerializeField]
    GameObject endScreen;
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        endScreen.SetActive(false);
        scores = new List<int>();
        answerObject.SetActive(false);
        questionObject.SetActive(true);
        questionText.text = GenerateRandomString(scoreKeeper.memoryLevel + 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (runClock == true)
        {
            timer += Time.deltaTime;
        }
        if(timer > timeTheyCanSeeSentence)
        {
            runClock = false;
            timer = 0;
            currentString = RemoveWhiteSpace(questionText.text);
            questionObject.SetActive(false);
            answerObject.SetActive(true);
            index = Random.Range(1, currentString.Length - 1);
            infoText.text = "What is the " + index.ToString() + " letter in the sequence of words";
        }
    }

    string GenerateRandomString(int amountOfWords)
    {
        string randomString = "";
        for(int i = 0; i < amountOfWords; i++)
        {
            randomString += words[Random.Range(0, words.Length)] + " ";
        }
        return randomString;
    }

    string RemoveWhiteSpace(string stringForRemoving)
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        string fixedString = "";
        for(int i = 0; i < stringForRemoving.Length; i++)
        {
            if(letters.IndexOf(stringForRemoving[i]) != -1)
            {
                fixedString += stringForRemoving[i];
            }
        }
        return fixedString;
    }

    public void EndGame()
    {
        sessionManager.ContinueSession();
    }

    public void GuessLetter(string letter)
    {
        currentRound++;
        if (letter.ToCharArray()[0] == currentString[index - 1])
        {
            scoreKeeper.memoryPoints += 100;
            scores.Add(100);
            saveLoader.SaveGameData();
        }
        else
        { scores.Add(0); }
        if (currentRound >= 10)
        {
            questionObject.SetActive(false);
            answerObject.SetActive(false);
            endScreen.SetActive(true);
            endText.text = "";
            for (int i = 1; i < maxRounds + 1; i++)
            {
                endText.text += "Round :" + i + " | Score " + scores[i - 1];
                endText.text += "\n";
            }
            endText.text += "\n";
            endText.text += "Well Done! Keep Improving :)";
        }
        else
        {
            answerObject.SetActive(false);
            questionObject.SetActive(true);
            questionText.text = GenerateRandomString(scoreKeeper.memoryLevel + 3);
            runClock = true;
        }
        if (scoreKeeper.memoryPoints >= scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
        {
            scoreKeeper.memoryLevel++;
        }
    }
}
