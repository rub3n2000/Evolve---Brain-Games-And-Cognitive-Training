using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWithPicture : MonoBehaviour
{
    PickTheCardWithPictures pickTheCardWithPictures;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    AntonymsSfxManager antonymsSfxManager;

    // Start is called before the first frame update
    void Start()
    {
        pickTheCardWithPictures = FindObjectOfType<PickTheCardWithPictures>();
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (pickTheCardWithPictures.canTry)
        {
            pickTheCardWithPictures.tryCount++;
            scoreKeeper.concentrationPoints += 100;
            if(scoreKeeper.concentrationPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.concentrationLevel +1])
            {
                scoreKeeper.concentrationLevel++;
            }
            Camera.main.GetComponent<Animator>().SetTrigger("Shake");
            antonymsSfxManager.PlayAudio(true);
            pickTheCardWithPictures.totalScore += 100;
            saveLoader.SaveGameData();
            GetComponent<SpriteRenderer>().material.color = Color.yellow;
        }
    }
}
