using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafForLeafAndFlower : MonoBehaviour
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
            findTheFlowerAmong.totalScore -= 50;
            findTheFlowerAmong.scores.Add(0);
            findTheFlowerAmong.times.Add(findTheFlowerAmong.timer);
            GetComponent<SpriteRenderer>().material.color = Color.red;
            findTheFlowerAmong.gameIsGoing = false;
            findTheFlowerAmong.Invoke("StartNewRound", 1f);
        }
    }
}
