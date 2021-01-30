using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTeleport : MonoBehaviour
{
    [SerializeField]
    private PlayerControler playerController;
    public GameObject[] ghostTeleportTransform;
    // Update is called once per frame

    void Start()
    {
        ghostTeleportTransform = new GameObject[4];
        for(int i = 0; i < 4; ++i)
        {
            string teleportName = "GhostTeleport";
            Debug.Log("Teleport name is: " + teleportName);
            ghostTeleportTransform[i] = GameObject.FindGameObjectsWithTag(teleportName)[i].gameObject;
        }
    }

    void Update()
    {
        Keys();
    }

    void Keys()
    {
        if(playerController.playerCharacter == PlayerCharacter.Human)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = ghostTeleportTransform[0].transform.position;
            transform.rotation = ghostTeleportTransform[0].transform.rotation;
            playerController.stunTime = 4.0f;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = ghostTeleportTransform[1].transform.position;
            transform.rotation = ghostTeleportTransform[1].transform.rotation;
            playerController.stunTime = 4.0f;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = ghostTeleportTransform[2].transform.position;
            transform.rotation = ghostTeleportTransform[2].transform.rotation;
            playerController.stunTime = 4.0f;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.position = ghostTeleportTransform[3].transform.position;
            transform.rotation = ghostTeleportTransform[3].transform.rotation;
            playerController.stunTime = 4.0f;
            return;
        }
    }
}
