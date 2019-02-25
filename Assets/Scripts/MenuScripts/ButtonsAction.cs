using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsAction : MonoBehaviour{

    [SerializeField]
    private StartingMenuController menuController;

    #region Bool
    [SerializeField]
    private bool isHover = false;

    [SerializeField]
    private bool buttonIsClicked = false;
    #endregion

    #region String
    [SerializeField]
    private string buttonName = "";
    #endregion

    #region Transform
    [SerializeField]
    private Transform textHoverTransform;

    [SerializeField]
    private Transform textNormalTransform;

    [SerializeField]
    private Transform textCurrentTransform;
    #endregion

    #region Float
    [SerializeField]
    private float step = 2.0f;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start()
    {
        buttonName = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTextPosition();
    }
    #endregion

    #region On Mouse Methods
    void OnMouseEnter()
    {
        Debug.Log(this.gameObject.name);
        isHover = true;
    }

    void OnMouseExit()
    {
        Debug.Log("Cofam");
        isHover = false;
    }

    void OnMouseDown()
    {
        Debug.Log(buttonName);
        buttonIsClicked = false;
        menuController.ButtonClicked(this);
    }
    #endregion

    #region TextPosition
    private void ChangeTextPosition()
    {
        if (isHover == true)
        {
            textCurrentTransform.position = Vector3.MoveTowards(textCurrentTransform.position, textHoverTransform.position, step * Time.deltaTime);
        }
        else
        {
            textCurrentTransform.position = Vector3.MoveTowards(textCurrentTransform.position, textNormalTransform.position, step * Time.deltaTime);
        }
    }
    #endregion

    #region Getters
    public string GetButtonName()
    {
        return this.buttonName;
    }

    public bool GetButtonClicked()
    {
        return this.buttonIsClicked;
    }
    #endregion

    #region Setters
    public void SetButtonName(string buttonName)
    {
        this.buttonName = buttonName;
    }

    public void SetButtonClicked(bool buttonIsClicked)
    {
        this.buttonIsClicked = buttonIsClicked;
    }
    #endregion
}
