using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMenuManager : MonoBehaviour
{
    public static ServerMenuManager instance;

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

    public void closeMenu(Menu menu)
    {
        menu.closeMenu();
    }
}
