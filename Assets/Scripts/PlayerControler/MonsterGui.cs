using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGui : MonoBehaviour
{
    [SerializeField]
    private PlayerControler playerController;
    public Canvas monsterCanvas;
    // Start is called before the first frame update
    void Start()
    {
        monsterCanvas = GameObject.FindGameObjectWithTag("MonsterCanvas").GetComponent<Canvas>();
        if(playerController.playerCharacter == PlayerCharacter.Ghost)
        {
            enableMosterCanvas();
        }
    }

    void enableMosterCanvas()
    {
        monsterCanvas.enabled = true;
    }
}
