using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Riddle : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private RiddleAnswer riddleAnswer;

    void Start()
    {
        riddleAnswer = GameObject.Find("Riddles").GetComponent<RiddleAnswer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        riddleAnswer.AddToAnswer(this.gameObject.name[0]);
        if(this.gameObject.name[0] != '0' && riddleAnswer.GetCounter() < 4)
            riddleAnswer.AddCounter(1);
        if(riddleAnswer.SetAnswer())
        {
            riddleAnswer.SetCorrectAnswer();
        }
        Debug.Log(this.gameObject.name);
    }

}
