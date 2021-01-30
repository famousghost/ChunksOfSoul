using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderboardEnabler : MonoBehaviour
{
    public Canvas ladderBoardCanvas;

    public void activeCanvas(bool value)
    {
        Debug.Log("wcianales taba kurcze");
        ladderBoardCanvas.enabled = value;
    }
}
