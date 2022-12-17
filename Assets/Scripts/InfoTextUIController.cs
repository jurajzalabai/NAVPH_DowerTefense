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

    // temporary text used for warnings, set temporary visibility
    void Update()
    {
        if (textUI.GetComponent<TextMeshProUGUI>().alpha > 0)
        {
            textUI.GetComponent<TextMeshProUGUI>().alpha -= (0.2f * Time.deltaTime);
        }
    }

    public static void SetText(string text)
    {
        textUI.GetComponent<TextMeshProUGUI>().alpha = 1;
        textUI.GetComponent<TextMeshProUGUI>().text = text;
    }
}
