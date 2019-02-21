using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class MakeDamage : MonoBehaviour {


    //In Progress...
    #region Float
    [SerializeField]
    private float damage;
    #endregion

    #region TakeDamage Class
    [SerializeField]
    private TakeDamage takeDamage;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        damage = 2.0f;
        takeDamage = GameObject.Find("Player").GetComponent<TakeDamage>();
	}

    // Update is called once per frame
    void Update () {
        Atack();
    }
    #endregion

    #region Attack
    private void Atack()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            takeDamage.SubstractHP(damage);
        }
    }
    #endregion
}
