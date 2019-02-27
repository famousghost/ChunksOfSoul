using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDisplay : MonoBehaviour {

    #region Variables
    private bool cancelActive = false;
    private bool settingsDisplayed = false;
    private bool controlsDisplayed = false;

    [SerializeField]
    public GameObject gamePauseParent;

    [SerializeField]
    public GameObject gamePauseMain;

    [SerializeField]
    public GameObject gamePauseMainSettings;

    [SerializeField]
    public GameObject gamePauseMainControls;

    [SerializeField]
    private PlayerControler playerControler;

    [SerializeField]
    private ItemDisplay itemDisplay; //MARCIN TUTAJ! (i w linijce 107)

    [SerializeField]
    private ItemsPickUp itemsPickUp;

    [SerializeField]
    public GameObject player;
    #endregion

    #region Start
    void Awake () {
        gamePauseMainSettings.SetActive(false);
        gamePauseParent.SetActive(false);
        gamePauseMainControls.SetActive(false);
    }
    #endregion

    #region Update
    void Update () {
        GamePause(0);
	}
    #endregion

    #region GamePause
    public void GamePause(int a)
    {
        if (Input.GetKeyDown(KeyCode.Escape) || a > 0.0f)
        {
            if (gamePauseMainSettings.active)
            {
                SettingsToggle();
            }
            else if (gamePauseMainControls.active)
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

    #region SettingsTOGGLE
    public void SettingsToggle()
    {
        gamePauseMain.SetActive(settingsDisplayed);
        settingsDisplayed = !settingsDisplayed;
        gamePauseMainSettings.SetActive(settingsDisplayed);
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
        itemDisplay.enabled = cancelActive;

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
