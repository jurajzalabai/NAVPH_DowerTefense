using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    public GameObject shopMenu;
    public GameObject indicator;
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
            if(indicator != null) {
                indicator.SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            CloseShop();
        }
    }

    public void OpenShop()
    {
        shopMenu.SetActive(true);
    }

    public void CloseShop()
    {
        shopMenu.SetActive(false);
    }

}
