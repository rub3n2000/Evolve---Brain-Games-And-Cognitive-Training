using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTheSpellingErrorTexts : MonoBehaviour
{
    public bool imTheError = false;
    FindTheSpellingError findTheSpelling;

    // Start is called before the first frame update
    void Start()
    {
        findTheSpelling = FindObjectOfType<FindTheSpellingError>();
    }

    private void OnMouseDown()
    {
       
    }
}
