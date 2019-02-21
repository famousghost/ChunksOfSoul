using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemUse : MonoBehaviour, IPointerDownHandler
{
    //Item which could be helpfull to use an item
    #region Items
    [SerializeField]
    private Generator generator;

    [SerializeField]
    private EmergencyCard emergencyCard;
    #endregion

    #region Item Class
    [Header("Instance of item class")]
    [SerializeField]
    protected Item item;

    [SerializeField]
    private ItemsPickUp itemsPickUp;

    [SerializeField]
    protected string nameOfItem;
    #endregion

    #region Floats
    [SerializeField]
    private float timer = -0.1f;
    #endregion

    #region GUI Style
    [SerializeField]
    private GUIStyle colorOfText;
    #endregion

    //Start Method
    #region Unity System Methods
    // Use this for initialization
    void Start()
    {
        //Generator Logic
        emergencyCard = GameObject.Find("UsableItems").GetComponent<EmergencyCard>();
        generator = GameObject.Find("UsableItems").GetComponent<Generator>();
        colorOfText.normal.textColor = Color.red;
        colorOfText.fontSize = 20;
        nameOfItem = "empty";
        item = GetComponent<Item>();
        itemsPickUp = GameObject.Find("Player").GetComponent<ItemsPickUp>();
    }
    #endregion

    //Interaction with mouse
    #region On Pointer Down
    public void OnPointerDown(PointerEventData eventData)
    {
        if (item.CheckItemImage())
        {
            nameOfItem = item.GetItemName();
            Debug.Log(this.gameObject.name + " Is Clicked Item");
            //Items Use IFs
            if (generator.GetUseGenerator() && generator.CheckGeneratorUse(nameOfItem))
            {
                generator.UseGenerator();
                item.RemoveItem();
                itemsPickUp.SubtractCurrentNumberOfItem();
            }
            if (emergencyCard.GetUseCard() && emergencyCard.CheckCardUse(nameOfItem))
            {
                emergencyCard.UseCard();
                item.RemoveItem();
                itemsPickUp.SubtractCurrentNumberOfItem();
            }
            else
            {
                timer = 1.4f;
            }
        }
        else
        {
            Debug.Log(this.gameObject.name + " There is no item");
        }
    }
    #endregion

    #region GUI
    void OnGUI()
    {
        if (timer > 0.0f)
        {
            int stringWidth = 200;
            int stringHeight = 50;
            GUI.Label(new Rect(Screen.width / 2 - (stringWidth / 2), Screen.height / 2, stringWidth, stringHeight), "cannot use here", colorOfText);
            timer -= Time.deltaTime;
        }
    }
    #endregion
}
