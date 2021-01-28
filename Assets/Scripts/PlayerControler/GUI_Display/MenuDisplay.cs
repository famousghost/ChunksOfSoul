using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDisplay : MonoBehaviour {

    #region Variables
    private bool cancelActive = false;
    private bool controlsDisplayed = false;

    [SerializeField]
    public GameObject gamePauseParent;

    [SerializeField]
    public GameObject gamePauseMain;

    [SerializeField]
    public GameObject gamePauseMainControls;

    [SerializeField]
    private PlayerControler playerControler;

    [SerializeField]
    private ItemsPickUp itemsPickUp;

    [SerializeField]
    public GameObject player;
    #endregion

    #region Start
    void Awake () {
        gamePauseParent.SetActive(false);
        gamePauseMainControls.SetActive(false);
    }
    #endregion

    #region Update
    void Update () {
        GamePause();
	}
    #endregion

    #region GamePause
    public void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePauseMainControls.activeSelf)
            {
                ControlsToggle();
            }
            else
            {
                MenuToggle();
            }
        }
    }
    #endregion

    #region BackToMenu
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(player);
        Destroy(GameManager.instance.gameObject);
    }
    #endregion

    #region ControlsTOGGLE
    public void ControlsToggle()
    {
        gamePauseMain.SetActive(controlsDisplayed);
        controlsDisplayed = !controlsDisplayed;
        gamePauseMainControls.SetActive(controlsDisplayed);
    }
    #endregion

    #region MenuToggle
    void MenuToggle()
    {
        gamePauseParent.SetActive(!cancelActive);
        playerControler.enabled = cancelActive;
        itemsPickUp.enabled = cancelActive;

        cancelActive = !cancelActive;

        Cursor.visible = cancelActive;
        if (Cursor.lockState == CursorLockMode.Confined || Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    #endregion
}
