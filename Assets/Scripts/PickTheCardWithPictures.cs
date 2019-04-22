using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickTheCardWithPictures : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    GameObject cardWithPicture;
    [SerializeField]
    GameObject blankCard;
    [HideInInspector]
    public int tryCount = 0;
    [HideInInspector]
    public bool canTry = false;
    [HideInInspector]
    public int totalScore = 0;
    [SerializeField]
    Text scoreText;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    
    int currentRound = 1;
    [SerializeField]
    float cardSpeed;
    int maxRounds = 11;

    List<GameObject> cards;
    [SerializeField]
    GameObject gameContainer;
    [SerializeField]
    GameObject gameContainer2;
    [SerializeField]
    GameObject endScreenContainer;
    [SerializeField]
    Text endscreenText;
    int animIndex1;
    int animIndex2;
    int animIndex3;
    int animIndex4;
    int animIndex5;

    Animator animator1;
    Animator animator2;
    Animator animator3;
    Animator animator4;
    Animator animator5;

    [SerializeField]
    GameObject intro;

    float timer = -5;

    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        cards = new List<GameObject>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(false);
        gameContainer2.SetActive(false);
        intro.SetActive(true);
        Invoke("StartRound", 3f);

    }

    void EndGame()
    {
        endScreenContainer.SetActive(true);
        gameContainer.SetActive(false);
        gameContainer2.SetActive(false);
        if (scoreKeeper.concentrationPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.concentrationLevel + 1])
        {
            scoreKeeper.concentrationLevel++;
        }
        endscreenText.text = "Total score " + totalScore;
    }


    void StartRound()
    {
        if(currentRound == 1)
        {
            gameContainer.SetActive(true);
            gameContainer2.SetActive(true);
            intro.SetActive(false);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            Destroy(cards[i]);
        }
        cards.Clear();
        if (currentRound == maxRounds)
        {
            EndGame();
        }
        animIndex1 = Random.Range(1, 4);
        animIndex2 = Random.Range(1, 4);
        animIndex3 = Random.Range(1, 4);
        animIndex4 = Random.Range(1, 4);
        animIndex5 = Random.Range(1, 4);
        

        if (scoreKeeper.concentrationLevel == 0)
        {
            int cardWithPictureSpawnIndex = Random.Range(0, spawnPoints.Length);
            cards.Add(Instantiate(cardWithPicture, spawnPoints[cardWithPictureSpawnIndex].position, Quaternion.identity, spawnPoints[cardWithPictureSpawnIndex].transform));
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                if(i != cardWithPictureSpawnIndex)
                {
                    cards.Add(Instantiate(blankCard, spawnPoints[i].position, Quaternion.identity, spawnPoints[i].transform));
                }
            }
        }
        else if(scoreKeeper.concentrationLevel == 1)
        {
            int cardWithPictureSpawnIndex = Random.Range(0, spawnPoints.Length);
            int cardWithPictureSpawnIndex2 = Random.Range(0, spawnPoints.Length);
            while(cardWithPictureSpawnIndex2 == cardWithPictureSpawnIndex)
            {
                cardWithPictureSpawnIndex2 = Random.Range(0, spawnPoints.Length);
            }
            cards.Add(Instantiate(cardWithPicture, spawnPoints[cardWithPictureSpawnIndex].position, Quaternion.identity, spawnPoints[cardWithPictureSpawnIndex].transform));
            cards.Add(Instantiate(cardWithPicture, spawnPoints[cardWithPictureSpawnIndex2].position, Quaternion.identity, spawnPoints[cardWithPictureSpawnIndex2].transform));
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (i != cardWithPictureSpawnIndex && i!= cardWithPictureSpawnIndex2)
                {
                    cards.Add(Instantiate(blankCard, spawnPoints[i].position, Quaternion.identity, spawnPoints[i].transform));
                }
            }
        }
        else
        {
            int cardWithPictureSpawnIndex = Random.Range(0, spawnPoints.Length);
            int cardWithPictureSpawnIndex2 = Random.Range(0, spawnPoints.Length);
            int cardWithPictureSpawnIndex3 = Random.Range(0, spawnPoints.Length);
            while (cardWithPictureSpawnIndex2 == cardWithPictureSpawnIndex)
            {
                cardWithPictureSpawnIndex2 = Random.Range(0, spawnPoints.Length);
            }
            while(cardWithPictureSpawnIndex3 == cardWithPictureSpawnIndex || cardWithPictureSpawnIndex3 == cardWithPictureSpawnIndex2)
            {
                cardWithPictureSpawnIndex3 = Random.Range(0, spawnPoints.Length);
            }
            cards.Add(Instantiate(cardWithPicture, spawnPoints[cardWithPictureSpawnIndex].position, Quaternion.identity, spawnPoints[cardWithPictureSpawnIndex].transform));
            cards.Add(Instantiate(cardWithPicture, spawnPoints[cardWithPictureSpawnIndex2].position, Quaternion.identity, spawnPoints[cardWithPictureSpawnIndex2].transform));
            cards.Add(Instantiate(cardWithPicture, spawnPoints[cardWithPictureSpawnIndex3].position, Quaternion.identity, spawnPoints[cardWithPictureSpawnIndex3].transform));
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (i != cardWithPictureSpawnIndex && i != cardWithPictureSpawnIndex2 && i!= cardWithPictureSpawnIndex3)
                {
                    cards.Add(Instantiate(blankCard, spawnPoints[i].position, Quaternion.identity,spawnPoints[i].transform));
                }
            }
        }
        timer = -1;
        for(int i = 0; i <  cards.Count; i++)
        {
            if(cards[i].transform.parent.name == "SpawnPoint 1")
            {
                animator1 = cards[i].GetComponent<Animator>();
            }
            if (cards[i].transform.parent.name == "SpawnPoint 2")
            {
                animator2 = cards[i].GetComponent<Animator>();
            }
            if (cards[i].transform.parent.name == "SpawnPoint 3")
            {
                animator3 = cards[i].GetComponent<Animator>();
            }
            if (cards[i].transform.parent.name == "SpawnPoint 4")
            {
                animator4 = cards[i].GetComponent<Animator>();
            }
            if (cards[i].transform.parent.name == "SpawnPoint 5")
            {
                animator5 = cards[i].GetComponent<Animator>();
            }
            if(cards[i].gameObject.CompareTag("WithPicture"))
            {
                cards[i].GetComponent<SpriteRenderer>().material.color = Color.yellow;
            }
            if(!cards[i].gameObject.CompareTag("WithPicture"))
            {
                cards[i].GetComponent<SpriteRenderer>().material.color = Color.white;
            }
        }


        Invoke("StartGameProperly", 3f);
 
    }

    public void StartGameProperly()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].GetComponent<SpriteRenderer>().material.color = Color.red;
        }
        currentRound++;
        timer = 0;
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = totalScore.ToString();
        if(timer >= 0)
        {
            timer += Time.deltaTime;
        }
        if (timer < 2f && canTry == false && timer >= 0)
        {
            switch(animIndex1)
            {
                case 1:animator1.SetBool("Move1", true);break;
                case 2: animator1.SetBool("Move12", true); break;
                default: animator1.SetBool("Move13", true); break;
            }
            switch (animIndex2)
            {
                case 1: animator2.SetBool("Move2", true); break;
                case 2: animator2.SetBool("Move22", true); break;
                default: animator2.SetBool("Move23", true); break;
            }
            switch (animIndex3)
            {
                case 1: animator3.SetBool("Move3", true); break;
                case 2: animator3.SetBool("Move32", true); break;
                default: animator3.SetBool("Move33", true); break;
            }
            switch (animIndex4)
            {
                case 1: animator4.SetBool("Move4", true); break;
                case 2: animator4.SetBool("Move42", true); break;
                default: animator4.SetBool("Move43", true); break;
            }
            switch (animIndex5)
            {
                case 1: animator5.SetBool("Move5", true); break;
                case 2: animator5.SetBool("Move52", true); break;
                default: animator5.SetBool("Move53", true); break;
            }
        }

        if(scoreKeeper.concentrationLevel == 0 && tryCount == 1 && canTry)
        {
            canTry = false;
            tryCount = 0;
        }
        if (scoreKeeper.concentrationLevel == 1 && tryCount == 2 && canTry)
        {
            canTry = false;
            tryCount = 0;
        }
        if (scoreKeeper.concentrationLevel >= 2 && tryCount == 3 && canTry)
        {
            canTry = false;
            tryCount = 0;
        }

        if (canTry == false && timer == -2)
        {
            Invoke("StartRound", 3f); timer = -3;
        }

        if(timer > 2f)
        {
            canTry = true; timer = -2;
            switch (animIndex1)
            {
                case 1: animator1.SetBool("Move1", false); break;
                case 2: animator1.SetBool("Move12", false); break;
                default: animator1.SetBool("Move13", false); break;
            }
            switch (animIndex2)
            {
                case 1: animator2.SetBool("Move2", false); break;
                case 2: animator2.SetBool("Move22", false); break;
                default: animator2.SetBool("Move23", false); break;
            }
            switch (animIndex3)
            {
                case 1: animator3.SetBool("Move3", false); break;
                case 2: animator3.SetBool("Move32", false); break;
                default: animator3.SetBool("Move33", false); break;
            }
            switch (animIndex4)
            {
                case 1: animator4.SetBool("Move4", false); break;
                case 2: animator4.SetBool("Move42", false); break;
                default: animator4.SetBool("Move43", false); break;
            }
            switch (animIndex5)
            {
                case 1: animator5.SetBool("Move5", false); break;
                case 2: animator5.SetBool("Move52", false); break;
                default: animator5.SetBool("Move53", false); break;
            }
        }
    }
}
