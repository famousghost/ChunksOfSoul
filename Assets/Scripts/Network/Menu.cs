using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;

    public bool open;

    public void openMenu()
    {
        open = true;
        gameObject.SetActive(true);
    }

    public void openCanvasMenu()
    {
        open = true;
        gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void closeCanvasMenu()
    {
        open = false;
        gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void closeMenu()
    {
        open = false;
        gameObject.SetActive(false);
    }
}
