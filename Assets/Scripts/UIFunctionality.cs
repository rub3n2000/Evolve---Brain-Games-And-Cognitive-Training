using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionality : MonoBehaviour
{
    SessionManager sessionManager;

    public void Practice()
    {
        sessionManager = FindObjectOfType<SessionManager>();
        sessionManager.StartSession();
    }
}
