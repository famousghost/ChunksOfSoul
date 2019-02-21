using UnityEngine;
using System.Collections;
using System;

public class KeysInput : MonoBehaviour {

    #region KeysInput
    [Header("Input Float")]
    [SerializeField]
    private float walkX;

    [SerializeField]
    private float walkY;

    [SerializeField]
    private float mouseX;

    [SerializeField]
    private float mouseY;

    [Header("Input KeyCode")]
    [SerializeField]
    private KeyCode jump = KeyCode.Space;

    [SerializeField]
    private KeyCode run = KeyCode.LeftShift;

    [SerializeField]
    private KeyCode lifting = KeyCode.Mouse0;

    [SerializeField]
    private KeyCode crouch = KeyCode.LeftControl;

    [SerializeField]
    private KeyCode lookLeft = KeyCode.Q;

    [SerializeField]
    private KeyCode lookRight = KeyCode.E;

    [Header("Input Bool")]
    [SerializeField]
    private bool isRunning = false;

    [SerializeField]
    private bool isJumping = false;

    [SerializeField]
    private bool onGrounded = true;

    [SerializeField]
    private bool isLiftetd = false;

    [SerializeField]
    private bool playerIsCrouch = false;

    [SerializeField]
    private bool leftLook = false;

    [SerializeField]
    private bool rightLook = false;

    #endregion

    #region InputsMethod
    public void Inputs()
    {
        walkX = Input.GetAxis("Horizontal");
        walkY = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        isJumping = Input.GetKeyDown(jump);
        isRunning = Input.GetKey(run);
        isLiftetd = Input.GetKey(lifting);
        if (!rightLook)
        {
            leftLook = Input.GetKey(lookLeft);
        }
        if (!leftLook)
        {
            rightLook = Input.GetKey(lookRight);
        }
        playerIsCrouch = Input.GetKey(crouch);

    }
    #endregion

    #region Getters
    public float GetWalkX()
    {
        return walkX;
    }

    public float GetWalkY()
    {
        return walkY;
    }

    public float GetMouseX()
    {
        return mouseX;
    }

    public float GetMouseY()
    {
        return mouseY;
    }

    public bool GetIsJupming()
    {
        return isJumping;
    }

    public bool GetIsRunning()
    {
        return isRunning;
    }

    public bool GetOnGrounded()
    {
        return onGrounded;
    }

    public bool GetIsLifted()
    {
        return isLiftetd;
    }

    public bool GetIsCrouching()
    {
        return playerIsCrouch;
    }

    public bool GetLeftSide()
    {
        return leftLook;
    }

    public bool GetRightSide()
    {
        return rightLook;
    }
    #endregion

    #region Setters

    public void SetOnGrounded(bool set)
    {
        onGrounded = set;
    }

    public void SetLiftetd(bool set)
    {
        isLiftetd = set;
    }

    #endregion

}
