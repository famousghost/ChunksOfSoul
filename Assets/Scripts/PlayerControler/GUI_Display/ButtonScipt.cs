using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScipt : MonoBehaviour {

    public void MenuExit()
    {
        SceneManager.LoadScene(0);
    }

}
