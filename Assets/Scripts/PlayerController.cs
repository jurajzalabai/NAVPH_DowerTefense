using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float health = 100;
    private float shield = 0;
    private float speed = 5;
    public GameObject healthUI;
    public GameObject shieldUI;
    public GameObject moneyUI;

    public static float money = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = money.ToString();
    }

    public void Damaged(float hitDamage)
    {
        health -= hitDamage;
        if (health <= 0)
        {
            InfoTextUIController.SetText("Player dead");
            Destroy(this.gameObject);
        }
        Vector3 locScale = healthUI.transform.Find("Health").gameObject.transform.localScale;
        healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(health / 100, locScale.y, locScale.z);

    }

    public static void KillAddMoney(string name)
    {
        switch (name)
        {
            case "Enemy1(Clone)":
                money += 10;
                break;
            case "Enemy2(Clone)":
                money += 5;
                break;
            case "Enemy3(Clone)":
                money += 20;
                break;
            case "Enemy4(Clone)":
                money += 25;
                break;
            default:
                money += 10;
                break;
        }
    }
}
