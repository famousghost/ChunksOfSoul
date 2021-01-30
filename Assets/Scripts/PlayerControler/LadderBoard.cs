using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBoard : MonoBehaviour
{
    public LadderboardEnabler ladderBoardCanvas;
    public bool canvasIsActivated;

    // Start is called before the first frame update
    void Start()
    {
        canvasIsActivated = false;
        ladderBoardCanvas = GameObject.FindGameObjectWithTag("LadderBoard").GetComponent<LadderboardEnabler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvasIsActivated = !canvasIsActivated;
            ladderBoardCanvas.activeCanvas(canvasIsActivated);
        }
    }
}
