using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBasketButtons : MonoBehaviour
{

    [SerializeField]
    GameObject basket;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        basket.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
