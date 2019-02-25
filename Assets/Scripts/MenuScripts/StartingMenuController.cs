using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingMenuController : MonoBehaviour
{

    #region Bool
    [SerializeField]
    private bool isRotating = false;

    [SerializeField]
    private bool startClicked = false;

    [SerializeField]
    private bool gameStarted = false;
    #endregion

    #region Image
    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Image loadingScreen;
    #endregion

    #region Slider
    [SerializeField]
    private Slider loadingSlider;
    #endregion

    #region Async
    [SerializeField]
    private AsyncOperation async;
    #endregion

    #region Text
    [SerializeField]
    private Text loadingDone;

    [SerializeField]
    private Text loadingText;
    #endregion

    #region String
    [SerializeField]
    private string textToLoadingScreen;
    #endregion

    #region Transform
    [SerializeField]
    private Transform cameraOriginalLookAtPosition;

    [SerializeField]
    private Transform rotationDestination;

    [SerializeField]
    private Transform cameraSettingsPosition;

    [SerializeField]
    private Transform cameraMenuPosition;

    [SerializeField]
    private Transform cameraNewGamePostion;
    #endregion

    #region Float
    [SerializeField]
    private float step = 2.0f;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start()
    {
        loadingSlider = loadingScreen.GetComponentInChildren<Slider>();
        loadingSlider.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SettingsRotation();
        GameStarter();
    }
    #endregion

    #region ButtonClicked
    public void ButtonClicked(ButtonsAction button)
    {
        if (button.GetButtonName() == "StartPosition" && button.GetButtonClicked() == false)
        {
            startClicked = true;
            button.SetButtonClicked(true);
        }
        if (button.GetButtonName() == "ExitPosition" && button.GetButtonClicked() == false)
        {
            button.SetButtonClicked(true);
            Application.Quit();
        }
        if (button.GetButtonName() == "SettingsPosition" && button.GetButtonClicked() == false)
        {
            isRotating = false;
            button.SetButtonClicked(true);
            isRotating = true;
            rotationDestination = cameraSettingsPosition;
        }
        if (button.GetButtonName() == "BackPosition" && button.GetButtonClicked() == false)
        {
            isRotating = false;
            button.SetButtonClicked(true);
            isRotating = true;
            rotationDestination = cameraOriginalLookAtPosition;
        }
    }
    #endregion

    #region Load Game
    public void LoadGame(int lvl)
    {
        StartCoroutine(LoadingScreenScene(lvl));
    }

    IEnumerator LoadingScreenScene(int lvl)
    {
        loadingText.text = textToLoadingScreen;
        loadingSlider.enabled = true;
        loadingScreen.GetComponent<Image>().enabled = true;
        async = SceneManager.LoadSceneAsync(lvl);
        async.allowSceneActivation = false;
        backgroundImage.GetComponent<Image>().enabled = true;
        fillImage.GetComponent<Image>().enabled = true;

        while (async.isDone == false)
        {
            loadingText.GetComponent<Text>().enabled = true;
            loadingSlider.value = async.progress;
            if (async.progress == 0.9f)
            {
                loadingDone.GetComponent<Text>().enabled = true;
                if (Input.anyKey)
                    async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    #endregion

    #region CameraRotateTowards
    private void SettingsRotation()
    {
        if (isRotating)
        {
            CameraRotateTowards();
            if (IsAtRotationDestination())
            {
                Debug.Log("xD");
                isRotating = false;
            }
        }
    }

    private void CameraRotateTowards()
    {
        cameraMenuPosition.rotation = Quaternion.RotateTowards(cameraMenuPosition.rotation, rotationDestination.rotation, step * 10 * Time.deltaTime);
    }

    private bool IsAtRotationDestination()
    {
        return (Vector3.Angle(cameraMenuPosition.forward, rotationDestination.position - cameraMenuPosition.position) < 10);
    }
    #endregion

    #region GameStarter
    private void GameStarter()
    {
        if (startClicked && !gameStarted)
        {
            cameraMenuPosition.rotation = Quaternion.RotateTowards(cameraMenuPosition.rotation, cameraNewGamePostion.rotation, step * 10 * Time.deltaTime);
            cameraMenuPosition.position = Vector3.MoveTowards(cameraMenuPosition.position, cameraNewGamePostion.position, step * Time.deltaTime / 3);
            bool checkCameraPosition = (cameraMenuPosition.rotation == cameraNewGamePostion.rotation);
            if (checkCameraPosition)
            {
                gameStarted = true;
                LoadGame(1);
            }
        }
    }
    #endregion
}
