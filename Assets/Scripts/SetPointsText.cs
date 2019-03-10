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
                if (scoreKeeper.reactionLevel < 100)
                {
                    text.text = scoreKeeper.reactionPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.reactionLevel + 1].ToString() + " points";
                }
                else { text.text = "Max Lvl"; }
                break;
            case Categories.logic:
                if (scoreKeeper.logicLevel < 100)
                {
                    text.text = scoreKeeper.logicPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.logicLevel + 1].ToString() + " points";
                }
                else { text.text = "Max Lvl"; }
                break;
            case Categories.memory:
                if (scoreKeeper.memoryLevel < 100)
                {
                    text.text = scoreKeeper.memoryPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.memoryLevel + 1].ToString() + " points";
                }
                else { text.text = "Max Lvl"; }
                break;
            case Categories.concentration:
                if (scoreKeeper.concentrationLevel < 100)
                {
                    text.text = scoreKeeper.concentrationPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.concentrationLevel + 1].ToString() + " points";
                }
                else
                { text.text = "Max Lvl"; }
                 break;
            case Categories.language:
                if (scoreKeeper.languageLevel < 100)
                {
                    text.text = scoreKeeper.languagePoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.languageLevel + 1].ToString() + " points";
                }
                else { text.text = "Max Lvl"; }
                break;
            case Categories.multitasking:
                if (scoreKeeper.multitaskingLevel < 100)
                {
                    text.text = scoreKeeper.multitaskingPoints.ToString() + "/" + scoreKeeper.pointsRequiredForLevel[scoreKeeper.multitaskingLevel + 1].ToString() + " points";
                }
                else { text.text = "Max Lvl"; }
                break;
            default:
                text.text = "An error occured..";
                break;
        }
    }
}

enum Categories { reaction, logic, memory, concentration, language, multitasking };