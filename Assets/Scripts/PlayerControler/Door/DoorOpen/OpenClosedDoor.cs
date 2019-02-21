using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClosedDoor : MonoBehaviour {

    #region Variables
    [Header("Camera")]
    [SerializeField]
    private Camera playerView;

    [Header("Emergency")]
    [SerializeField]
    private EmergencyLights emergencyLights;

    [Header("DoorIsOpenClass")]
    [SerializeField]
    private DoorIsOpenedAndClosed currentDoorTransform;

    [SerializeField]
    private DoorOpenColseSound doorOpenCloseSound;

    [SerializeField]
    private ButtonClicked currentButtonState;

    [Header("Texture2d Variables")]
    [SerializeField]
    private Texture2D openDoorImage;

    [SerializeField]
    private const float MAXDISTANCEOFRAY = 1.4f;

    [Header("Key To Open")]
    [SerializeField]
    private KeyCode leftMouseKeyDown;
    #endregion

    #region Unity System Methods
    // Use this for initialization
    void Start () {
        playerView = GetComponentInChildren<Camera>();
        emergencyLights = GameObject.FindGameObjectWithTag("LightState").GetComponent<EmergencyLights>();
        leftMouseKeyDown = KeyCode.Mouse0;
    }
	
	// Update is called once per frame
	void Update () {
        RayToDoor();
    }
    #endregion

    #region Ray to Doors
    private void RayToDoor()
    {
        Ray ray = new Ray(playerView.transform.position, playerView.transform.forward);
        RaycastHit hit;
        
        if(Physics.Raycast(ray,out hit,MAXDISTANCEOFRAY))
        {
            if (Input.GetKeyDown(leftMouseKeyDown))
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Buttons"))
                {
                    currentButtonState = hit.collider.gameObject.GetComponent<ButtonClicked>();
                    currentDoorTransform = hit.collider.gameObject.GetComponentInParent<DoorIsOpenedAndClosed>();
                    doorOpenCloseSound = hit.collider.gameObject.GetComponentInParent<DoorOpenColseSound>();
                    currentButtonState.ButtonClick();
                    doorOpenCloseSound.PlayClip();

                    if (emergencyLights.GetLightState() != LightsState.Off)
                    {
                        currentDoorTransform.SetDoorIsOpened();
                    }
                }
            }
        }
    }
    #endregion

    #region GUI
    void OnGUI()
    {
        Ray ray = new Ray(playerView.transform.position, playerView.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISTANCEOFRAY))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Buttons"))
            {
                GUI.DrawTexture(new Rect(Screen.width / 2 - 10.0f, Screen.height / 2 - 10, 30, 30), openDoorImage);
            }
        }
    }
    #endregion
}
