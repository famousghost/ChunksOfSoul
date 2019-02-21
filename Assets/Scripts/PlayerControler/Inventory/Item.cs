using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

    //Current Image and Empty Image
    #region Item Image
    //Main Item Sprite which could be display in inventory
    [Header("Sprite")]
    [SerializeField]
    private Sprite itemSprite;

    //Empty item sprite which is background sprite
    [SerializeField]
    private Sprite emptyItemSprite;
    #endregion

    #region Item String
    [SerializeField]
    private string itemName;
    #endregion

    //Start
    #region Unity System Methods
    // Use this for initialization
    void Start () {
        itemName = "empty";
        emptyItemSprite = GetComponent<Image>().sprite;
        itemSprite = emptyItemSprite;
    }
    #endregion

    //This method is remove a item
    #region Remove Item
    public void RemoveItem()
    {
        this.itemSprite = this.emptyItemSprite;
        this.GetComponent<Image>().sprite = this.itemSprite;
    }
    #endregion

    //This method is adding a item
    #region Add Item
    public void AddItem(Sprite itemSprite)
    {
        this.itemName = itemSprite.name;
        this.itemSprite = itemSprite;
        this.GetComponent<Image>().sprite = this.itemSprite;
    }
    #endregion

    //This method check if item is empty
    #region Check item image
    public bool CheckItemImage()
    {
        if (itemSprite == emptyItemSprite)
        {
            return false;
        }
        return true;
    }
    #endregion

    #region Getter
    public string GetItemName()
    {
        return this.itemName;
    }
    #endregion
}
