using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStun : MonoBehaviour
{
    [SerializeField]
    private PlayerControler playerController;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "StunObject" && playerController.playerCharacter == PlayerCharacter.Ghost)
        {
            Destroy(col.gameObject);
            playerController.stunTime = 4.0f;
        }
    }
}
