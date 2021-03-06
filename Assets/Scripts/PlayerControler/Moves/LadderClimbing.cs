﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : MonoBehaviour {

    #region PlayerControlerScript
    [Header("PlayerControler")]
    [SerializeField]
    private PlayerControler playerBody;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        playerBody = GetComponent<PlayerControler>();
    }
    #endregion

    #region Triggers
    void OnTriggerStay(Collider collider)
    {
        if(!playerBody.photonView.IsMine)
        {
            return;
        }
        if(playerBody.playerCharacter == PlayerCharacter.Ghost)
        {
            return;
        }
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            playerBody.GravityDisable();
            if (playerBody.GetRotationeY() >= 0.0f)
            {
                playerBody.isClimbing = false;
                playerBody.PlayerClimb(false);
            }
            else
            {
                playerBody.isClimbing = true;
                playerBody.PlayerClimb(true);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (!playerBody.photonView.IsMine)
        {
            return;
        }
        if (playerBody.playerCharacter == PlayerCharacter.Ghost)
        {
            return;
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            playerBody.GravityEnable();
        }
    }
    #endregion
}
