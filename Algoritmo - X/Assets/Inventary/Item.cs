using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public PlayerController playerController;
    [Header("Identificacion")]
    public int ID;
    public string type;
    [Header("Identificacion/Icon")]
    public string descripcion;
    public Sprite icon;

    [HideInInspector]
    public bool pickedUp;

    [HideInInspector]
    public bool equipped;

    [HideInInspector]
    public GameObject weaponManager;

    [HideInInspector]
    public GameObject weapon;

    public bool playerWeapon;
    private void Start()
    {
        weaponManager = GameObject.FindWithTag("WeaponManager");

        if (!playerWeapon)
        {
            int allweapons = weaponManager.transform.childCount;

            for (int i = 0; i < allweapons; i++)
            {
                if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID==ID)
                {
                    weapon = weaponManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }
    private void Update()
    {
        if (equipped)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                equipped = false;
                playerController.tieneArma = false;
            }
            if (equipped==false)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ItemUsage()
    {
        if (type=="Weapon")
        {
                playerController.tieneArma = true;
                weapon.SetActive(true);

                weapon.GetComponent<Item>().equipped = true;

        }
    }
}
