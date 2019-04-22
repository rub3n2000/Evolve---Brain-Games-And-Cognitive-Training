using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayBlockBreakerAndHangman : MonoBehaviour
{
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;

    [SerializeField]
    GameObject game2;

    public float speed = 0.8f;


    [SerializeField]
    GameObject berry;

    [SerializeField]
    GameObject buttons;

    [SerializeField]
    GameObject game;
  
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    Text endscreenText;

    [SerializeField]
    Text firstLetter;
    [SerializeField]
    Text secondLetter;
    [SerializeField]
    Text thirdLetter;
    [SerializeField]
    Text fourthLetter;
    [SerializeField]
    Text fifthLetter;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Image life1;
    [SerializeField]
    Image life2;
    [SerializeField]
    Image life3;
    [SerializeField]
    Image life4;
    [SerializeField]
    Image life5;
    [SerializeField]
    Image life6;
    [SerializeField]
    Image life7;
    [SerializeField]
    Image life8;
    [SerializeField]
    Image life9;
    [SerializeField]
    Image life10;

    string word;

    [SerializeField]
    GameObject intro;

    [SerializeField]
    string[] fiveLetterWords;

    int totalScore = 0;

    [SerializeField]
    Transform[] spawnPoints;

    float spawnFrequency = 4f;
    AntonymsSfxManager antonymsSfxManager;
    float timer = 0;

    public void EndGame()
    {
        endScreen.SetActive(true);
        buttons.SetActive(false);
        game.SetActive(false);
        game2.SetActive(false);
        
        endscreenText.text = "Total score " + totalScore;
        scoreKeeper.multitaskingPoints += totalScore;
        if (scoreKeeper.multitaskingPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.multitaskingLevel + 1])
        {
            scoreKeeper.multitaskingLevel++;
        }
        saveLoader.SaveGameData();

    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    public void Guess(string character)
    {
        if(word.IndexOf(character) != -1)
        {
            antonymsSfxManager.PlayAudio(true);
            if (word.IndexOf(character) == 0)
            {
                firstLetter.text = word[0].ToString().ToUpper();
            }
            if (word.IndexOf(character) == 1)
            {
                secondLetter.text = word[1].ToString().ToUpper();
            }
            if (word.IndexOf(character) == 2)
            {
                thirdLetter.text = word[2].ToString().ToUpper();
            }
            if (word.IndexOf(character) == 3)
            {
                fourthLetter.text = word[3].ToString().ToUpper();
            }
            if (word.IndexOf(character) == 4)
            {
                fifthLetter.text = word[4].ToString().ToUpper();
            }
            if(firstLetter.text != "_" && secondLetter.text != "_" && thirdLetter.text != "_" && fourthLetter.text != "_" && fifthLetter.text != "_")
            {
                totalScore += 1000;
                scoreKeeper.multitaskingPoints += 1000;
                speed = 0.8f;
                spawnFrequency = 4f;
                Camera.main.GetComponent<Animator>().SetTrigger("Shake");
                if (scoreKeeper.multitaskingPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.multitaskingLevel +1])
                {
                    scoreKeeper.multitaskingLevel++;
                }
                saveLoader.SaveGameData();
                word = fiveLetterWords[Random.Range(0, fiveLetterWords.Length)];
                life1.color = Color.green;
                life2.color = Color.green;
                life3.color = Color.green;
                life4.color = Color.green;
                life5.color = Color.green;
                life6.color = Color.green;
                life7.color = Color.green;
                life8.color = Color.green;
                life9.color = Color.green;
                life10.color = Color.green;
                firstLetter.text = "_";
                secondLetter.text = "_";
                thirdLetter.text = "_";
                fourthLetter.text = "_";
                fifthLetter.text = "_";
            }
        }
        else
        {
            antonymsSfxManager.PlayAudio(false);
            if (life1.color == Color.green)
            {
                life1.color = Color.red;
            }
            else if (life2.color == Color.green && life1.color == Color.red)
            {
                life2.color = Color.red;
            }
            else if (life3.color == Color.green && life2.color == Color.red && life1.color == Color.red)
            {
                life3.color = Color.red;
                
            }
            else if(life4.color == Color.green && life3.color == Color.red)
            {
                life4.color = Color.red;
            }
            else if(life5.color == Color.green && life4.color == Color.red)
            {
                life5.color = Color.red;
                
            }
            else if(life5.color == Color.red && life6.color == Color.green)
            {
                life6.color = Color.red;
            }
            else if (life6.color == Color.red && life7.color == Color.green)
            {
                life7.color = Color.red;
            }
            else if (life7.color == Color.red && life8.color == Color.green)
            {
                life8.color = Color.red;
            }
            else if (life8.color == Color.red && life9.color == Color.green)
            {
                life9.color = Color.red;
            }
            else
            {
                life10.color = Color.red;
                EndGame();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
        endScreen.SetActive(false);
        game.SetActive(false);
        buttons.SetActive(false);
        timer = -1000000;
        Invoke("StartGame", 7.5f);
    }

    void StartGame()
    {
        buttons.SetActive(true);
        game.SetActive(true);
        intro.SetActive(false);
        timer = 0;
        word = fiveLetterWords[Random.Range(0, fiveLetterWords.Length)];
        life1.color = Color.green;
        life2.color = Color.green;
        life3.color = Color.green;
        life4.color = Color.green;
        life5.color = Color.green;
        life6.color = Color.green;
        life7.color = Color.green;
        life8.color = Color.green;
        life9.color = Color.green;
        life10.color = Color.green;
        firstLetter.text = "_";
        secondLetter.text = "_";
        thirdLetter.text = "_";
        fourthLetter.text = "_";
        fifthLetter.text = "_";
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > spawnFrequency)
        {
            int transformIndex = Random.Range(0, 5);
            Instantiate(berry, spawnPoints[transformIndex].position, Quaternion.identity);
            timer = 0;
            if (spawnFrequency > 0.8f)
            {
                spawnFrequency -= 0.04f;
            }
            if (speed < 1)
            {
                speed += 0.01f;
            }
        }
        scoreText.text = totalScore.ToString();
        timer += Time.deltaTime;
    }
}
