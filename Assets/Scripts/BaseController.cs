using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseController : MonoBehaviour
{
    public float health = 2000;
    public float healthMax = 2000;
    public GameObject healthUI;

    

    public void Damaged(float hitDamage)
    {
        health -= hitDamage;
        if (health <= 0)
        {
            InfoTextUIController.SetText("Base destroyed");
            Destroy(healthUI.gameObject);
            Destroy(this.gameObject);
            SceneManager.LoadScene(3);
        }
        Vector3 locScale = healthUI.transform.Find("Health").gameObject.transform.localScale;
        healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(health / healthMax, locScale.y, locScale.z);

    }
}
