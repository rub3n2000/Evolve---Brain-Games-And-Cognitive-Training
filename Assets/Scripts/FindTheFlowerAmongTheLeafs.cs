using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTheFlowerAmongTheLeafs : MonoBehaviour
{
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

    int currentRound = 1;

    [HideInInspector]
    public int totalScore = 0;
    [SerializeField]
    Text timerText;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    [SerializeField]
    Transform[] spawnPoints;

    [SerializeField]
    GameObject leaf;
    [SerializeField]
    GameObject flower;
    public List<int> scores;
    public List<float> times;

    List<GameObject> spawnedObjects;

    int guessId = 0;

    public bool gameIsGoing = false;

    int correctId = 0;


    [HideInInspector]
    public float timer = 0;


    int maxRounds = 11;

    // Start is called before the first frame update
    void Start()
    {
        List<int> scores = new List<int>();
        List<float> times = new List<float>();
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        spawnedObjects = new List<GameObject>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(false);
        gameContainer2.SetActive(false);
        intro.SetActive(true);
        Invoke("StartNewRound", 3f);
    }

    public void StartNewRound()
    {
        gameIsGoing = true;
        gameContainer2.SetActive(true);
        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            Destroy(spawnedObjects[i]);
        }
        gameContainer.SetActive(true);
        intro.SetActive(false);
        int randomNmbr = Random.Range(0, spawnPoints.Length);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == randomNmbr)
            {
                spawnedObjects.Add(Instantiate(flower, spawnPoints[i].transform.position, Quaternion.identity, spawnPoints[i]));
            }
            else
            {
                spawnedObjects.Add(Instantiate(leaf, spawnPoints[i].transform.position, Quaternion.identity, spawnPoints[i]));
            }
        }

        currentRound++;
        if (currentRound > maxRounds)
        {
            EndGame();
        }
        correctId = randomNmbr;
        timer = 0;
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    void EndGame()
    {
        endScreenContainer.SetActive(true);
        gameContainer.SetActive(false);
        gameContainer2.SetActive(false);
       
        scoreKeeper.reactionPoints += totalScore;
        if (scoreKeeper.reactionPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.reactionLevel + 1])
        {
            scoreKeeper.reactionLevel++;
        }
        saveLoader.SaveGameData();
        endscreenText.text = "";
        for (int i = 0; i < times.Count; i++)
        {
            endscreenText.GetComponent<Text>().text += "Round " + (i + 1).ToString() + " : ";
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

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("0.0");
    }
}

