using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoTextUIController : MonoBehaviour
{
    public static GameObject textUI;
    // Start is called before the first frame update
    void Awake()
    {
        textUI = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (textUI.GetComponent<TextMeshProUGUI>().alpha > 0)
        {
            textUI.GetComponent<TextMeshProUGUI>().alpha -= (0.3f * Time.deltaTime);
        }
    }

    public static void SetText(string text)
    {
        textUI.GetComponent<TextMeshProUGUI>().alpha = 1;
        textUI.GetComponent<TextMeshProUGUI>().text = text;
    }
}
