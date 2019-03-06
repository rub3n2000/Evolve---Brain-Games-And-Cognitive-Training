using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueSession : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SessionManager sessionManager = FindObjectOfType<SessionManager>();
        sessionManager.ContinueSession();
    }

}
