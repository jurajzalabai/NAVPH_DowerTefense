using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public GameObject shopMenu;
    public GameObject indicator;
    public GameObject shopButton;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            // player is in shop range, he can press shop button to buy
            PlayerController.isInShopArea = true;
            if(shopButton != null){
                shopButton.GetComponent<Button>().interactable = true;
            }
            // deactivate indicator in tutorial scene after user enters shop
            if(indicator != null) {
                indicator.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            // player is not in shop area, deactivate button
            if(shopButton != null){
                shopButton.GetComponent<Button>().interactable = false;
            }
            PlayerController.isInShopArea = false;
            CloseShop();
        }
    }

    public void OpenShop()
    {
        if (PlayerController.isInShopArea){
            shopMenu.SetActive(true);
        }
    }

    public void CloseShop()
    {
        // close shop and hide tooltip system
        shopMenu.SetActive(false);
        TooltipSystem.Hide();
    }

}
