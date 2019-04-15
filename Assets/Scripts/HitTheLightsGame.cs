using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitTheLightsGame : MonoBehaviour
{
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    [SerializeField]
    Transform[] spawnPoints;
    [HideInInspector]
    public int totalScore = 0;
    [SerializeField]
    Text scoreText;
    int currentRound = 1;
   
    int maxRounds = 12;

    [SerializeField]
    GameObject gameContainer;
    [SerializeField]
    GameObject gameContainer2;
    [SerializeField]
    GameObject endScreenContainer;
    [SerializeField]
    Text endscreenText;
    [SerializeField]
    GameObject intro;

    
    float timeToClick = 0.5f;

    float timer;

    bool gameIsGoing;

    [SerializeField]
    GameObject light;

    GameObject currentLight;


    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(false);
        gameContainer2.SetActive(false);
        intro.SetActive(true);
        switch (scoreKeeper.reactionLevel)
        {
            case 0:timeToClick = 1; break;
            case 1: timeToClick = 0.9f; break;
            case 2: timeToClick = 0.8f; break;
            case 3: timeToClick = 0.7f; break;
            case 4: timeToClick = 0.6f; break;
            default: timeToClick = 0.5f; break;
        }
        Invoke("StartRound", 3f);
    }
    void EndGame()
    {
        endScreenContainer.SetActive(true);
        gameContainer.SetActive(false);
        gameContainer2.SetActive(false);
        endscreenText.text = "Total score " + totalScore;
        scoreKeeper.reactionPoints += totalScore;
        if(scoreKeeper.reactionPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.reactionLevel+1])
        {
            scoreKeeper.reactionLevel++;
        }
        saveLoader.SaveGameData();
    }

    public void StartRound()
    {
        gameContainer.SetActive(true);
        gameContainer2.SetActive(true);
        intro.SetActive(false);
        int randomNmbr = Random.Range(0, spawnPoints.Length);
        currentLight = Instantiate(light, spawnPoints[randomNmbr].position, Quaternion.identity, spawnPoints[randomNmbr]);
        timer = timeToClick;
        currentRound++;
        if(currentRound >= maxRounds)
        {
            EndGame();
        }
        gameIsGoing = true;
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    // Update is called once per frame
    void Update()
    {
      if(gameIsGoing)
      {    
        timer -= Time.deltaTime;
      }

        scoreText.text = totalScore.ToString();

      if(timer < 0 && gameIsGoing)
      {
        Destroy(currentLight);
        StartRound();
      }

    }
}
