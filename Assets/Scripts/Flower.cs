using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    FindTheFlowerAmongTheLeafs findTheFlowerAmong;
    // Start is called before the first frame update
    void Start()
    {
        findTheFlowerAmong = FindObjectOfType<FindTheFlowerAmongTheLeafs>();
    }

    private void OnMouseDown()
    {
        if (findTheFlowerAmong.gameIsGoing)
        {
            int score = 100 - (int)(findTheFlowerAmong.timer * 5);
            findTheFlowerAmong.totalScore += score;
            findTheFlowerAmong.scores.Add(score);
            findTheFlowerAmong.times.Add(findTheFlowerAmong.timer);
            GetComponent<SpriteRenderer>().material.color = Color.green;
            findTheFlowerAmong.Invoke("StartNewRound", 1f);
            findTheFlowerAmong.gameIsGoing = false;
        }
    }
  
}
