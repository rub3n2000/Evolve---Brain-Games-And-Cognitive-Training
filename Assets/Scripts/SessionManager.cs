using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{

    public static SessionManager sessionManager = null;

    [SerializeField]
    int[] reactionGamesBuildIndexes;
    [SerializeField]
    int[] logicGamesBuildIndexes;
    [SerializeField]
    int[] memoryGamesBuildIndexes;
    [SerializeField]
    int[] concentrationGamesBuildIndexes;
    [SerializeField]
    int[] languageGamesBuildIndexes;
    [SerializeField]
    int[] multitaskingGamesBuildIndexes;

    int currentSessionIndex = 0;

    List<int> currentSession;

    private void Start()
    {
        if (sessionManager == null)
        {
            sessionManager = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(this);
        }
        else if (sessionManager != null && sessionManager != this)
        {
            Destroy(this);
        }
    }

    public void StartSession()
    {
        sessionManager.currentSession = new List<int>();
        sessionManager.currentSession.Add(reactionGamesBuildIndexes[Random.Range(0, reactionGamesBuildIndexes.Length)]);
        sessionManager.currentSession.Add(logicGamesBuildIndexes[Random.Range(0, logicGamesBuildIndexes.Length)]);
        sessionManager.currentSession.Add(memoryGamesBuildIndexes[Random.Range(0, memoryGamesBuildIndexes.Length)]);
        sessionManager.currentSession.Add(concentrationGamesBuildIndexes[Random.Range(0, concentrationGamesBuildIndexes.Length)]);
        sessionManager.currentSession.Add(languageGamesBuildIndexes[Random.Range(0, languageGamesBuildIndexes.Length)]);
        sessionManager.currentSession.Add(multitaskingGamesBuildIndexes[Random.Range(0, multitaskingGamesBuildIndexes.Length)]);
        SceneManager.LoadScene(sessionManager.currentSession[currentSessionIndex]);
    }

    public void ContinueSession()
    {
        if(currentSessionIndex == 5)
        {
            currentSessionIndex = 0;
            sessionManager.currentSession.Clear();
            SceneManager.LoadScene(0);
        }
        currentSessionIndex++;
        SceneManager.LoadScene(sessionManager.currentSession[currentSessionIndex]);
    }
}
