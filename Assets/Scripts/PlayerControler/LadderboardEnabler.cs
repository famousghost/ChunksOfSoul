using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderboardEnabler : MonoBehaviour
{
    public Canvas ladderBoardCanvas;

    public void canvasChangeState()
    {
        ladderBoardCanvas.enabled = !ladderBoardCanvas.enabled;
    }
}
