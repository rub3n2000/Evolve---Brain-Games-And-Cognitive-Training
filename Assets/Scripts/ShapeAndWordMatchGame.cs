using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeAndWordMatchGame : MonoBehaviour
{
    int maxRounds = 10;
    int currentRound = 0;
    [SerializeField]
    GameObject gameContainer;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    [SerializeField]
    GameObject endScreenContainer;
    [SerializeField]
    GameObject image;
    [SerializeField]
    Text endscreenText;
    Color originalColor;
    bool canAnswer = false;
    float timer = 0;
    List<int> scores;
    List<float> times;
    int randomNumber;
    [SerializeField]
    Color[] colors;

    [SerializeField]
    Color green;
    [SerializeField]
    Color red;

    [SerializeField]
    Sprite circle;
    [SerializeField]
    Sprite square;
    [SerializeField]
    Sprite star;
    [SerializeField]
    Sprite hexagon;
    [SerializeField]
    Sprite triangle;
    List<Sprite> shapes;
    [SerializeField]
    Text shapeText;
    [SerializeField]
    Text timerText;
    [SerializeField]
    SpriteRenderer background;
    int shapeIndex = 0;
    bool gameIsGoing = false;
    List<string> shapeStrings;
    bool hasChanged = false;
    AntonymsSfxManager antonymsSfxManager;

    // Start is called before the first frame update
    void Start()
    {
        scores = new List<int>();
        times = new List<float>();
        shapeStrings = new List<string>();
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(true);
        originalColor = background.material.color;
        image.SetActive(true);
        shapes = new List<Sprite>();
        shapes.Add(circle);
        shapes.Add(square);
        shapes.Add(star);
        shapes.Add(hexagon);
        shapes.Add(triangle);
        shapeStrings.Add("Circle");
        shapeStrings.Add("Square");
        shapeStrings.Add("Star");
        shapeStrings.Add("Hexagon");
        shapeStrings.Add("Triangle");
        SetupGame();
    }



    void SetupGame()
    {
        gameContainer.SetActive(true);
        background.material.color = originalColor;
        int modifier = Random.Range(0, 2);
        if(modifier == 0)
        {
            hasChanged = false;
            int change = Random.Range(0, 2);
            int colorIndex = Random.Range(0, colors.Length);
            if (change == 0)
            {
                image.GetComponent<Image>().color = colors[colorIndex];
                if (colorIndex == 0)
                {
                    shapeText.color = colors[colorIndex + 1];
                }
                else
                {
                    shapeText.color = colors[colorIndex - 1];
                }
                shapeIndex = Random.Range(0, shapes.Count);
                image.GetComponent<Image>().sprite = shapes[shapeIndex];
                shapeText.text = shapeStrings[shapeIndex];
            }
            else
            {
                image.GetComponent<Image>().color = colors[colorIndex];
                shapeText.color = colors[colorIndex];
                
                shapeIndex = Random.Range(0, shapes.Count);
                image.GetComponent<Image>().sprite = shapes[shapeIndex];
                if (shapeIndex == 0)
                {
                    shapeText.text = shapeStrings[shapeIndex + 1];
                }
                else
                {
                    shapeText.text = shapeStrings[shapeIndex - 1];
                }
            }
        }
        else
        {
            hasChanged = true;
            int colorIndex = Random.Range(0, colors.Length);
            image.GetComponent<Image>().color = colors[colorIndex];
            shapeText.color = colors[colorIndex];
            shapeIndex = Random.Range(0, shapes.Count);
            image.GetComponent<Image>().sprite = shapes[shapeIndex];
            shapeText.text = shapeStrings[shapeIndex];
        }
        gameIsGoing = true;
    }

    public void MakeAGuess(bool _hasChanged)
    {
        gameContainer.SetActive(false);
        if (currentRound > maxRounds)
        {
            EndGame();
        }
        else
        {
            currentRound++;
            gameIsGoing = false;

            if (_hasChanged == hasChanged)
            {
                Camera.main.GetComponent<Animator>().SetTrigger("Shake");
                background.material.color = green;
                antonymsSfxManager.PlayAudio(true);
                int scoreToAdd = 100 + (int)(100 - timer);
                times.Add(timer);
                timer = 0;
                scores.Add(scoreToAdd);
                scoreKeeper.logicPoints += scoreToAdd;
            }
            else
            {
                background.material.color = red;
                times.Add(timer);
                antonymsSfxManager.PlayAudio(false);
                timer = 0;
                scores.Add(0);
            }
            saveLoader.SaveGameData();
            Invoke("SetupGame", 1f);
        }
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    void EndGame()
    {
        endScreenContainer.SetActive(true);
        gameContainer.SetActive(false);
        endscreenText.text = "";
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i] == 0)
            {
                endscreenText.text += "Wrong answer | " + " Time : " + times[i].ToString("0.00") + "\n";
            }
            else
            {
                endscreenText.text += "Correct answer | " + "Time : " + times[i].ToString("0.00") + "\n";
            }
        }
        endscreenText.text += "\n" + " Well Done, Keep Improving!";
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsGoing)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("0.0");
        }
    }
}
