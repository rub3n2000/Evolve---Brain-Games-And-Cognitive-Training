using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    PlayBlockBreakerAndHangman playBlockBreakerAndHangman;
    // Start is called before the first frame update
    void Start()
    {
        playBlockBreakerAndHangman = FindObjectOfType<PlayBlockBreakerAndHangman>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playBlockBreakerAndHangman.EndGame();
    }
}
