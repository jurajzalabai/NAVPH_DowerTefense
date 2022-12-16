using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopBuyingController : MonoBehaviour
{
    public GameObject magazineUI;
    public GameObject HUDUI;
    public GameObject TowerHUDUI;
    public GameObject player;
    public GameObject _base;

    public AudioSource buySound;

    public int PLAYER_HEALTH_COST = 100;
    public int BASE_HEALTH_COST = 100;

    public void buyAmmo(GameObject ammoType)
    {
        // check if player has weapon needed for given ammo type
        if (player.transform.Find("Aim").transform.Find(ammoType.GetComponent<BulletController>().nameOfWeapon))
        {
            // check if player has enough money to buy given ammo
            if (BuyCheckAndBuy(ammoType.GetComponent<BulletController>().costOfAmmo))
            {
                buySound.Play();
                // add ammo for given weapon
                player.transform.Find("Aim").transform.Find(ammoType.GetComponent<BulletController>().nameOfWeapon).GetComponent<WeaponController>().countAmmo += ammoType.GetComponent<BulletController>().defaultAdd;
                player.GetComponent<PlayerWeaponController>().GetWeapon();
            }

        }
    }

    public void buyWeapon(GameObject weaponType)
    {
        // check if there is free space in weapon inventory
        if (HUDUI.GetComponent<HUDController>().CanAdd())
        {
            // check if player already owns weapon of this type, if yes then he can't buy it
            if (!HUDUI.GetComponent<HUDController>().SameType(weaponType.GetComponent<WeaponController>().weaponBullet))
            {
                // check if player has enough money for given weapon
                if (BuyCheckAndBuy(weaponType.GetComponent<WeaponController>().cost))
                {
                    buySound.Play();
                    // create new weapon object
                    GameObject weapon = Instantiate(weaponType, player.transform.position, Quaternion.identity);
                    weapon.SetActive(false);
                    // set parent to aim object and set weapon position to player's aim point
                    weapon.transform.parent = player.transform.Find("Aim");
                    weapon.transform.localPosition = new Vector3(weapon.GetComponent<WeaponController>().x, weapon.GetComponent<WeaponController>().y, 0);
                    weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, -weapon.transform.localScale.y, 1);
                    weapon.transform.name = weapon.GetComponent<WeaponController>().weaponBullet.GetComponent<BulletController>().nameOfWeapon;

                    // add bought weapon to player's inventory
                    HUDUI.GetComponent<HUDController>().AddWeapon(weapon);
                }
            }
        }
    }

    public void buyPlayerHealth()
    {
        if (BuyCheck(PLAYER_HEALTH_COST))
        {
            // check if player already doesn't have full health
            if (player.GetComponent<PlayerController>().health == player.GetComponent<PlayerController>().maxHealth)
            {
                InfoTextUIController.SetText("Full Health");
                return;
            }
            // check if health should be capped at max health value
            else if (player.GetComponent<PlayerController>().health + 40 >= player.GetComponent<PlayerController>().maxHealth)
            {
                player.GetComponent<PlayerController>().health = player.GetComponent<PlayerController>().maxHealth;
            }
            // add health to player
            else
            {
                player.GetComponent<PlayerController>().health += 40;

            }
            buySound.Play();
            BuyCheckAndBuy(PLAYER_HEALTH_COST);
            // update health UI element according to how many % of health player has
            Vector3 locScale = player.GetComponent<PlayerController>().healthUI.transform.Find("Health").gameObject.transform.localScale;
            player.GetComponent<PlayerController>().healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(player.GetComponent<PlayerController>().health / player.GetComponent<PlayerController>().maxHealth, locScale.y, locScale.z);
        }
    }

    // buy health for base
    public void buyBaseHealth()
    {
        // check if player has enough money for buying base health
        if (BuyCheck(BASE_HEALTH_COST))
        {
            // check if base already has full hp
            if (_base.GetComponent<BaseController>().health == _base.GetComponent<BaseController>().healthMax)
            {
                InfoTextUIController.SetText("Full Health of Base");
                return;
            }
            // check if health base should be capped at maximum value
            else if (_base.GetComponent<BaseController>().health + 200 >= _base.GetComponent<BaseController>().healthMax)
            {
                _base.GetComponent<BaseController>().health = _base.GetComponent<BaseController>().healthMax;
            }
            // add health to base
            else
            {
                _base.GetComponent<BaseController>().health += 200;

            }
            buySound.Play();
            BuyCheckAndBuy(BASE_HEALTH_COST);
            // Update health base UI element according to new hp base %
            Vector3 locScale = _base.GetComponent<BaseController>().healthUI.transform.Find("Health").gameObject.transform.localScale;
            _base.GetComponent<BaseController>().healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(_base.GetComponent<BaseController>().health / _base.GetComponent<BaseController>().healthMax, locScale.y, locScale.z);
        }
    }

    public void buyTower(GameObject towerType)
    {
        // Check if new tower can be added to inventory (inventory has free slot)
        if (TowerHUDUI.GetComponent<TowerHUD>().CanAdd()) {
            // check if player has enough money to buy this tower
            if (BuyCheckAndBuy(towerType.GetComponent<TowerController>().cost)) {
                buySound.Play();
                // create new tower object and add it to player towers inventory
                GameObject tower = Instantiate(towerType, player.transform.position, Quaternion.identity);
                tower.transform.parent = player.transform.Find("Towers");
                tower.SetActive(false);
                // add new tower to tower inventory HUD
                TowerHUDUI.GetComponent<TowerHUD>().AddTower(towerType);
            }
        }
    }


    // check if player has enough money for buying item, if yes substract cost of item from player money
    bool BuyCheckAndBuy(float cost)
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


    // check if player has enough money for buying item
    bool BuyCheck(float cost)
    {
        if (PlayerController.money - cost < 0)
        {
            InfoTextUIController.SetText("Not enough money");
            return false;
        }
        else
        {
            return true;
        }
    }

}
