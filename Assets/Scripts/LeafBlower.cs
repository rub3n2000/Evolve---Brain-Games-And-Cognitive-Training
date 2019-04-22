using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeafBlower : MonoBehaviour
{
    [SerializeField]
    GameObject gameContainer;

    [SerializeField]
    GameObject endScreenContainer;

    [SerializeField]
    Text endscreenText;

    [SerializeField]
    Text timerText;

    [SerializeField]
    GameObject leaf;
    [SerializeField]
    GameObject leaf1;
    [SerializeField]
    GameObject leaf2;
    [SerializeField]
    GameObject leaf3;
    
    [SerializeField]
    GameObject leaf5;
    [SerializeField]
    GameObject leaf4;


    float timer = 60;

    [SerializeField]
    public float leafSpeed = 5;

    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;

    public float totalScoreCollected { get; set; }

    [SerializeField]
    float spawnRate = 1f;

    float spawnTimer;

    bool gameIsGoing = true;

    bool started = false;

    [SerializeField]
    GameObject intro;


    // Start is called before the first frame update
    void Start()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        endScreenContainer.SetActive(false);
        gameContainer.SetActive(true);
        spawnTimer = spawnRate;
        intro.SetActive(true);
        Invoke("StartGame", 3f);
    }

    void StartGame()
    {
        started = true;
        intro.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("0.0");
            spawnTimer -= Time.deltaTime;
            if (spawnTimer < 0 && gameIsGoing)
            {
                int random = Random.Range(1, 7);
                GameObject theLeaf = leaf;
                switch (random)
                {
                    case 1: theLeaf = leaf; break;
                    case 2: theLeaf = leaf1; break;
                    case 3: theLeaf = leaf2; break;
                    case 4: theLeaf = leaf3; break;
                    case 5: theLeaf = leaf4; break;
                    case 6: theLeaf = leaf5; break;
                }
                Instantiate(theLeaf, new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y + 1), Quaternion.Euler(0, 0, Random.Range(0, 181)));
                spawnTimer = spawnRate;
            }
            if (timer < 0)
            {
                gameIsGoing = false;
                gameContainer.SetActive(false);
                endScreenContainer.SetActive(true);
                endscreenText.text = "Total Score " + totalScoreCollected;
            }
        }
    }

    public void ContinueSession()
    {
        sessionManager.ContinueSession();
    }
}
