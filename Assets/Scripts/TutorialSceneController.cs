using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialSceneController : MonoBehaviour
{
    public GameObject baseCanvas;

    public GameObject secretCanvas;
    public GameObject secret;

    public GameObject shopCanvas;
    public GameObject shop;

    public GameObject movesCanvas;

    public GameObject buildCanvas;
    public GameObject tile;


    public GameObject GameManager;



    [Space(10)]
    // indicator (arrow) showing direction to player in tutorial scene
    public GameObject indicator;
    public GameObject indicatorText;

    void Update()
    {
        if (!GameManager.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (movesCanvas.activeSelf)
                {
                    GotItMoves();
                }
                else
                {
                    InfoTextUIController.SetText("Follow the tutorial!");
                }
            }
        }
      
    }
    
    // call this function when base part of tutorial was completed
    public void GotItBase()
    {
        baseCanvas.SetActive(false);
        secretCanvas.SetActive(true);
        indicator.GetComponent<IndicatorController>().target = secret.transform;
        indicatorText.GetComponent<TextMeshProUGUI>().text = "secret path";
    }


    // call this function when secret paths part of tutorial was completed
    public void GotItSecret()
    {
        secretCanvas.SetActive(false);
        shopCanvas.SetActive(true);
        indicator.GetComponent<IndicatorController>().target = shop.transform;
        indicatorText.GetComponent<TextMeshProUGUI>().text = "shop";
    }


    // call this function when shop part of tutorial was completed
    public void GotItShop()
    {
        shopCanvas.SetActive(false);
        movesCanvas.SetActive(true);
    }


    // call this function when possible moves part of tutorial was completed
    public void GotItMoves()
    {
        indicator.SetActive(true);
        movesCanvas.SetActive(false);

        buildCanvas.SetActive(true);
        indicator.GetComponent<IndicatorController>().target = tile.transform;
        indicatorText.GetComponent<TextMeshProUGUI>().text = "tile";

    }


    // call this function when build part of tutorial was completed
    public void GotItBuild()
    {
        indicator.SetActive(false);
        buildCanvas.SetActive(false);
        GameManager.SetActive(true);


    }
}
