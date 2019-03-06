using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetLevelText : MonoBehaviour
{
    [SerializeField]
    Categories thisTextCategories;

    void Start()
    {
      ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
      Text text = GetComponent<Text>();
      switch(thisTextCategories)
        {
            case Categories.reaction:
                text.text = "Level " + scoreKeeper.reactionLevel.ToString();
                break;
            case Categories.logic:
                text.text = "Level " + scoreKeeper.logicLevel.ToString();
                break;
            case Categories.memory:
                text.text = "Level " + scoreKeeper.memoryLevel.ToString();
                break;
            case Categories.concentration:
                text.text = "Level " + scoreKeeper.concentrationLevel.ToString();
                break;
            case Categories.language:
                text.text = "Level " + scoreKeeper.languageLevel.ToString();
                break;
            case Categories.multitasking:
                text.text = "Level " + scoreKeeper.multitaskingLevel.ToString();
                break;
            default:
                text.text = "An error occured..";
                break;
        }
    }
}
