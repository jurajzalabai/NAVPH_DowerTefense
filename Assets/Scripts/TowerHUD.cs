using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerHUD : MonoBehaviour
{
    private static int towerCount = 0;
    public GameObject player;
    public Color activeColor;
    private Color oldTextColor;
    private Color oldColor;
    private int activeSlot = -1;

    private void Awake()
    {
        int poz = player.transform.Find("Towers").transform.childCount;
        towerCount = player.transform.Find("Towers").transform.childCount -1;
        for (int i = 0; i < poz; i++)
        {
            this.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = player.transform.Find("Towers").transform.GetChild(i).transform.GetComponent<TowerController>().towerImage;
        }
    }

    void Update()
    {
        if(!PlayerController.builderMode){
            setActiveSlot(-1);
        }
    }

    public void AddTower(GameObject tower)
    {
        if (towerCount >= 9)
        {
            InfoTextUIController.SetText("Cannot add tower");
        }
        else
        {
            towerCount += 1;
            this.transform.GetChild(0).transform.GetChild(towerCount).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = tower.GetComponent<TowerController>().towerImage;
        }
    }

    public void RemoveTower(int index)
    {
        this.transform.GetChild(0).transform.GetChild(index).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = null;

        for (int i = index; i <= towerCount; i++){
            Image nextImage;
            if(i==towerCount){
                nextImage = null;
            } else{
                nextImage = this.transform.GetChild(0).transform.GetChild(i+1).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>();
            }
            Image currImage = this.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>();
            currImage.sprite = nextImage?.sprite;
        }
        if (player.transform.Find("Towers").transform.childCount >= 1)
        {
            //activeSlot = player.transform.Find("Towers").transform.childCount - 1;
            GameObject turret = player.transform.Find("Towers").transform.GetChild(player.transform.Find("Towers").transform.childCount - 1).gameObject;
            TurretBuildManager.instance.SetTurretToBuild(turret, player.transform.Find("Towers").transform.childCount - 1);
            setActiveSlot(player.transform.Find("Towers").transform.childCount - 1);
        }
        towerCount -= 1;
    }

    public bool CanAdd()
    {
        if (towerCount + 1 >= 9)
        {
            InfoTextUIController.SetText("Cannot add tower - full inventory");
            return false;
        }
        else
        {
            return true;
        }
    }

    public void setActiveSlot(int index) {
        if(activeSlot > -1 && oldColor != null && oldTextColor != null)
        {
            this.transform.GetChild(0).transform.GetChild(activeSlot).transform.GetComponent<Image>().color = oldColor;
            this.transform.GetChild(0).transform.GetChild(activeSlot).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().color = oldTextColor;

        }

        if (index > -1){
            Image img = this.transform.GetChild(0).transform.GetChild(index).GetComponent<Image>();
            TextMeshProUGUI text = this.transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
            oldTextColor = text.color;
            oldColor = img.color;
            text.color = activeColor;
            img.color = activeColor;
        }
        activeSlot = index;
    }
}
