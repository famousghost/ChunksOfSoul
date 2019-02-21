using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClicked : MonoBehaviour {

    #region Game Objects
    [SerializeField]
    private GameObject thisButton;
    #endregion

    #region Transform
    [SerializeField]
    private Transform buttonClicked;

    [SerializeField]
    private Transform currentButtonState;

    [SerializeField]
    private Transform buttonReleased;
    #endregion

    #region Bool
    [SerializeField]
    private bool isClicked = false;
    #endregion

    #region Float
    [SerializeField]
    private float speedOfClicked = 0.2f;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        thisButton = this.gameObject;
        buttonClicked = thisButton.transform.Find("ButtonClicked");
        buttonReleased = thisButton.transform.Find("ButtonReleased");
        currentButtonState = thisButton.transform.Find("Button");

    }

    void Update()
    {
        ChangeButtonState();
    }
    #endregion

    #region Change Button State
    private void ChangeButtonState()
    {
        if(isClicked)
            thisButton.transform.Find("Button").position = Vector3.MoveTowards(thisButton.transform.Find("Button").position, buttonClicked.position, speedOfClicked * Time.deltaTime);
        else
            thisButton.transform.Find("Button").position = Vector3.MoveTowards(thisButton.transform.Find("Button").position, buttonReleased.position, speedOfClicked * Time.deltaTime);
        if(currentButtonState.position == buttonClicked.position)
        {
            isClicked = false;
        }
    }
    #endregion

    #region Button Click
    public void ButtonClick()
    {
        isClicked = true;
    }
    #endregion

}
