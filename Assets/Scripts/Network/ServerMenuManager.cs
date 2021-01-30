using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerMenuManager : MonoBehaviour
{
    public static ServerMenuManager instance;
    public TMP_Text sensitivityValue;

    [SerializeField]
    private Menu[] menus;

    private void Awake()
    {
        instance = this;
    }

    public void openMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if(menus[i].menuName == menuName)
            {
                menus[i].openMenu();
            }
            else if(menus[i].open)
            {
                closeMenu(menus[i]);
            }
        }
    }

    public void adjustSensitivity(float sensitivity)
    {
        sensitivityValue.text = sensitivity.ToString();
    }

    public void openMenuCanvas(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].openCanvasMenu();
            }
            else if (menus[i].open)
            {
                closeMenuCanvas(menus[i]);
            }
        }
    }

    public void openMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                closeMenu(menus[i]);
            }
        }
        menu.openMenu();
    }

    public void openMenuCanvas(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                closeMenuCanvas(menus[i]);
            }
        }
        menu.openCanvasMenu();
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void continueGame()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                closeMenuCanvas(menus[i]);
            }
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void closeMenu(Menu menu)
    {
        menu.closeMenu();
    }

    public void closeMenuCanvas(Menu menu)
    {
        menu.closeCanvasMenu();
    }
}
