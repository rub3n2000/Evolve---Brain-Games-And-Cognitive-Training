using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCard : MonoBehaviour
{
    PickTheCardWithPictures pickTheCardWithPictures;
    AntonymsSfxManager antonymsSfxManager;

    // Start is called before the first frame update
    void Start()
    {
        pickTheCardWithPictures = FindObjectOfType<PickTheCardWithPictures>();
        antonymsSfxManager = FindObjectOfType<AntonymsSfxManager>();
    }

    private void OnMouseDown()
    {
      if(pickTheCardWithPictures.canTry)
        {
            antonymsSfxManager.PlayAudio(false);
            pickTheCardWithPictures.canTry = false;
            GetComponent<SpriteRenderer>().material.color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
