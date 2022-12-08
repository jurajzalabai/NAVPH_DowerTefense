using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : MonoBehaviour
{

    private static TooltipSystem current;

    public Tooltip tooltip;


    public void Awake() {
        current = this;
    }

    public static void Show(){
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide(){
        current.tooltip.gameObject.SetActive(false);
    }

    public static void SetText(string header, string[] text, string[] values){
        current.tooltip.headerField.text = header;
        for(int i = 0; i < current.tooltip.text.Length; i++){
            if(text.Length > i){
                current.tooltip.text[i].gameObject.SetActive(true);
                current.tooltip.values[i].gameObject.SetActive(true);

                current.tooltip.text[i].text = text[i];
                current.tooltip.values[i].text = values[i];

            } else{
                current.tooltip.text[i].gameObject.SetActive(false);
                current.tooltip.values[i].gameObject.SetActive(false);
            }
        }
    }

    public static void SetImage(Sprite image){
        current.tooltip.image.GetComponent<Image>().sprite = image;
    }
}
