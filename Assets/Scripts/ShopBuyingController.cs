using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopBuyingController : MonoBehaviour
{
    public GameObject magazineUI;
    public GameObject HUDUI;
    public GameObject player;

    public void buyAmmo(GameObject ammoType)
    {
        if (player.transform.Find("Aim").transform.Find(ammoType.GetComponent<BulletController>().nameOfWeapon))
        {
            if (BuyCheck(ammoType.GetComponent<BulletController>().costOfAmmo))
            {
                player.transform.Find("Aim").transform.Find(ammoType.GetComponent<BulletController>().nameOfWeapon).GetComponent<WeaponController>().countAmmo += ammoType.GetComponent<BulletController>().defaultAdd;
                player.GetComponent<PlayerWeaponController>().GetWeapon();
                //PlayerController.money -= ammoType.GetComponent<BulletController>().costOfAmmo;
            }

        }
    }

    public void buyWeapon(GameObject weaponType)
    {
        if (HUDUI.GetComponent<HUDController>().CanAdd())
        {
            if (!HUDUI.GetComponent<HUDController>().SameType(weaponType.GetComponent<WeaponController>().weaponBullet))
            {
                if (BuyCheck(weaponType.GetComponent<WeaponController>().cost))
                {
                    GameObject weapon = Instantiate(weaponType, player.transform.position, Quaternion.identity);
                    weapon.SetActive(false);
                    weapon.transform.parent = player.transform.Find("Aim");
                    weapon.transform.localPosition = new Vector3(weapon.GetComponent<WeaponController>().x, weapon.GetComponent<WeaponController>().y, 0);
                    weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, -weapon.transform.localScale.y, 1);
                    weapon.transform.name = weapon.GetComponent<WeaponController>().weaponBullet.GetComponent<BulletController>().nameOfWeapon;
                    HUDUI.GetComponent<HUDController>().AddWeapon(weapon);
                    //PlayerController.money -= weapon.GetComponent<WeaponController>().cost;
                }
            }
        }
        
        //player.transform.Find("Aim").transform.Find(ammoType.GetComponent<BulletController>().nameOfWeapon).GetComponent<WeaponController>().countAmmo += ammoType.GetComponent<BulletController>().defaultAdd;
        //player.GetComponent<PlayerWeaponController>().GetWeapon();
    }

    bool BuyCheck(float cost)
    {
        if (PlayerController.money - cost < 0)
        {
            InfoTextUIController.SetText("Not enough money");
            return false;
        }
        else
        {
            PlayerController.money -= cost;
            return true;
        }
    }
    /* public void BuyM4Ammo()
     {
         foreach(var weapon in weapons)
         {
             if (weapon.gameObject.transform.name == "M4")
             {
                 weapon.GetComponent<WeaponController>().countAmmo += 30;

             }
         }
     }

     public void BuyPistolAmmo()
     {
         foreach (var weapon in weapons)
         {
             if (weapon.gameObject.transform.name == "Pistol")
             {
                 weapon.GetComponent<WeaponController>().countAmmo += 12;
                 player.GetComponent<PlayerWeaponController>().GetWeapon();
             }
         }
     }

     public void BuyUziAmmo()
     {
         foreach (var weapon in weapons)
         {
             if (weapon.gameObject.transform.name == "Uzi")
             {
                 weapon.GetComponent<WeaponController>().countAmmo += 20;
                 player.GetComponent<PlayerWeaponController>().GetWeapon();
             }
         }
     }*/

}
