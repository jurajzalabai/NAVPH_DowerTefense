using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuildManager : MonoBehaviour
{

    public static TurretBuildManager instance;

    private GameObject turretToBuild;
    private int inventoryIndex;

    public GameObject towerHUD;

    private void Awake() {
        if (instance != null){
            return;
        }

        instance = this;    
    }

    public GameObject GetTurretToBuild(){
        return turretToBuild;
    }

    public int getInventoryIndex(){
        return inventoryIndex;
    }

    public void SetTurretToBuild(GameObject turret, int index) {
        towerHUD.GetComponent<TowerHUD>().setActiveSlot(index);
        turretToBuild = turret;
        inventoryIndex = index;
    }

    // Start is called before the first frame update
    void Start()
    {
        // turretToBuild = turretPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
