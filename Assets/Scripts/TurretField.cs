using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretField : MonoBehaviour
{

    private GameObject turret;

    private Renderer rend;

    // starting color of field
    private Color startColor;
    // color of field to be set on hover
    public Color hoverColor;

    // tower inventory
    public GameObject towerHUD;

    void Start()
    {
        // save original color of field, so we can set it back later
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter() {
        // if building on this field is possible, change field color to hoverColor
        if(canBuild()){
            rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit() {
        // change field color back to original value
        rend.material.color = startColor;
    }

    private void OnMouseDown() {
        // on mouse click check if tower can be build here, if yes build tower here
        if(canBuild()){
            buildTurret();
        }
    }

    private void buildTurret() {
        // check if player has selected turret to build
        GameObject turretToBuild = TurretBuildManager.instance.GetTurretToBuild();
        if(!turretToBuild){
            return;
        }
        // Remove turret from inventory, and place it on this field 
        towerHUD.GetComponent<TowerHUD>().RemoveTower(TurretBuildManager.instance.getInventoryIndex());
        turret = (GameObject) Instantiate(turretToBuild, transform.position, transform.rotation);
        turret.SetActive(true);
        turret.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
        TurretBuildManager.instance.SetTurretToBuild(null, -1);
        Destroy(turretToBuild);
        
    }

    // return true if player can build on this field
    private bool canBuild() {
        // if there is already turret present on field, new turret can't be build here
        if(turret != null){
            return false;
        }
        // building isn't possible in tabbed mode
        if (CameraController.tabPress){
            return false;
        }
        // player must be in builder mode to build
        if (!PlayerController.builderMode){
            return false;
        }
        return true;
    }
}
