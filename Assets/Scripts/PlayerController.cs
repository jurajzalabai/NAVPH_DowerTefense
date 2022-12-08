using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float health = 150;
    public float maxHealth = 150;
    private float shield = 0;
    private float speed = 5;
    public GameObject healthUI;
    public GameObject shieldUI;
    public GameObject moneyUI;
    public KeyCode builderModeKey;

    public GameObject HUDUI;
    public GameObject TowerHUDUI;

    public AudioSource damagedSound;

    public static bool builderMode = false;

    public static float money = 1000;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 locScale = healthUI.transform.Find("Health").gameObject.transform.localScale;
        healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(health / maxHealth, locScale.y, locScale.z);

    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = money.ToString() + "$";

        if(Input.GetKeyDown(builderModeKey)) {
            builderMode = !builderMode;
            if (builderMode)
            {
                if (transform.Find("Towers").transform.childCount >= 1){
                    SetTower(0);
                }
                HUDUI.SetActive(false);
                TowerHUDUI.SetActive(true);
            }
            else
            {
                HUDUI.SetActive(true);
                TowerHUDUI.SetActive(false);
            }
            //Debug.Log("Builder mode active: " + builderMode);
        }

        HandleTowerInventory();
    }


    private void HandleTowerInventory()
    {
        if (PlayerController.builderMode) {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetTower(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetTower(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetTower(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetTower(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetTower(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SetTower(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SetTower(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SetTower(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SetTower(8);
            }
        }
    }

    private void SetTower(int param)
    {
        if (transform.Find("Towers").transform.childCount >= param + 1)
        {
            GameObject turret = transform.Find("Towers").transform.GetChild(param).gameObject;
            Debug.Log(turret);
            Debug.Log(TurretBuildManager.instance);
            TurretBuildManager.instance.SetTurretToBuild(turret, param);
        }
    }

    public void Damaged(float hitDamage)
    {
        health -= hitDamage;
        damagedSound.Play();
        if (health <= 0)
        {
            InfoTextUIController.SetText("Player dead");
            Destroy(this.gameObject);
            SceneManager.LoadScene(3);
        }
        Vector3 locScale = healthUI.transform.Find("Health").gameObject.transform.localScale;
        healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(health / maxHealth, locScale.y, locScale.z);

    }

    public static void KillAddMoney(GameObject enemy)
    {
        money += enemy.GetComponent<EnemyController>().killedMoney;
    }

}
