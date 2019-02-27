using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //to do: add loading screens and move player to starting position on next levels

    #region Variables
    [SerializeField]
    public MenuDisplay menuDisplay;

    [SerializeField]
    public GameOver gameOver;

    public static GameManager instance;
    #endregion

    #region Awake
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            instance.menuDisplay = FindObjectOfType<MenuDisplay>();
            instance.gameOver = FindObjectOfType<GameOver>();
        }

        DontDestroyOnLoad(gameObject);
        menuDisplay = FindObjectOfType<MenuDisplay>();
        gameOver = FindObjectOfType<GameOver>();
    }
    #endregion

    #region NextLevel
    public void NextLevel()
    {

    }
    #endregion

    #region GameOver
    public void GameOver()
    {
        gameOver.GameOverToggle();
    }
    #endregion

    #region GameManagerBackToMenu
    public void GameManagerBackToMenu()
    {
        SceneManager.LoadScene("StartingMenu");
    }
    #endregion
}
