using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [Header("Tower attributes")]
    public float range = 4.0f;
    public float damage = 10;
    public float fireRate = 1.0f;
    public float cost = 100;
    public float splashArea = 0.5f;
    public float slowDuration = 1f;
    public float turnSpeed = 5f;
    public float slowMultiplier = 0.5f;
    public bool splashDamage = false;
    public float fireCountdown = 0f;

    private Transform target = null;


    [Header("Game objects")]
    public string enemyTag = "Enemy";
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform towerRotate;
    public Sprite towerImage;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateEnemies", 0f, 0.5f);
    }

    // https://www.youtube.com/watch?v=oqidgRQAMB8&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=6
    void UpdateEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies){
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < shortestDistance){
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy && shortestDistance <= range){
            target = closestEnemy.transform;
        } else {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target){    
            Vector3 vectorToTarget = target.position - transform.position;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;
            
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
            
            towerRotate.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            if (fireCountdown <= 0f) {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject BulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        TowerBulletController bullet = BulletGO.GetComponent<TowerBulletController>();

        bullet.SetDamage(damage);
        bullet.setSlowDuration(slowDuration);
        bullet.setSlowMultiplier(slowMultiplier);
        bullet.SetExplosionRadius(splashArea);

        if (bullet != null){
            bullet.Chase(target);
        }
    }
}
