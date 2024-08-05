using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizContainer : MonoBehaviour
{
    public GameObject questionContainer, AnswerContainer;

    public TextMeshProUGUI titleText;
    public GameObject AbiltyUI;
    // public Button optionA, optionB, optionC, optionD;

    public Button[] buttons;


    public void EnableComponent()
    {
        AbiltyUI.SetActive(false);
        GetComponent<Image>().enabled = true;
        questionContainer.SetActive(true);
        AnswerContainer.SetActive(true);
    }

    public void DisableComponent()
    {
        AbiltyUI.SetActive(true);
        GetComponent<Image>().enabled = !true;
        questionContainer.SetActive(false);
        AnswerContainer.SetActive(false);
    }

    private void Start()
    {
         AbiltyUI = GameObject.FindGameObjectWithTag("AbilityUI");
    }




}
