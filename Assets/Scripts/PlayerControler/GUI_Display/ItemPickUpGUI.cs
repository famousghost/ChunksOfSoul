using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpGUI : MonoBehaviour {

    #region Camera
    [SerializeField]
    private Camera playerView;
    #endregion

    #region Texture2d
    [SerializeField]
    private Texture2D pickUpHand;
    #endregion

    #region Const
    [SerializeField]
    private const float MAXDISANCEOFRAY = 2.3f;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        playerView = GetComponentInChildren<Camera>();
    }
    #endregion

    #region GUI
    void OnGUI()
    {
        Ray ray = new Ray(playerView.transform.position, playerView.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISANCEOFRAY))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                GUI.DrawTexture(new Rect((Screen.width / 2) - 25, (Screen.height / 2) - 25, 50, 50), pickUpHand);
            }
        }     
    }
    #endregion
}
