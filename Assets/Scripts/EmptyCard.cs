using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCard : MonoBehaviour
{
    PickTheCardWithPictures pickTheCardWithPictures;

    // Start is called before the first frame update
    void Start()
    {
        pickTheCardWithPictures = FindObjectOfType<PickTheCardWithPictures>();
    }

    private void OnMouseDown()
    {
      if(pickTheCardWithPictures.canTry)
        {
            pickTheCardWithPictures.canTry = false;
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
