using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpiritChunk : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ItemToPickUp")
        {
            Destroy(col.gameObject);
            PlayerManager.instance.spiritChunkCounter++;
            Debug.Log(PlayerManager.instance.spiritChunkCounter);
        }
    }
}
