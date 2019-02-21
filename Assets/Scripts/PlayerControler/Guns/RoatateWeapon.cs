using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoatateWeapon : MonoBehaviour {

    #region System Methods
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * 20.0f);
	}
    #endregion
}
