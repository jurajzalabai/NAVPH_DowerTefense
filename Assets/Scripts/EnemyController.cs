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
            if (PlayerPrefs.GetInt("type") == 0)
            {
                fullHealth = fullHealth * EnemySpawnerController.difficulty;
                health = health * EnemySpawnerController.difficulty;
            }
            else
            {
                fullHealth = fullHealth * UnlimitedSpawnerController.difficulty;
                health = health * UnlimitedSpawnerController.difficulty;
            }
        }
        // set UI healthbar for enemy when he is spawned
        Vector3 locScale = this.transform.Find("UI").transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale;
        this.transform.Find("UI").transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale = new Vector3((health / fullHealth), locScale.y, locScale.z);
    }

    private void Start()
    { 
        // set starting speed for enemy
        aiPath = gameObject.GetComponent<AIPath>();
        startSpeed = aiPath.maxSpeed;
    }

    // function is called when enemy receives damage
    public void Damaged(float bulletDamage)
    {
        health -= bulletDamage;
        if (health <= 0)
        {
            // enemy died
            PlayerController.KillAddMoney(this.gameObject);
            GameObject blood = Instantiate(bloodEffect, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        // update enemy healthbar according to new health value
        Vector3 locScale = this.transform.Find("UI").transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale;
        this.transform.Find("UI").transform.Find("EnemyHealthUI").transform.Find("Health").gameObject.transform.localScale = new Vector3((health / fullHealth), locScale.y, locScale.z);
        
    }

    // slow enemy by (slowMultiplier*100)% for slowDuration seconds
    public void Slow(float slowMultiplier, float slowDuration){ 
        aiPath.maxSpeed = (1 - slowMultiplier) * startSpeed;
        slowTime = slowDuration;
    }

    void Update()
    {
        // check if slow duration should end, if yes return speed to original value, otherwise substract passed time from current slowTime
        if(slowTime > 0f){
            slowTime -= Time.deltaTime;
        }
        else{
            aiPath.maxSpeed = startSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Untagged")
        {
            // enemy collision with player
            if (collision.gameObject.tag == "Player")
            {
                // check if enemy is ready for next attack
                if (canHit)
                {
                    // hit player
                    canHit = false;
                    collision.gameObject.GetComponent<PlayerController>().Damaged(damage);
                }

                if (!canHit)
                {
                    // wait before next hit
                    StartCoroutine(WaitForHit());
                }
            }
            // enemy collision with base
            else if (collision.gameObject.tag == "Base")
            {
                // check if enemy is ready for next base attack
                if (canHit)
                {
                    // hit base, substract health hp
                    canHit = false;
                    collision.transform.parent.gameObject.GetComponent<BaseController>().Damaged(damage);
                    this.GetComponent<EnemyController>().Damaged(10);
                }

                if (!canHit)
                {
                    // wait for next hit
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
