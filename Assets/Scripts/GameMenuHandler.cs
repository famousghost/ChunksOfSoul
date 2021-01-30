using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuHandler : MonoBehaviour
{
    public ServerMenuManager menuManager;

    void Start()
    {
        menuManager = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<ServerMenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menuManager.openMenuCanvas("GameMenu");
        }
    }
}
