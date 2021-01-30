using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBoard : MonoBehaviour
{
    public LadderboardEnabler ladderBoardCanvas;

    // Start is called before the first frame update
    void Start()
    {
        ladderBoardCanvas = GameObject.FindGameObjectWithTag("LadderBoard").GetComponent<LadderboardEnabler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ladderBoardCanvas.canvasChangeState();
        }
    }
}
