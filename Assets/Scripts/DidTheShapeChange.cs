using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DidTheShapeChange : MonoBehaviour
{
    [SerializeField]
    GameObject gameContainer;

    [SerializeField]
    GameObject endScreenContainer;
    [SerializeField]
    Text endscreenText;
   

    float timer = 0;

    int currentRound = 0;
    int maxRounds = 10;

    [HideInInspector]
    public int totalScore = 0;
    [SerializeField]
    Text timerText;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;

    [SerializeField]
    AntonymsSfxManager antonymsSfxManager;


    public List<int> scores;
    public List<float> times;
    
    [SerializeField]
    GameObject buttons;

    [SerializeField]
    Image shapeImage;

    [SerializeField]
    Sprite[] shapes;

    [SerializeField]
    SpriteRenderer backGround;

    bool hasChanged = false;

    int spriteIndex = 0;

    Color originalColor;

    // Start is called before the first frame update
    private void Awake()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        originalColor = backGround.material.color;
    }
    void Start()
    {


        List<int> scores = new List<int>();
        List<float> times = new List<float>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(true);
       
        StartNewRound();
        Invoke("StartNewRound2",1f);
    }

    void StartNewRound2()
    {
        currentRound++;
        backGround.material.color = originalColor;
     
        buttons.SetActive(true);
        
        
        int rndNmbr = Random.Range(0, 2);
        if (rndNmbr == 0)
        {
            hasChanged = false;
        }
        else
        {
            hasChanged = true;
            int spriteIndex2 = Random.Range(0, shapes.Length);
            while (spriteIndex2 == spriteIndex)
            {
                spriteIndex2 = Random.Range(0, shapes.Length);
            }
            spriteIndex = spriteIndex2;
            shapeImage.sprite = shapes[spriteIndex];
        }

        Debug.Log("hi");

        if (currentRound > maxRounds)
        {
            EndGame();
        }
            timer = 0;
    }

    public void MakeAGuess(int guess)
    {
        if ((hasChanged && guess == 1) || (!hasChanged && guess == 0))
        {
            int score = 10 + (int)(100 - (timer * 10));
            antonymsSfxManager.PlayAudio(true);
            scores.Add(score);
            times.Add(timer);
            scoreKeeper.memoryPoints += score;
            if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
            {
                scoreKeeper.memoryLevel++;
            }
            saveLoader.SaveGameData();
            backGround.material.color = Color.green;
            Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        }
        else
        {
            scoreKeeper.memoryPoints += 0;
            scores.Add(0);
            antonymsSfxManager.PlayAudio(false);
            times.Add(timer);
            if (scoreKeeper.memoryPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1])
            {
                scoreKeeper.memoryLevel++;
            }
            saveLoader.SaveGameData();
            backGround.material.color = Color.red;
            
        }
        buttons.SetActive(false);
        Invoke("StartNewRound2", 0.1f);
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }



    void EndGame()
    {
        endScreenContainer.SetActive(true);
        gameContainer.SetActive(false);

       
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

    void StartNewRound()
    {
        buttons.SetActive(false);
       
        spriteIndex = Random.Range(0, shapes.Length);
        shapeImage.sprite = shapes[spriteIndex];
        
     
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = timer.ToString("0.0");
        timer += Time.deltaTime;
    }
}
