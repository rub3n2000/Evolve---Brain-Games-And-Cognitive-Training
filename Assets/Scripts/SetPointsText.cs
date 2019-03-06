using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPointsText : MonoBehaviour
{
    [SerializeField]
    Categories thisTextCategories;
    void Start()
    {
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        Text text = GetComponent<Text>();
        switch (thisTextCategories)
        {
            case Categories.reaction:
                text.text = scoreKeeper.reactionPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.reactionLevel + 1].ToString() + " points";
                break;
            case Categories.logic:
                text.text = scoreKeeper.logicPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.logicLevel + 1].ToString() + " points";
                break;
            case Categories.memory:
                text.text = scoreKeeper.memoryPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1].ToString() + " points";
                break;
            case Categories.concentration:
                text.text = scoreKeeper.concentrationPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.concentrationLevel + 1].ToString() + " points";
                break;
            case Categories.language:
                text.text = scoreKeeper.languagePoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.languageLevel + 1].ToString() + " points";
                break;
            case Categories.multitasking:
                text.text = scoreKeeper.multitaskingPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.multitaskingLevel + 1].ToString() + " points";
                break;
            default:
                text.text = "An error occured..";
                break;
        }
    }
}

enum Categories { reaction, logic, memory, concentration, language, multitasking };