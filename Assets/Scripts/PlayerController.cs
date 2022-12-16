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

    public Texture2D cursorBuild;

    public static bool builderMode = false;

    public static bool isInShopArea = false;

    public static float money = 10000;

    void Awake()
    {
        money = 10000;
    }
    void Start()
    {
        // set health UI element according to current health
        Vector3 locScale = healthUI.transform.Find("Health").gameObject.transform.localScale;
        healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(health / maxHealth, locScale.y, locScale.z);

    }

    void Update()
    {
        // set actual money amount in UI element
        moneyUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = money.ToString() + "$";

        // check if key for switching between shooting and building mode was pressed, if yes change modes
        if(Input.GetKeyDown(builderModeKey)) {
            builderMode = !builderMode;

            // switch to builder mode
            if (builderMode)
            {
                // if player has at least one tower, set it to active tower in inventory
                if (transform.Find("Towers").transform.childCount >= 1){
                    SetTower(0);
                }
                // change cursor image to build cursor
                Cursor.SetCursor(cursorBuild, Vector2.zero, CursorMode.Auto);
                // hide weapon inventory
                HUDUI.SetActive(false);
                TowerHUDUI.SetActive(true);
            }
            // switch to shooting mode
            else
            {
                // change cursor to default one (for shooting)
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                // hide tower inventory
                HUDUI.SetActive(true);
                TowerHUDUI.SetActive(false);
            }
        }

        // check if selected tower from inventory has changed
        HandleTowerInventory();
    }

    // set selected tower from tower inventory based on key pressed
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

    // set new selected tower
    private void SetTower(int param)
    {
        // check if player has at least one tower in inventory
        if (transform.Find("Towers").transform.childCount >= param + 1)
        {
            GameObject turret = transform.Find("Towers").transform.GetChild(param).gameObject;
            // set selected tower in TurretBuildManager singleton
            TurretBuildManager.instance.SetTurretToBuild(turret, param);
        }
    }

    // function that is called when player receives damage, argument hitDamage represents how much damage player received
    public void Damaged(float hitDamage)
    {
        // substract damage value from current health
        health -= hitDamage;
        damagedSound.Play();

        if (health <= 0)
        {
            // player died
            InfoTextUIController.SetText("Player dead");
            Destroy(this.gameObject);
            SceneManager.LoadScene(3);
        }

        // update health UI element based on new health value
        Vector3 locScale = healthUI.transform.Find("Health").gameObject.transform.localScale;
        healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(health / maxHealth, locScale.y, locScale.z);

    }

    // when player kills enemy, this function is called for adding kill money to player
    public static void KillAddMoney(GameObject enemy)
    {
        money += enemy.GetComponent<EnemyController>().killedMoney;
    }

}
