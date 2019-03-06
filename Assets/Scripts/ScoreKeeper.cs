using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper scoreKeeper = null;

    public int reactionLevel { get; set; } = 0;
    public int logicLevel { get; set; } = 0;
    public int memoryLevel { get; set; } = 0;
    public int concentrationLevel { get; set; } = 0;
    public int languageLevel { get; set; } = 0;
    public int multitaskingLevel { get; set; } = 0; 

    public int reactionPoints { get; set; } = 0;
    public int logicPoints { get; set; } = 0;
    public int memoryPoints { get; set; } = 0;
    public int concentrationPoints { get; set; } = 0;
    public int languagePoints { get; set; } = 0;
    public int multitaskingPoints { get; set; } = 0;

    SaveLoader saveLoader;

    
    public int[] pointsRequiredForLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreKeeper == null)
        {
            scoreKeeper = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(this);
        }
        else if (scoreKeeper != null && scoreKeeper != this)
        {
            Destroy(this);
        }
    }
}
