using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100;
    public GameObject bloodEffect;

    // Start is called before the first frame update
    private void Start()
    { 
    }
    public void Damaged(float bulletDamage)
    {
        health -= bulletDamage;
        if (health <= 0)
        {
            GameObject blood = Instantiate(bloodEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        Vector3 locScale = this.transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale;
        this.transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale = new Vector3(health / 100, locScale.y, locScale.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
