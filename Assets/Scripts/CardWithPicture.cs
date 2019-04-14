using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWithPicture : MonoBehaviour
{
    PickTheCardWithPictures pickTheCardWithPictures;
    SessionManager sessionManager;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Start()
    {
        pickTheCardWithPictures = FindObjectOfType<PickTheCardWithPictures>();
        sessionManager = FindObjectOfType<SessionManager>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
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
            scoreKeeper.concentrationPoints += 10;
            if(scoreKeeper.concentrationPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.concentrationLevel +1])
            {
                scoreKeeper.concentrationLevel++;
            }
            pickTheCardWithPictures.totalScore += 10;
            saveLoader.SaveGameData();
            GetComponent<SpriteRenderer>().material.color = Color.yellow;
        }
    }
}
