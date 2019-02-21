using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour {
    //Instance of Classes
    #region Instane of class
    //Instance of Inventory class
    [Header("Instace of class")]

    //Player Instance
    [SerializeField]
    private PlayerControler playerControler;

    [SerializeField]
    private ItemsPickUp itemPickUp;

    //Item Canvas
    [SerializeField]
    private Canvas itemCavnas;
    #endregion

    //Bool variables
    #region Bool variables
    [Header("Bool variables")]
    [SerializeField]
    private bool inventoryIsActive = false;
    #endregion

    //Start and Update
    #region Unity system methods
    // Use this for initialization
    void Start () {
        playerControler = GetComponentInParent<PlayerControler>();
        itemPickUp = GetComponentInParent<ItemsPickUp>();
        itemCavnas = GameObject.Find("InventoryCanvas").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        InventoryActions();
    }
    #endregion

    //Active and Disactive inventory
    #region Inventory Actions
    private void InventoryActions()
    {
        SetInventoryIsActive();
        if (inventoryIsActive)
        {
            DisplayInventory();
        }
        else
        {
            HideInventory();
        }
    }
    #endregion

    //Bool setter inventoryIsAcitve setter
    #region Inventory Is Active Setter
    private void SetInventoryIsActive()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryIsActive = !inventoryIsActive;
        }
    }
    #endregion

    //Display methods
    #region Enable And Disable Inventory
    private void DisplayInventory()
    {
        itemCavnas.GetComponent<Canvas>().enabled = true;
        CursorEnable();
        PlayerMoveDisable();
    }

    private void HideInventory()
    {
        itemCavnas.GetComponent<Canvas>().enabled = false;
        CursorDisable();
        PlayerMoveEnable();
    }

    private void CursorEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CursorDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void PlayerMoveEnable()
    {
        playerControler.enabled = true;
        itemPickUp.enabled = true;
    }

    private void PlayerMoveDisable()
    {
        playerControler.enabled = false;
        itemPickUp.enabled = false;
    }
    #endregion
}
