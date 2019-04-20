using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafForLeafAndFlower : MonoBehaviour
{

    FindTheFlowerAmongTheLeafs findTheFlowerAmong;

    GameObject background;

    [SerializeField]
    Color red;

    
    AntonymsSfxManager antonymsSfxManager;

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
            antonymsSfxManager.PlayAudio(false);
            findTheFlowerAmong.totalScore -= 50;
            findTheFlowerAmong.scores.Add(0);
            findTheFlowerAmong.times.Add(findTheFlowerAmong.timer);
            background.GetComponent<SpriteRenderer>().material.color = red;
            findTheFlowerAmong.gameIsGoing = false;
            findTheFlowerAmong.Invoke("StartNewRound", 1f);
        }
    }
}
