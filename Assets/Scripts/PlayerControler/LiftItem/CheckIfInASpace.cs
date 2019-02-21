using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfInASpace : MonoBehaviour {

    #region Triggers
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Lifted") && other.gameObject.tag == "Player")
        //Debug.Log("udalo sie");
        other.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
    #endregion

}
