using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    [SerializeField]
    private GameObject gameOverCanvas;

    [SerializeField]
    private GameObject[] playerObjects;

    [SerializeField]
    private MenuDisplay menuDisplay;

    [SerializeField]
    private float timer = 60f;

    [SerializeField]
    private EmergencyCard emergencyCard;

	// Use this for initialization
	void Start () {
        gameOverCanvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(emergencyCard.GetCanDie())
        {
            timer -= (0.2f*Time.deltaTime);
        }
        if(timer<=0)
        {
            gameOverCanvas.SetActive(true);
            for(int i=0; i<= 2; i++)
            {
                playerObjects[i].SetActive(false);
            }
            menuDisplay.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
	}
}
