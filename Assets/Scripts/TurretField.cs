using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretField : MonoBehaviour
{

    private GameObject turret;

    private Renderer rend;
    private Color startColor;
    public Color hoverColor;

    public GameObject towerHUD;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseEnter() {
        if(canBuild()){
            rend.material.color = hoverColor;
        }
    }

    private void OnMouseExit() {
        rend.material.color = startColor;
    }

    private void OnMouseDown() {
        if(canBuild()){
            buildTurret();
        }
    }

    private void buildTurret() {
        GameObject turretToBuild = TurretBuildManager.instance.GetTurretToBuild();
        if(!turretToBuild){
            return;
        }
        towerHUD.GetComponent<TowerHUD>().RemoveTower(TurretBuildManager.instance.getInventoryIndex());
        turret = (GameObject) Instantiate(turretToBuild, transform.position, transform.rotation);
        turret.SetActive(true);
        turret.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
        TurretBuildManager.instance.SetTurretToBuild(null, -1);
        Destroy(turretToBuild);
        
    }

    private bool canBuild() {
        if(turret != null){
            return false;
        }

        if (CameraController.tabPress){
            return false;
        }
        if (!PlayerController.builderMode){
            return false;
        }
        return true;
    }
}
