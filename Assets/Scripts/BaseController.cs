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

    public AudioSource damagedSound;

    // function to be called when base receives damage of hitDamage value
    public void Damaged(float hitDamage)
    {
        health -= hitDamage;
        damagedSound.Play();
        if (health <= 0)
        {
            // base was destroyed
            InfoTextUIController.SetText("Base destroyed");
            Destroy(healthUI.gameObject);
            Destroy(this.gameObject);
            // switch to end scene
            SceneManager.LoadScene(3);
        }
        // update base health UI based on current base health
        Vector3 locScale = healthUI.transform.Find("Health").gameObject.transform.localScale;
        healthUI.transform.Find("Health").gameObject.transform.localScale = new Vector3(health / healthMax, locScale.y, locScale.z);

    }
}
