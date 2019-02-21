using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage: MonoBehaviour {

    #region Float
    [SerializeField]
    private float hp;
    #endregion

    #region Const
    [SerializeField]
    private const float MAXHP = 100.0f;
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        hp = MAXHP;
	}
    #endregion

    #region SubstractHP
    public void SubstractHP(float damage)
    {
        hp -= damage;
    }
    #endregion
}
