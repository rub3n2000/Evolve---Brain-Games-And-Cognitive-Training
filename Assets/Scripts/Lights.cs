using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    HitTheLightsGame hitTheLightsGame;
    AntonymsSfxManager antonymsSfxManager;

    private void Start()
    {
        hitTheLightsGame = FindObjectOfType<HitTheLightsGame>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
    }

    private void OnMouseDown()
    {
        antonymsSfxManager.PlayAudio(true);
        hitTheLightsGame.totalScore += 100;
        hitTheLightsGame.StartRound();
        Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        Destroy(gameObject);
    }

}
