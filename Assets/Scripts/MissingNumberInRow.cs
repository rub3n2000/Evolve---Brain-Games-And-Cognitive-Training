using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissingNumberInRow : MonoBehaviour
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

    [SerializeField]
    Text timerText;

    float timer = 0;

    List<float> times;

    List<int> scores;
    bool canAnswer = true;
    [SerializeField]
    WordToDefinition[] wordToDefinitions;

    List<float> correctAnswers;

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
        correctAnswers = new List<float>();
        SetupRound();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        timerText.text = timer.ToString("0.0");
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
                int score = (int)(1000 - (timer * 10));
                scoreKeeper.languagePoints +=score;
                scores.Add(score);
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
        game.SetActive(false);
        endScreen.SetActive(true);
        
        for (int i = 0; i < times.Count; i++)
        {
            endscreenText.text += "Round " + (i + 1).ToString() + " : ";
            if (scores[i] == 0)
            {
                endscreenText.text += "Wrong answer | " + " Time : " + times[i].ToString("0.00") + "\n";
            }
            else
            {
                endscreenText.text += "Correct answer | " + "Time : " + times[i].ToString("0.00") + "\n";
            }
        }

        endscreenText.text += "\n Great Job, keep trying to \n improve times and scores!";
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
        wordText.text = "";
        for(int i = 0; i < wordToDefinitions[wordIndex].numberRow.Length;i++)
        {
            wordText.text += wordToDefinitions[wordIndex].numberRow[i].ToString() + " - ";
        }
        currentAnswerId = Random.Range(0, 4);
        correctAnswers.Add(wordToDefinitions[wordIndex].missingNumber);
        switch (currentAnswerId)
        {
            case 0:
                button1Text.text = wordToDefinitions[wordIndex].missingNumber.ToString();
                int randomIndex11 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex11 == wordIndex || wordToDefinitions[randomIndex11].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex11 = Random.Range(0, wordToDefinitions.Length);
                }
                button2Text.text = wordToDefinitions[randomIndex11].missingNumber.ToString();
                int randomIndex12 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex12 == randomIndex11 || randomIndex12 == wordIndex || wordToDefinitions[randomIndex12].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex12 = Random.Range(0, wordToDefinitions.Length);
                }
                button3Text.text = wordToDefinitions[randomIndex12].missingNumber.ToString();
                int randomIndex13 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex13 == wordIndex || randomIndex13 == randomIndex12 || randomIndex13 == randomIndex11 || wordToDefinitions[randomIndex13].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex13 = Random.Range(0, wordToDefinitions.Length);
                }
                button4Text.text = wordToDefinitions[randomIndex13].missingNumber.ToString();
                break;
            case 1:
                button2Text.text = wordToDefinitions[wordIndex].missingNumber.ToString();
                int randomIndex21 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex21 == wordIndex || wordToDefinitions[randomIndex21].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex21 = Random.Range(0, wordToDefinitions.Length);
                }
                button1Text.text = wordToDefinitions[randomIndex21].missingNumber.ToString();
                int randomIndex22 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex22 == wordIndex || randomIndex22 == randomIndex21 || wordToDefinitions[randomIndex22].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex22 = Random.Range(0, wordToDefinitions.Length);
                }
                button3Text.text = wordToDefinitions[randomIndex22].missingNumber.ToString();
                int randomIndex23 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex23 == wordIndex || randomIndex23 == randomIndex22 || randomIndex23 == randomIndex21 || wordToDefinitions[randomIndex23].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex23 = Random.Range(0, wordToDefinitions.Length);
                }
                button4Text.text = wordToDefinitions[randomIndex23].missingNumber.ToString();
                break;
            case 2:
                button3Text.text = wordToDefinitions[wordIndex].missingNumber.ToString();
                int randomIndex31 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex31 == wordIndex || wordToDefinitions[randomIndex31].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex31 = Random.Range(0, wordToDefinitions.Length);
                }
                button1Text.text = wordToDefinitions[randomIndex31].missingNumber.ToString();
                int randomIndex32 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex32 == wordIndex || randomIndex32 == randomIndex31 || wordToDefinitions[randomIndex32].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex32 = Random.Range(0, wordToDefinitions.Length);
                }
                button2Text.text = wordToDefinitions[randomIndex32].missingNumber.ToString();
                int randomIndex33 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex33 == wordIndex || randomIndex33 == randomIndex32 || randomIndex33 == randomIndex31 || wordToDefinitions[randomIndex33].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex33 = Random.Range(0, wordToDefinitions.Length);
                }
                button4Text.text = wordToDefinitions[randomIndex33].missingNumber.ToString();
                break;
            case 3:
                button4Text.text = wordToDefinitions[wordIndex].missingNumber.ToString();
                int randomIndex41 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex41 == wordIndex || wordToDefinitions[randomIndex41].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex41 = Random.Range(0, wordToDefinitions.Length);
                }
                button1Text.text = wordToDefinitions[randomIndex41].missingNumber.ToString();
                int randomIndex42 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex42 == wordIndex || randomIndex42 == randomIndex41 || wordToDefinitions[randomIndex42].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex42 = Random.Range(0, wordToDefinitions.Length);
                }
                button3Text.text = wordToDefinitions[randomIndex42].missingNumber.ToString();
                int randomIndex43 = Random.Range(0, wordToDefinitions.Length);
                while (randomIndex43 == wordIndex || randomIndex43 == randomIndex42 || randomIndex43 == randomIndex41 || wordToDefinitions[randomIndex43].missingNumber == wordToDefinitions[wordIndex].missingNumber)
                {
                    randomIndex43 = Random.Range(0, wordToDefinitions.Length);
                }
                button2Text.text = wordToDefinitions[randomIndex43].missingNumber.ToString();
                break;
        }

    }

    [System.Serializable]
    struct WordToDefinition
    {
        public float[] numberRow;
        public float missingNumber;
    }
}
