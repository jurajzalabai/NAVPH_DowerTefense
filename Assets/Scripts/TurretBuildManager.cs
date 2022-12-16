using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuildManager : MonoBehaviour
{

    public static TurretBuildManager instance;

    private GameObject turretToBuild;
    private int inventoryIndex;

    public GameObject towerHUD;

    // create static object on awake (singleton)
    private void Awake() {
        if (instance != null){
            return;
        }

        instance = this;    
    }

    // getter method for private turretToBuild variable
    public GameObject GetTurretToBuild(){
        return turretToBuild;
    }

    // getter method for private inventoryIndex variable
    public int getInventoryIndex(){
        return inventoryIndex;
    }

    // set selected turret and remember turret index in inventory so we can later remove it by index
    public void SetTurretToBuild(GameObject turret, int index) {
        // set slot with selected index as active (change border color)
        towerHUD.GetComponent<TowerHUD>().setActiveSlot(index);
        turretToBuild = turret;
        inventoryIndex = index;
    }
}
