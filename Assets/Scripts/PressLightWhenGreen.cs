using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressLightWhenGreen : MonoBehaviour
{
    [SerializeField]
    GameObject gameContainer;

    [SerializeField]
    GameObject endScreenContainer;

    [SerializeField]
    Text endscreenText;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    GameObject introText;

    [SerializeField]
    Image light;

    float totalScore = 0;

    float timer = 0;

    float count = 1;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;

    bool canBePressed = false;

    float timeTillGreen = 0;

    [SerializeField]
    float timeRemainsGreen = 1f;

    AntonymsSfxManager antonymsSfxManager;

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        scoreText.text = "0";
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(false);
        Invoke("SetupGame", 3f);
    }

    void SetupGame()
    {
        if (count == 1)
        {
            gameContainer.SetActive(true);
            introText.SetActive(false);
        }
        timeTillGreen = Random.Range(1f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(count);
        timer += Time.deltaTime;
        if(timer > timeTillGreen && gameContainer.active == true)
        {
            SwitchColor();
            timer = 0;
        }
            
        if(timer > timeRemainsGreen && canBePressed)
        {
            SwitchColor();
            timer = 0;
        }
       

        if(count == 11)
        {
            gameContainer.SetActive(false);
            endScreenContainer.SetActive(true);
            endscreenText.text = "Total Score " + totalScore;
        }

    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    void SwitchColor()
    {
        if(canBePressed == true)
        {
            canBePressed = false;
            light.color = Color.red;
            count++;
            SetupGame();
        }
        else
        {
            light.color = Color.green;
            
            canBePressed = true;
        }
    }

    public void Press()
    {
        if(canBePressed)
        {
            scoreKeeper.concentrationPoints += 10;
            totalScore += 10;
            saveLoader.SaveGameData();
            SwitchColor();
            Camera.main.GetComponent<Animator>().SetTrigger("Shake");
            antonymsSfxManager.PlayAudio(true);
            scoreText.text = totalScore.ToString("0");
        }
        else
        {
            scoreKeeper.concentrationPoints += 5;
            totalScore -= 5;
            antonymsSfxManager.PlayAudio(false);
            saveLoader.SaveGameData();
            scoreText.text = totalScore.ToString("0");
        }
    }
}
