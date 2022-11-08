using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private static int weaponCount = 0;

    
    public void AddWeapon(GameObject weapon)
    {
        if (weaponCount >= 8)
        {
            InfoTextUIController.SetText("Cannot add weapon");
        }
        else
        {
            weaponCount += 1;
            this.transform.GetChild(0).transform.GetChild(weaponCount).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = weapon.transform.GetComponent<WeaponController>().weaponImage;
        }
    }

    public bool CanAdd()
    {
        if (weaponCount + 1 >= 8)
        {
            InfoTextUIController.SetText("Cannot add weapon - full inventory");
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool SameType(GameObject bullet)
    {
        foreach (Transform child in this.transform.GetChild(0).transform)
        {
            if (child.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite)
            {
                if (child.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite.name == bullet.GetComponent<BulletController>().nameOfWeapon)
                {

                    InfoTextUIController.SetText("Same type of weapon in inventory");
                    return true;
                }
            }
        }
        
        return false;
    }
}
