using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowManySyllables : MonoBehaviour
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
    int wordIndex;

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

            switch (wordToDefinitions[wordIndex].syllableCount)
            {
                case 2:
                    button1Text.color = Color.green; button2Text.color = Color.red; button3Text.color = Color.red; button4Text.color = Color.red;
                    break;
                case 3:
                    button1Text.color = Color.red; button2Text.color = Color.green; button3Text.color = Color.red; button4Text.color = Color.red;
                    break;
                case 4:
                    button1Text.color = Color.red; button2Text.color = Color.red; button3Text.color = Color.green; button4Text.color = Color.red;
                    break;
                case 5:
                    button1Text.color = Color.red; button2Text.color = Color.red; button3Text.color = Color.red; button4Text.color = Color.green;
                    break;
            }


            if (id == wordToDefinitions[wordIndex].syllableCount)
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
                endscreenText.text += "Round " + i + " | Wrong |" + "\n";
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
        wordIndex = Random.Range(0, wordToDefinitions.Length);
        wordText.text = wordToDefinitions[wordIndex].words;
    }

    [System.Serializable]
    struct WordToDefinition
    {
        public string words;
        public int syllableCount;
    }
}
