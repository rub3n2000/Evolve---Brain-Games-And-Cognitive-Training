using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    FindTheFlowerAmongTheLeafs findTheFlowerAmong;

    GameObject background;

    
    AntonymsSfxManager antonymsSfxManager;

    [SerializeField]
    Color green;

    // Start is called before the first frame update
    void Start()
    {
        findTheFlowerAmong = FindObjectOfType<FindTheFlowerAmongTheLeafs>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
        background = GameObject.FindGameObjectWithTag("background");
    }

    private void OnMouseDown()
    {
        if (findTheFlowerAmong.gameIsGoing)
        {
            int score = 100 - (int)(findTheFlowerAmong.timer * 5) + 50;
            findTheFlowerAmong.totalScore += score;
            antonymsSfxManager.PlayAudio(true);
            findTheFlowerAmong.scores.Add(score);
            findTheFlowerAmong.times.Add(findTheFlowerAmong.timer);
            background.GetComponent<SpriteRenderer>().material.color = green;
            findTheFlowerAmong.Invoke("StartNewRound", 1f);
            findTheFlowerAmong.gameIsGoing = false;
            Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        }
    }
  
}
