using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RiddleAnswer : MonoBehaviour {

    [SerializeField]
    private char[] answerToRiddle;

    [SerializeField]
    private bool canOpenRiddle = false;

    [SerializeField]
    private Text numbers;

    #region Canvas To Riddle
    [SerializeField]
    private Canvas canvas;
    #endregion

    [SerializeField]
    private EmergencyCard emergencyCard;

    [SerializeField]
    private int counter = 0;

    #region GUI style
    [SerializeField]
    private GUIStyle colorOfText;
    #endregion

    void Start()
    {
        colorOfText.normal.textColor = Color.black;
        colorOfText.fontSize = 20;
        answerToRiddle = new char[5];
        numbers = GameObject.Find("Code").GetComponent<Text>();
        emergencyCard = GameObject.Find("UsableItems").GetComponent<EmergencyCard>();
    }

    void Update()
    {
        SetAnswer();
    }

    public void RiddleEnable()
    {
        canOpenRiddle = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RiddleDisable()
    {
        canOpenRiddle = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool SetAnswer()
    {
        if (answerToRiddle[0] == '1' && answerToRiddle[1] == '2' && answerToRiddle[2] == '6' && answerToRiddle[3] == '3' && answerToRiddle[4] == '0')
        {
             canOpenRiddle = false;
             emergencyCard.SetCorrectCode(true);
             return true;
        }
        else
        {
            if (counter > 4)
            {
                counter = 0;
                numbers.text = "";
                for(int i=0;i<5;i++)
                {
                    answerToRiddle[i] = 'x';
                }
                return false;
            }
        }
        return false;
    }

    public void AddToAnswer(char answerToRiddle)
    {
        if (counter < 4)
        {
            this.answerToRiddle[counter] = answerToRiddle;
            if (counter == 0)
            {
                numbers.text = "";
            }
            if (answerToRiddle == '0')
            {
                numbers.text = "";
                counter = 0;
            }
            if (answerToRiddle != '0')
                numbers.text += answerToRiddle;
            Debug.Log(answerToRiddle);
            Debug.Log(counter);
        }
        else
        {
            if (answerToRiddle == '0')
            {
                this.answerToRiddle[4] = answerToRiddle;
                counter++;
            }
        }
    }

    public void SetCorrectAnswer()
    {
        emergencyCard.SetCorrectCode(true);
    }

    public bool GetCanOpenRiddle()
    {
        return canOpenRiddle;
    }

    public void AddCounter(int i)
    {
        counter += i;
    }

    void OnGUI()
    {
        int stringWidth = 150;
        int stringHeight = 50;
        if (canOpenRiddle && !emergencyCard.GetCanAnswerToRiddle() && !canvas.isActiveAndEnabled)
        {
            GUI.Label(new Rect(Screen.width / 2 - (stringWidth / 2), Screen.height / 2, stringWidth, stringHeight),"Find Key Card", colorOfText);
        }
        else if(canOpenRiddle && emergencyCard.GetCanAnswerToRiddle() && !canvas.isActiveAndEnabled)
        {
            GUI.Label(new Rect(Screen.width / 2 - (stringWidth / 4), Screen.height / 2, stringWidth, stringHeight), "LMB", colorOfText);
        }

    }

    public int GetCounter()
    {
        return counter;
    }
}
