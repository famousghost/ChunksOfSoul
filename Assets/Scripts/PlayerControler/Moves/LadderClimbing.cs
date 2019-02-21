using System.Collections;
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
        if(collider.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            playerBody.GravityDisable();
            if (playerBody.GetRotationeY() >= 0.0f)
            {
                playerBody.PlayerClimb(false);
            }
            else
            {
                playerBody.PlayerClimb(true);
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            playerBody.GravityEnable();
        }
    }
    #endregion
}
