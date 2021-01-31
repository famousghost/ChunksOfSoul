using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuHandler : MonoBehaviour
{
    public ServerMenuManager menuManager;
    public static bool isMenuOpened = false;

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
            if (!menuManager.menuOpened)
            {
                menuManager.openMenuCanvas("GameMenu");
                menuManager.menuOpened = true;
                isMenuOpened = true;
            }
            else
            {
                menuManager.closeMenuCanvas("GameMenu");
                menuManager.menuOpened = false;
                isMenuOpened = false;
            }
        }
    }
}
