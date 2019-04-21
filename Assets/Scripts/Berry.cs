using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berry : MonoBehaviour
{
    public float speed = 1;
    BerryCatchingAndMath playBlock;
    // Start is called before the first frame update
    void Start()
    {
        playBlock = FindObjectOfType<BerryCatchingAndMath>();
        if (playBlock)
        {
            speed = playBlock.speed;
        }
        else
        {
            speed = FindObjectOfType<PlayBlockBreakerAndHangman>().speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
    }
}
