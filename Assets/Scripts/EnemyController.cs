using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    public float fullHealth;
    public float health;
    public float damage;
    public float hitRate;
    public float killedMoney;

    private bool canHit = true;
    private float timer = 0;
    public GameObject bloodEffect;

    private float startSpeed;

    private float slowTime;

    private AIPath aiPath;

    private void Awake()
    {
        if (EnemySpawnerController.difficulty != 0)
        {
            fullHealth = fullHealth * EnemySpawnerController.difficulty;
            health = health * EnemySpawnerController.difficulty;
        }
      
        

        Vector3 locScale = this.transform.Find("UI").transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale;
        this.transform.Find("UI").transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale = new Vector3((health / fullHealth), locScale.y, locScale.z);
    }
    // Start is called before the first frame update
    private void Start()
    { 
        aiPath = gameObject.GetComponent<AIPath>();
        startSpeed = aiPath.maxSpeed;
    }
    public void Damaged(float bulletDamage)
    {
        health -= bulletDamage;
        if (health <= 0)
        {
            PlayerController.KillAddMoney(this.gameObject);
            GameObject blood = Instantiate(bloodEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        Vector3 locScale = this.transform.Find("UI").transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale;
        this.transform.Find("UI").transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale = new Vector3((health / fullHealth), locScale.y, locScale.z);
        
    }

    public void Slow(float slowMultiplier, float slowDuration){ 
        aiPath.maxSpeed = (1 - slowMultiplier) * startSpeed;
        slowTime = slowDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if(slowTime > 0f){
            slowTime -= Time.deltaTime;
        }
        else{
            aiPath.maxSpeed = startSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Untagged")
        {
            if (collision.gameObject.tag == "Player")
            {

                if (canHit)
                {
                    canHit = false;
                    collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
                }

                if (!canHit)
                {
                    StartCoroutine(WaitForHit());
                }
            }
            else if (collision.gameObject.tag == "Base")
            {
                if (canHit)
                {
                    canHit = false;
                    collision.transform.parent.gameObject.GetComponent<BaseController>().Damaged(damage);
                    this.GetComponent<EnemyController>().Damaged(10);
                }

                if (!canHit)
                {
                    StartCoroutine(WaitForHit());
                }
            }
        }
    }

    IEnumerator WaitForHit()
    {
        timer += Time.deltaTime;
        
        if (timer > hitRate)
        {
            canHit = true;
            timer = 0;
        }
        else
        {
            yield return null;
        }

    }
}
