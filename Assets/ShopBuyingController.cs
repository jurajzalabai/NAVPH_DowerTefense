using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopBuyingController : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject magazineUI;
    public void BuyM4Ammo()
    {
        foreach(var weapon in weapons)
        {
            if (weapon.gameObject.transform.name == "M4")
            {
                weapon.GetComponent<WeaponController>().countAmmo += 30;
                magazineUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = weapon.GetComponent<WeaponController>().magazineCount.ToString() + "/" + weapon.GetComponent<WeaponController>().countAmmo.ToString();
                
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
                magazineUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = weapon.GetComponent<WeaponController>().magazineCount.ToString() + "/" + weapon.GetComponent<WeaponController>().countAmmo.ToString();

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
                magazineUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = weapon.GetComponent<WeaponController>().magazineCount.ToString() + "/" + weapon.GetComponent<WeaponController>().countAmmo.ToString();

            }
        }
    }

}
