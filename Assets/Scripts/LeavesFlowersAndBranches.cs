using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeavesFlowersAndBranches : MonoBehaviour
{
    int maxRounds = 3;
    int currentRound = 0;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
 
    [SerializeField]
    Text questionText;

    [SerializeField]
    Image[] images;

    [SerializeField]
    Sprite leaf;
    [SerializeField]
    Sprite flower;
    [SerializeField]
    Sprite branch;

    Color originalColor;
    int currentAnswer;
    [SerializeField]
    GameObject game;
    [SerializeField]
    GameObject game2;
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    Text endscreenText;

    [SerializeField]
    Image background;

    List<int> scores;
    bool canAnswer = true;

    int leafsCount;
    int flowersCount;
    int branchesCount;

    [SerializeField]
    int[] timeToWatch;

    List<string> correctAnswers;

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        endScreen.SetActive(false);
        game.SetActive(false);
        game2.SetActive(false);
        originalColor = background.color;
        scores = new List<int>();
        correctAnswers = new List<string>();
        SetupRound();
    }

    public void Guess(int id)
    {
        if (canAnswer)
        {
            canAnswer = false;
            if(id != 11)
            {
                if (id == currentAnswer)
                {
                    scoreKeeper.memoryPoints += 400;
                    scores.Add(400);
                    if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
                    {
                        scoreKeeper.memoryLevel++;
                    }
                    saveLoader.SaveGameData();
                    background.color = Color.green;
                }
                else { scores.Add(0); background.color = Color.red; }
            }
            else
            {
                if(currentAnswer > 9)
                {
                    scoreKeeper.memoryPoints += 400;
                    scores.Add(400);
                    if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
                    {
                        scoreKeeper.memoryLevel++;
                    }
                    saveLoader.SaveGameData();
                    background.color = Color.green;
                }
                else
                {
                    scores.Add(0); background.color = Color.red;
                }
            }
            Invoke("SetupRound", 1);
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
        game2.SetActive(false);
        endscreenText.text = "";
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] == 0)
            {
                endscreenText.text += "Round " + (i + 1).ToString() + " | Wrong |" + "\n";
            }
            else
            {
                endscreenText.text += "Round " + (i + 1).ToString() + " | Correct | " + scores[i] + " points" + "\n";
            }
        }
        endscreenText.text += "\n" + " Well Done, Keep Improving!";
    }

 
    void SetupRound()
    {
        background.color = originalColor;
        flowersCount = 0;
        branchesCount = 0;
        leafsCount = 0;
        
        currentRound++;
        game2.SetActive(true);
        game.SetActive(false);
        for(int i = 0; i < images.Length; i++)
        {
            int index = Random.Range(1, 4);
            if(index == 1)
            {
                images[i].sprite = leaf;
                leafsCount++;
            }
            else if(index == 2)
            {
                images[i].sprite = branch;
                branchesCount++;
            }
            else
            {
                images[i].sprite = flower;
                flowersCount++;
            }
        }

        if(currentRound > maxRounds)
        {
            EndGame();
        }


        Invoke("LetThemAnswer", timeToWatch[scoreKeeper.memoryLevel]);

    }
  
    void LetThemAnswer()
    {
        game2.SetActive(false);
        game.SetActive(true);
        canAnswer = true;
        int index = Random.Range(1, 4);
        if(index == 1)
        {
            questionText.text = "How Many Leafs were there?";
            currentAnswer = leafsCount;
        }
        else if(index == 2)
        {
            questionText.text = "How Many Branches were there?";
            currentAnswer = branchesCount;
        }
        else
        {
            questionText.text = "How Many Flowers were there?";
            currentAnswer = flowersCount;
        }
    }
}
