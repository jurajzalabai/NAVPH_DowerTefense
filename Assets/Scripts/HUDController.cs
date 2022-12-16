using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    private static int weaponCount = 0;
    public GameObject player;

    public Color activeColor;
    private Color oldColor;
    private Color oldTextColor;
    private int activeSlot = -1;

    private int MAX_WEAPON_COUNT = 8;

    // set initial weapon inventory HUD state according to according to weapons present in player's inventory
    private void Awake()
    {
        setActiveSlot(0);
        int poz = player.transform.Find("Aim").transform.childCount;
        weaponCount = player.transform.Find("Aim").transform.childCount - 1;
        for (int i = 0; i < poz; i++)
        {
            // for every weapon in player's inventory add weapon image to weapon HUD 
            this.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = player.transform.Find("Aim").transform.GetChild(i).transform.GetComponent<WeaponController>().weaponImage;
        }
    }
    public void AddWeapon(GameObject weapon)
    {
        // check if there is empty slot in weapon inventory
        if (weaponCount >= MAX_WEAPON_COUNT)
        {
            InfoTextUIController.SetText("Cannot add weapon");
        }
        else
        {
            // if there is empty slot, add weapon to player's weapon inventory and add weapon image to weapon HUD
            weaponCount += 1;
            this.transform.GetChild(0).transform.GetChild(weaponCount).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = weapon.transform.GetComponent<WeaponController>().weaponImage;
        }
    }

    // check if new weapon can be added to inventory (inventory has free slot)
    public bool CanAdd()
    {
        if (weaponCount + 1 >= MAX_WEAPON_COUNT)
        {
            InfoTextUIController.SetText("Cannot add weapon - full inventory");
            return false;
        }
        else
        {
            return true;
        }
    }

    // check if weapon of given type is already present in player inventory
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

    // set active slot according to current selected weapon (active slot has different text and border color)
    public void setActiveSlot(int index)
    {
        // when active slot changes, set text and border color of previous active slot to original value
        if (activeSlot > -1 && oldColor != null && oldTextColor != null)
        {
            this.transform.GetChild(0).transform.GetChild(activeSlot).transform.GetComponent<Image>().color = oldColor;
            this.transform.GetChild(0).transform.GetChild(activeSlot).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().color = oldTextColor;
        }

        if (index > -1)
        {
            // get border and text of new slot
            Image img = this.transform.GetChild(0).transform.GetChild(index).GetComponent<Image>();
            TextMeshProUGUI text = this.transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
            // remember original color values
            oldTextColor = text.color;
            oldColor = img.color;
            // change new color value according to activeColor
            text.color = activeColor;
            img.color = activeColor;
        }
        activeSlot = index;
    }
}
