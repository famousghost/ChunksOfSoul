using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TMP_Text winnerPlayer;
    public Canvas gameOverCanvas;

    public void enableGameOverCanvas(string playerNickName)
    {
        winnerPlayer.text = playerNickName;
        gameOverCanvas.enabled = true;
    }
}
