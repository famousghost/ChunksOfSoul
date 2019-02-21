using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIsOpenedAndClosed : MonoBehaviour {

    #region Variables
    [SerializeField]
    private GameObject thisDoor;

    [SerializeField]
    private EmergencyLights emergencyLights;

    [SerializeField]
    private Transform doorOpened1;

    [SerializeField]
    private Transform doorClosed1;

    [SerializeField]
    private Transform doorOpened2;

    [SerializeField]
    private Transform doorClosed2;

    [SerializeField]
    private bool doorIsOpened;

    [SerializeField]
    private float openingSpeed = 2.0f;

    [SerializeField]
    private bool doorShutdown = false;
    #endregion

    #region Unity System Methods
    // Use this for initialization
    void Start()
    {
        emergencyLights = GameObject.FindGameObjectWithTag("LightState").GetComponent<EmergencyLights>();
        doorIsOpened = false;
        thisDoor = this.gameObject;
        doorOpened1 = thisDoor.transform.Find("Position/opened1").GetComponent<Transform>();
        doorClosed1 = thisDoor.transform.Find("Position/closed1").GetComponent<Transform>();
        doorOpened2 = thisDoor.transform.Find("Position/opened2").GetComponent<Transform>();
        doorClosed2 = thisDoor.transform.Find("Position/closed2").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(emergencyLights.GetLightState() != LightsState.Off)
            ChangeDoorState();
    }
    #endregion

    #region Door Open and Close
    private void DoorOpen()
    {
        thisDoor.transform.Find("CorridorDoors1").position = Vector3.MoveTowards(thisDoor.transform.Find("CorridorDoors1").position, doorOpened1.position, openingSpeed * Time.deltaTime);
        thisDoor.transform.Find("CorridorDoors2").position = Vector3.MoveTowards(thisDoor.transform.Find("CorridorDoors2").position, doorOpened2.position, openingSpeed * Time.deltaTime);
    }

    private void DoorClose()
    {
        thisDoor.transform.Find("CorridorDoors1").position = Vector3.MoveTowards(thisDoor.transform.Find("CorridorDoors1").position, doorClosed1.position, openingSpeed * Time.deltaTime);
        thisDoor.transform.Find("CorridorDoors2").position = Vector3.MoveTowards(thisDoor.transform.Find("CorridorDoors2").position, doorClosed2.position, openingSpeed * Time.deltaTime);
    }

    private void ChangeDoorState()
    {
        if (doorIsOpened)
        {
            DoorOpen();
        }
        else
        {
            DoorClose();
        }
    }
    #endregion

    #region Setter
    public void SetDoorIsOpened()
    {
        if(!doorShutdown)
        this.doorIsOpened = !this.doorIsOpened;
    }

    public void SetDoorClosed()
    {
        this.doorIsOpened = false;
        doorShutdown = true;
    }

    public void SetDoorShutdownFalse()
    {
        doorShutdown = false;
    }
    #endregion

    #region Getter
    public bool GetDoorIsOpened()
    {
        return this.doorIsOpened;
    }
    #endregion


}
