using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Select Weapon Enum
public enum SelectWeapon
{
    pistol = 0,
    bazooka,
    machineGun
};
#endregion

public class GunPickUp : MonoBehaviour {

    #region Variables
    #region GameObjects
    [Header("Game Objects")]
    [SerializeField]
    private GameObject machineGun;

    [SerializeField]
    private GameObject machineGunToDestroy;

    [SerializeField]
    private GameObject bazooka;

    [SerializeField]
    private GameObject bazookaToDestroy;
    #endregion

    #region Other Class
    [Header("Other Class")]
    [SerializeField]
    private GunShoot gunShoot;
    #endregion

    #region SelectWeapon Enumerator
    [Header("Enumerators")]
    [SerializeField]
    private SelectWeapon selectWeapon;
    #endregion

    #region Float Variables
    [Header("Float Variables")]
    [SerializeField]
    private float timer= 0.0f;
    #endregion

    #region Bool Variables
    [Header("Bool Variables")]
    [SerializeField]
    private bool weaponIsPickedUp = false;
    #endregion
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        gunShoot = GameObject.Find("Player").GetComponent<GunShoot>();
        if(selectWeapon == SelectWeapon.machineGun)
        {
            machineGun = Resources.Load("Guns/MachineGun", typeof(GameObject)) as GameObject;
            timer = 60.0f;
            machineGunToDestroy = Instantiate(machineGun, this.transform);
        }
        if(selectWeapon == SelectWeapon.bazooka)
        {
            bazooka = Resources.Load("Guns/Bazooka", typeof(GameObject)) as GameObject;
            timer = 120.0f;
            bazookaToDestroy = Instantiate(bazooka, this.transform);
        }
       
    }
	
	// Update is called once per frame
	void Update () {
        MachineGunAppear();
        BazookaAppear();
    }
    #endregion

    #region PickUpWeapon
    public void PickUpWeapon()
    {
        if (selectWeapon == SelectWeapon.machineGun)
        {
            gunShoot.SetHaveMachineGun(true);
            gunShoot.SetMachineGunAmmo(50);
            Destroy(machineGunToDestroy);
            weaponIsPickedUp = true;
        }
        if(selectWeapon == SelectWeapon.bazooka)
        {
            gunShoot.SetHaveBazooka(true);
            gunShoot.SetBazookaAmmo(3);
            Destroy(bazookaToDestroy);
            weaponIsPickedUp = true;
        }
    }
    #endregion

    #region MachineGunAppear
    private void MachineGunAppear()
    {
        if (selectWeapon == SelectWeapon.machineGun)
        {
            if (weaponIsPickedUp && timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0.0f)
            {
                machineGunToDestroy = Instantiate(machineGun, this.transform);
                timer = 60.0f;
                weaponIsPickedUp = false;
            }
        }
    }
    #endregion

    #region BazookaAppear
    private void BazookaAppear()
    {
        if (selectWeapon == SelectWeapon.bazooka)
        {
            if (weaponIsPickedUp && timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0.0f)
            {
                bazookaToDestroy = Instantiate(bazooka, this.transform);
                timer = 120.0f;
                weaponIsPickedUp = false;
            }
        }
    }
    #endregion
}
