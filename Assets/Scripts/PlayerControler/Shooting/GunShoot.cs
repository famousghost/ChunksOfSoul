using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region WeaponSelect Enum
public enum WeaponSelect
{
    Pistol=0,
    MachineGun,
    Bazooka
};
#endregion

public class GunShoot : MonoBehaviour {

    #region Variables
    #region Bool Variables
    [Header("Bool Variables")]
    [SerializeField]
    private bool haveMachineGun = false;

    [SerializeField]
    private bool haveBazooka = false;
    #endregion

    #region GameObjects
    [Header("GameObjects")]
    [SerializeField]
    private GameObject[] weaponsArray;

    [SerializeField]
    private GameObject pistolBulletHole;
    #endregion

    #region Other Class
    [Header("Other Class")]
    [SerializeField]
    private PlayerControler playerControler;
    #endregion

    #region Main Camera
    [Header("Main Camera")]
    [SerializeField]
    private Camera mainCam;
    #endregion

    #region Enumerator
    [Header("Enumerators")]
    [SerializeField]
    private WeaponSelect weaponSelect;
    #endregion

    #region Int Variables
    [Header("Int Variables")]
    [SerializeField]
    private int[] currentReloadAmmo;

    [SerializeField]
    private int[] currentAmmo;

    [SerializeField]
    private int[] allAmmo;

    [SerializeField]
    private readonly int[] MAXAMMO = {120,250,15};

    [SerializeField]
    private int weaponCount = 3;
    #endregion

    #region Rays
    private Ray shootingRay;
    private RaycastHit hitInfo;
    #endregion
    #endregion

    #region System Methods
    // Use this for initialization
    void Start () {
        playerControler = GetComponent<PlayerControler>();
        weaponSelect = WeaponSelect.Pistol;
        allAmmo = new int[weaponCount];
        currentReloadAmmo = new int[weaponCount];
        allAmmo[0] = 80;
        currentAmmo = new int[weaponCount];
        currentReloadAmmo[0] = 25;
        currentReloadAmmo[1] = 50;
        currentReloadAmmo[2] = 1;
        currentAmmo[0] = 25;
        currentAmmo[1] = 50;
        currentAmmo[2] = 1;
        weaponsArray = new GameObject[3];
        mainCam = playerControler.GetComponentInChildren<Camera>();
        pistolBulletHole = Resources.Load("BulletHole", typeof(GameObject)) as GameObject;
        AddWeaponsToArray();

    }
	
	// Update is called once per frame
	void Update () {
        RayCastShoot();
    }
    #endregion

    #region AddWeaponsToArray
    private void AddWeaponsToArray()
    {
        weaponsArray[0] = GameObject.FindGameObjectWithTag("Pistol");
        weaponsArray[1] = GameObject.FindGameObjectWithTag("MachineGun");
        weaponsArray[2] = GameObject.FindGameObjectWithTag("Bazooka");
    }
    #endregion

    #region Shooting Function
    private void RayCastShoot()
    {
        shootingRay = new Ray(mainCam.transform.position, mainCam.transform.forward);
        ChangeWeapon();
        SelectWeapon();
        if (Physics.Raycast(shootingRay, out hitInfo, 100.0f))
        {
            if (weaponSelect == WeaponSelect.Pistol)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (hitInfo.collider.tag == "Enemy")
                    {
                        Debug.Log("trafiłeś wroga");
                    }
                    else if (hitInfo.collider.tag != "Weapon")
                    {
                        Debug.Log("trafiłeś ściane");
                        BulletHole();
                    }
                }
            }
            else if(weaponSelect == WeaponSelect.MachineGun)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (hitInfo.collider.tag == "Enemy")
                    {
                        Debug.Log("trafiłeś wroga");
                    }
                    else if (hitInfo.collider.tag != "Weapon")
                    {
                        Debug.Log("trafiłeś ściane");
                        BulletHole();
                    }
                }
            }
        }
    }
    #endregion

    #region Make Bullet Hole Function
    private void BulletHole()
    {
        if (currentAmmo[0] > 0 && currentAmmo[1] > 0 && weaponSelect != WeaponSelect.Bazooka)
        {
            GameObject go;
            go = Instantiate(pistolBulletHole, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal)) as GameObject;
            Destroy(go, 5);
        }
    }
    #endregion

    #region Select Actual Weapon
    private void SelectWeapon()
    {
        switch(weaponSelect)
        {
            case WeaponSelect.Pistol:
               // currentAmmo = currentReloadAmmo;
                if (currentAmmo[0] > 0 && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    currentAmmo[0]--;
                }
                if(Input.GetKeyDown(KeyCode.R) && allAmmo[0] > 0)
                {
                    if (allAmmo[0] >= currentReloadAmmo[0])
                    {
                        allAmmo[0] -= (currentReloadAmmo[0] - currentAmmo[0]);
                        currentAmmo[0] = currentReloadAmmo[0];
                    }
                    else
                    {
                        allAmmo[0] -= (currentReloadAmmo[0] - currentAmmo[0]);
                        currentAmmo[0] = (currentReloadAmmo[0] - allAmmo[0]);

                    }
                    if (allAmmo[0] <= 0)
                    {
                        allAmmo[0] = 0;
                    }
                }
                break;
            case WeaponSelect.MachineGun:
                // currentAmmo = currentReloadAmmo;
                if (currentAmmo[1] > 0 && Input.GetKey(KeyCode.Mouse0))
                {
                    currentAmmo[1]--;
                }
                if (Input.GetKeyDown(KeyCode.R) && allAmmo[1] > 0)
                {
                    allAmmo[1] -= (currentReloadAmmo[1] - currentAmmo[1]);
                    currentAmmo[1] = currentReloadAmmo[1];
                    if (allAmmo[1] <= 0)
                    {
                        allAmmo[1] = 0;
                    }
                }
                break;
            case WeaponSelect.Bazooka:
                if (currentAmmo[2] > 0 && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    currentAmmo[2]--;
                }
                if (Input.GetKeyDown(KeyCode.R) && allAmmo[2] > 0)
                {
                    allAmmo[2] -= (currentReloadAmmo[2] - currentAmmo[2]);
                    currentAmmo[2] = currentReloadAmmo[2];
                    if (allAmmo[2] <= 0)
                    {
                        allAmmo[2] = 0;
                    }
                }
                break;
            default:
                break;
        }
    }
    #endregion

    #region Change Weapon
    private void ChangeWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSelect = WeaponSelect.Pistol;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && haveMachineGun)
        {
            weaponSelect = WeaponSelect.MachineGun;
            currentReloadAmmo[1] = 50;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && haveBazooka)
        {
            weaponSelect = WeaponSelect.Bazooka;
            currentReloadAmmo[2] = 1;
        }
    }
    #endregion

    #region Setters
    public void SetHaveBazooka(bool haveBazooka)
    {
        this.haveBazooka = haveBazooka;
    }

    public void SetHaveMachineGun(bool haveMachineGun)
    {
        this.haveMachineGun = haveMachineGun;
    }

    public void SetPistolAmmo(int pickUpAmmo)
    {
        if (allAmmo[0] <= MAXAMMO[0])
        {
            allAmmo[0] += pickUpAmmo;
            if(allAmmo[0] >= MAXAMMO[0])
            {
                allAmmo[0] = MAXAMMO[0];
            }
        }
    }

    public void SetMachineGunAmmo(int pickUpAmmo)
    {
        if (allAmmo[1] <= MAXAMMO[1])
        {
            allAmmo[1] += pickUpAmmo;
            if (allAmmo[1] >= MAXAMMO[1])
            {
                allAmmo[1] = MAXAMMO[1];
            }
        }
    }

    public void SetBazookaAmmo(int pickUpAmmo)
    {
        if (allAmmo[2] <= MAXAMMO[2])
        {
            allAmmo[2] += pickUpAmmo;
            if (allAmmo[2] >= MAXAMMO[2])
            {
                allAmmo[2] = MAXAMMO[2];
            }
        }
    }
    #endregion
}
