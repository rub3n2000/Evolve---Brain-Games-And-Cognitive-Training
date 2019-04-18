using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCatchingGameEnder : MonoBehaviour
{
    BerryCatchingAndMath BerryCatchingAndMath;
    // Start is called before the first frame update
    void Start()
    {
        BerryCatchingAndMath = FindObjectOfType<BerryCatchingAndMath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BerryCatchingAndMath.EndGame();
        Destroy(collision.gameObject);
    }
}
