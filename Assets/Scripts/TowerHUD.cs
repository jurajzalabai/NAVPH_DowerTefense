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

    private int MAX_TOWERS_COUNT = 9;

    // player can have some towers set at the beggining of game, so add them to towers inventory
    private void Awake()
    {
        int poz = player.transform.Find("Towers").transform.childCount;
        towerCount = player.transform.Find("Towers").transform.childCount -1;
        for (int i = 0; i < poz; i++)
        {
            // for every tower set slot image in towers inventory to image of tower
            this.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = player.transform.Find("Towers").transform.GetChild(i).transform.GetComponent<TowerController>().towerImage;
        }
    }

    void Update()
    {
        if(!PlayerController.builderMode){
            // if player turns off builder mode, unset active slot in turret inventory
            setActiveSlot(-1);
        }
    }

    public void AddTower(GameObject tower)
    {
        if (towerCount >= MAX_TOWERS_COUNT)
        {
            InfoTextUIController.SetText("Cannot add tower");
        }
        else
        {
            // If more towers can be bought, increment tower count and add tower to player's tower inventory
            towerCount += 1;
            this.transform.GetChild(0).transform.GetChild(towerCount).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = tower.GetComponent<TowerController>().towerImage;
        }
    }

    public void RemoveTower(int index)
    {
        // remove tower image from tower's inventory
        this.transform.GetChild(0).transform.GetChild(index).transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().sprite = null;

        // move all towers on the rights side of inventory one position to left and update images in towers inventory
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
            GameObject turret = player.transform.Find("Towers").transform.GetChild(player.transform.Find("Towers").transform.childCount - 1).gameObject;
            TurretBuildManager.instance.SetTurretToBuild(turret, player.transform.Find("Towers").transform.childCount - 1);
            setActiveSlot(player.transform.Find("Towers").transform.childCount - 1);
        }
        towerCount -= 1;
    }

    // return true if new tower can be bought
    public bool CanAdd()
    {
        if (towerCount + 1 >= MAX_TOWERS_COUNT)
        {
            InfoTextUIController.SetText("Cannot add tower - full inventory");
            return false;
        }
        else
        {
            return true;
        }
    }

    // set active slot in tower inventory when new tower is selected by player
    public void setActiveSlot(int index) {
        if(activeSlot > -1 && oldColor != null && oldTextColor != null)
        {
            // change border and text color of old active inventory slot to original color
            this.transform.GetChild(0).transform.GetChild(activeSlot).transform.GetComponent<Image>().color = oldColor;
            this.transform.GetChild(0).transform.GetChild(activeSlot).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().color = oldTextColor;
        }

        if (index > -1){
            // change color of inventory slot to active
            Image img = this.transform.GetChild(0).transform.GetChild(index).GetComponent<Image>();
            TextMeshProUGUI text = this.transform.GetChild(0).transform.GetChild(index).transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
            // save original color
            oldTextColor = text.color;
            oldColor = img.color;
            // set new active color
            text.color = activeColor;
            img.color = activeColor;
        }
        activeSlot = index;
    }
}
