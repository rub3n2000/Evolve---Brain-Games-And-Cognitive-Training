using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    LeafBlower leafBlower;
    SaveLoader saveLoader;
    ScoreKeeper scoreKeeper;
    [SerializeField]
    GameObject explosion;
    AntonymsSfxManager antonymsSfxManager;

    // Start is called before the first frame update
    void Start()
    {
        leafBlower = FindObjectOfType<LeafBlower>();
        saveLoader = FindObjectOfType<SaveLoader>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 1, 0), Time.deltaTime * leafBlower.leafSpeed);
    }

    private void OnMouseDown()
    {
        scoreKeeper.reactionPoints += 10;
        antonymsSfxManager.PlayAudio(true);
        if (scoreKeeper.reactionPoints > scoreKeeper.pointsRequiredForLevel[scoreKeeper.reactionLevel + 1])
        {
            scoreKeeper.reactionLevel++;
        }
        saveLoader.SaveGameData();
        leafBlower.totalScoreCollected += 10;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Camera.main.GetComponent<Animator>().SetTrigger("Shake");
        Destroy(gameObject);
    }
}
