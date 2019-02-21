using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageOfItem : MonoBehaviour {
    //Image to our game object every Object will has a IamgeOfItem and we will set 
    #region Iamge of Our Item
    [SerializeField]
    private Sprite itemImage;
    #endregion

    #region Getter To Sprite
    public Sprite GetItemImage()
    {
        return itemImage;
    }
    #endregion
}
