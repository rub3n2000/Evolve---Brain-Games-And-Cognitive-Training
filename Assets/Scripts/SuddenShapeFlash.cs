using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuddenShapeFlash : MonoBehaviour
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
    GameObject buttonsContainer;
    [SerializeField]
    GameObject image;
    [SerializeField]
    Image button1Image;
    [SerializeField]
    Image button2Image;
    [SerializeField]
    Image button3Image;
    [SerializeField]
    Image button4Image;
    [SerializeField]
    Image button5Image;
    int currentAnswerId;
    [SerializeField]
    Text endscreenText;
    Color originalColor;
    bool canAnswer = false;
    float timer = 0;
    List<int> scores;
    int randomNumber;
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
    Text explanationText;
    float timeAllowedToWatchShape;
    AntonymsSfxManager antonymsSfxManager;


    // Start is called before the first frame update
    void Start()
    {
        scores = new List<int>();
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(true);
        originalColor = button1Image.color;
        image.SetActive(false);
        buttonsContainer.SetActive(false);
        switch(scoreKeeper.concentrationLevel)
        {
            case 0: timeAllowedToWatchShape = 1f; break;
            case 1: timeAllowedToWatchShape = 0.8f; break;
            case 2: timeAllowedToWatchShape = 0.7f; break;
            case 3: timeAllowedToWatchShape = 0.6f; break;
            default: timeAllowedToWatchShape = 0.5f; break;
        }
        shapes = new List<Sprite>();
        shapes.Add(circle);
        shapes.Add(square);
        shapes.Add(star);
        shapes.Add(hexagon);
        shapes.Add(triangle);
        SetupRound();
    }

    void SetupRound()
    {
        button1Image.color = originalColor; button2Image.color = originalColor; button3Image.color = originalColor;
        button4Image.color = originalColor; button5Image.color = originalColor;
        buttonsContainer.SetActive(false);
        if(currentRound == 10)
        {
            EndGame();
        }
        randomNumber = Random.Range(1, 6);
        explanationText.text = "Wait For The Shape Flashing";
        canAnswer = false;
        currentRound++;
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
                endscreenText.text += "Round " + (i + 1).ToString() + " | Wrong |" + "\n";
            }
            else
            {
                endscreenText.text += "Round " + (i + 1).ToString() + " | Correct | " + scores[i] + " points" + "\n";
            }
        }
        endscreenText.text += "\n" + " Well Done, Keep Improving!";
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    public void Guess(int id)
    {
        if (canAnswer)
        {
            
            switch (currentAnswerId)
            {
                case 0:
                    button1Image.color = Color.green; button2Image.color = Color.red; button3Image.color = Color.red; button4Image.color = Color.red; button5Image.color = Color.red;
                    break;
                case 1:
                    button1Image.color = Color.red; button2Image.color = Color.green; button3Image.color = Color.red; button4Image.color = Color.red; button5Image.color = Color.red;
                    break;
                case 2:
                    button1Image.color = Color.red; button2Image.color = Color.red; button3Image.color = Color.green; button4Image.color = Color.red; button5Image.color = Color.red;
                    break;
                case 3:
                    button1Image.color = Color.red; button2Image.color = Color.red; button3Image.color = Color.red; button4Image.color = Color.green; button5Image.color = Color.red;
                    break;
                case 4:
                    button1Image.color = Color.red; button2Image.color = Color.red; button3Image.color = Color.red; button4Image.color = Color.red; button5Image.color = Color.green;
                    break;
            }

            if (id == currentAnswerId)
            {
                antonymsSfxManager.PlayAudio(true);
                Camera.main.GetComponent<Animator>().SetTrigger("Shake");
                scoreKeeper.concentrationPoints += 100;
                scores.Add(100);
                if (scoreKeeper.concentrationPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.concentrationLevel + 1])
                {
                    scoreKeeper.concentrationLevel++;
                }
                saveLoader.SaveGameData();
            }
            else { scores.Add(0); antonymsSfxManager.PlayAudio(false); }
            Invoke("SetupRound", 1);
        }
    }

    void Part2()
    {
                image.SetActive(false);
                buttonsContainer.SetActive(true);
                explanationText.text = "Which Shape Was It?";
                timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canAnswer)
        {
            timer += Time.deltaTime;
            if(timer > randomNumber && !canAnswer)
            {
                image.SetActive(true);
                currentAnswerId = Random.Range(0, 5);
                image.GetComponent<Image>().sprite = shapes[currentAnswerId];
                canAnswer = true;
                
                Invoke("Part2", timeAllowedToWatchShape);
            }
        }
    }
}
