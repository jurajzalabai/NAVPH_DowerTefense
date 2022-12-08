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
    public GameObject indicator;
    public GameObject indicatorText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    

    public void GotItBase()
    {
        baseCanvas.SetActive(false);
        secretCanvas.SetActive(true);
        indicator.GetComponent<IndicatorController>().target = secret.transform;
        indicatorText.GetComponent<TextMeshProUGUI>().text = "secret path";
    }

    public void GotItSecret()
    {
        secretCanvas.SetActive(false);
        shopCanvas.SetActive(true);
        indicator.GetComponent<IndicatorController>().target = shop.transform;
        indicatorText.GetComponent<TextMeshProUGUI>().text = "shop";
    }

    public void GotItShop()
    {
        shopCanvas.SetActive(false);
        movesCanvas.SetActive(true);
    }

    public void GotItMoves()
    {
        indicator.SetActive(true);
        movesCanvas.SetActive(false);

        buildCanvas.SetActive(true);
        indicator.GetComponent<IndicatorController>().target = tile.transform;
        indicatorText.GetComponent<TextMeshProUGUI>().text = "tile";

    }

    public void GotItBuild()
    {
        indicator.SetActive(false);
        buildCanvas.SetActive(false);
        GameManager.SetActive(true);


    }
}
