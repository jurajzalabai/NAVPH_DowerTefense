using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : MonoBehaviour
{

    private static TooltipSystem current;

    public Tooltip tooltip;


    // create static object reference (singleton)
    public void Awake() {
        current = this;
    }

    // static method to show tooltip
    public static void Show(){
        current.tooltip.gameObject.SetActive(true);
    }

    // static method to hide tooptip
    public static void Hide(){
        current.tooltip.gameObject.SetActive(false);
    }

    // static method to update content of tooltip, input parameters are header, and then array of text and values to be shown in tooltip
    public static void SetText(string header, string[] text, string[] values){
        current.tooltip.headerField.text = header;
        
        // for every element in text array set new text to tooltip 
        for(int i = 0; i < current.tooltip.text.Length; i++){
            if(text.Length > i){
                current.tooltip.text[i].gameObject.SetActive(true);
                current.tooltip.values[i].gameObject.SetActive(true);

                current.tooltip.text[i].text = text[i];
                current.tooltip.values[i].text = values[i];

            } else{
                // if there are less rows needed in tooltip then number of rows that tooltip contains, hide additional rows
                current.tooltip.text[i].gameObject.SetActive(false);
                current.tooltip.values[i].gameObject.SetActive(false);
            }
        }
    }

    public static void SetImage(Sprite image){
        current.tooltip.image.GetComponent<Image>().sprite = image;
    }
}
