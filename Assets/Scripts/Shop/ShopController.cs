using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{

    public GameObject shopMenu;
    public GameObject indicator;
    public GameObject shopButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PlayerController.isInShopArea = true;
            if(shopButton != null){
                shopButton.GetComponent<Button>().interactable = true;
            }
            if(indicator != null) {
                indicator.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
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
        shopMenu.SetActive(false);
        TooltipSystem.Hide();
    }

}
