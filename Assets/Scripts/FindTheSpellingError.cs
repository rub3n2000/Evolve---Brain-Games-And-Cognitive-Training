using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindTheSpellingError : MonoBehaviour
{

    [SerializeField]
    GameObject gameContainer;
   
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
    Text[] texts;

    int guessId = 0;

    int correctId = 0;

    [SerializeField]
    AntonymsSfxManager antonymsSfxManager;


    [HideInInspector]
    public float timer = 0;

    string[] englishWords = new string[187] {"about","after","again","air","all","along","also","an","and","another","any","are","around","as",
"at","away","back","be","because","been","before","below","between","both","but","by","came","can","come","could","day","did",
"different","do","does","dont","down","each","end","even","every","few","find","first","for","found","from","get","give","go","good","great",
"had","has","have","he","help","her","here","him","his","home","house","how","I","if","in","into","is","it","its","just","know","large","last",
"left","like","line","little","long","look","made","make","man","many","may","me","men","might","more","most","Mr","must","my","name","never","new",
"next","no","not","now","number","of","off","old","on","one","only","or","other","our","out","over","own","part","people","place","put","read","right",
"said","same","saw","say","see","she","should","show","small","so","some","something","sound","still","such","take","tell","than","that","the","them",
"then","there","these","they","thing","think","this","those","thought","three","through","time","to","together","too","two","under","up","us","use","very",
"want","water","way","we","well","went","were","what","when","where","which","while","who","why","will","with","word","work","world",
"would","write","year","you","your","was"};

    int maxRounds = 12;

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(false);
        intro.SetActive(true);
        Invoke("StartNewRound", 3f);
    }

    public void StartNewRound()
    {
        for(int i = 0; i < texts.Length;i++)
        {
            texts[i].color = Color.white;
        }
        gameContainer.SetActive(true);
        intro.SetActive(false);
        int randomNmbr = Random.Range(0, texts.Length);
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].text = englishWords[Random.Range(0, englishWords.Length)];
        }
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        char[] chars = texts[randomNmbr].text.ToCharArray();
        int randomNumber = Random.Range(0, chars.Length);
        string newText = "";
        for(int i = 0; i < chars.Length; i++)
        {
            if(i == randomNumber)
            {
                char oldCharI = chars[i];
                chars[i] = alphabet[Random.Range(0, alphabet.Length)];
                while(chars[i] == oldCharI)
                {
                    chars[i] = alphabet[Random.Range(0, alphabet.Length)];
                }
            }
            newText += chars[i];
        }
        texts[randomNmbr].text = newText;
        texts[randomNmbr].GetComponent<FindTheSpellingErrorTexts>().imTheError = true;
        currentRound++;
        if(currentRound > maxRounds)
        {
            EndGame();
        }
        correctId = randomNmbr;
        timer = 0;
    }

    public void Guess(int id)
    {
        guessId = id;
        if (id == correctId)
        {
            totalScore += 100 - (int)(timer * 5) + 50;
            ColorIt();
            Camera.main.GetComponent<Animator>().SetTrigger("Shake");
            antonymsSfxManager.PlayAudio(true);

        }
        else
        {
            totalScore -= 5;
            antonymsSfxManager.PlayAudio(false);
            ColorIt();
        }
    }

    void ColorIt()
    {
        if(guessId == correctId)
        {
            texts[guessId].color = Color.green;
        }
        else
        {
            texts[guessId].color = Color.red;
        }
        Invoke("StartNewRound", 1f);
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    void EndGame()
    {
        endScreenContainer.SetActive(true);
        gameContainer.SetActive(false);
        endscreenText.text = "Total score " + totalScore;
        scoreKeeper.languagePoints += totalScore;
        if (scoreKeeper.languagePoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.languageLevel + 1])
        {
            scoreKeeper.languageLevel++;
        }
        saveLoader.SaveGameData();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("0.0");
    }
}
