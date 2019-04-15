using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    HitTheLightsGame hitTheLightsGame;
    private void Start()
    {
        hitTheLightsGame = FindObjectOfType<HitTheLightsGame>();
    }

    private void OnMouseDown()
    {
        hitTheLightsGame.totalScore += 10;
        hitTheLightsGame.StartRound();
        Destroy(gameObject);
    }

}
