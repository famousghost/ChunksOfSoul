using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPickUp : MonoBehaviour {
    //Instance of Inventory, ImageOFItem, Camera
    #region Instances of class
    [Header("Instances of class")]

    //Instance of item
    [SerializeField]
    private Item[] item;

    //Instance of Item Image
    [SerializeField]
    private ImageOfItem imageOfItem;

    //This instance is helpfull to calculate a camera position
    [SerializeField]
    private Camera playerView;
    #endregion

    //Const MaxDistance of Ray
    #region Const Variables
    [Header("Const Variables")]
    //Max distance of ray how far a player can be from the item
    [SerializeField]
    private const float MAXDISTANCEOFRAY = 2.3f;

    [SerializeField]
    private const int ELEMENTSOFARRAY = 6;
    #endregion

    //String "Cannot carry more item" if player has a 6 items
    #region Carry Item Info
    [SerializeField]
    private string canCarryMoreItem;
    #endregion

    //Bool checking a Player can pick up new item
    #region Can Carry items Check Bool
    [SerializeField]
    private bool canCarryMore = false;
    #endregion

    //Curren items
    #region Current Count Of Items
    [SerializeField]
    private int CurrentNumberOfItems = 0;
    #endregion

    //Timer
    #region Timers
    [SerializeField]
    private float timer = 1.5f;
    #endregion

    //GUI style which helpfull to set Label color text
    #region GUI style
    [SerializeField]
    private GUIStyle colorOfText;
    #endregion

    //Start and Update
    #region Unity System Methods
    // Use this for initialization
    void Start () {
        playerView = GetComponentInChildren<Camera>();
        canCarryMoreItem = "Cannot Carry More Items";
        //Setting a color of our text
        colorOfText.normal.textColor = Color.black;
        colorOfText.fontSize = 20;
        item = new Item[ELEMENTSOFARRAY];
        //This algortim add a item to Array of Items
        for (int i=0;i<ELEMENTSOFARRAY;i++)
        {
            item[i] = GameObject.Find("Item" + i.ToString()).GetComponent<Item>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        PickUpRay();
    }
    #endregion

    //Ray to pick up items
    #region PickUpMethod
    private void PickUpRay()
    {
        Vector3 playerViewforward = playerView.transform.forward;
        Vector3 playerViewPosition = playerView.transform.position;
        Ray ray = new Ray(playerViewPosition, playerViewforward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MAXDISTANCEOFRAY))
        {
            if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Item"))
            {
                GameObject pickedUpItem = hit.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (pickedUpItem != null)
                    {
                        CheckCarryOfItems();
                        imageOfItem = pickedUpItem.GetComponent<ImageOfItem>();
                        AddElementToInventory(imageOfItem.GetItemImage());
                        Debug.Log(CurrentNumberOfItems);
                        Destroy(pickedUpItem);
                    }
                }
            }
        }
    }
    #endregion

    //This Method display a Info about Inventory If player has 6 items cannot carry more items
    #region GUI method
    void OnGUI()
    {
        int stringWidth = 200;
        int stringHeight = 50;
        if (canCarryMore)
        {
            if (timer > 0.0f)
            {
                GUI.Label(new Rect(Screen.width / 2 - (stringWidth / 2), Screen.height / 2, stringWidth, stringHeight), canCarryMoreItem, colorOfText);
                timer -= Time.deltaTime;
            }
        }
    }
    #endregion

    //Adding a Sprite of item
    #region Add Sprite Of Item to Inventory
    private void AddElementToInventory(Sprite imageToAdd)
    {
        if (CurrentNumberOfItems <= ELEMENTSOFARRAY)
        {
            for (int i = 0; i < ELEMENTSOFARRAY; i++)
            {
                if (!item[i].CheckItemImage())
                {
                    item[i].AddItem(imageToAdd);
                    CurrentNumberOfItems++;
                    break;
                }
            }
        }
    }
    #endregion

    //This method is use in itemUse
    #region Substract number Of items Elements
    public void SubtractCurrentNumberOfItem()
    {
        CurrentNumberOfItems--;
    }
    #endregion

    //Check carry of Items and set canCarryMore
    #region Check carrying a items
    private void CheckCarryOfItems()
    {
        if (CurrentNumberOfItems >= ELEMENTSOFARRAY)
        {
            timer = 1.5f;
            canCarryMore = true;
        }
        else
        {
            canCarryMore = false;
        }
    }
    #endregion
}
