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

    public AudioSource fireSound;

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
        // each half second calculate who next target should be based on distance
        // calling it on each update would be expensive operation
        InvokeRepeating("UpdateEnemies", 0f, 0.5f);
    }

    // code is used from https://www.youtube.com/watch?v=oqidgRQAMB8&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=6
    void UpdateEnemies()
    {
        // find all enemies present on map
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        // calulate closest enemy
        foreach (GameObject enemy in enemies){
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < shortestDistance){
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }

        // set closest enemy as turret target
        if (closestEnemy && shortestDistance <= range){
            target = closestEnemy.transform;
        } else {
            target = null;
        }
    }

    void Update()
    {
        // check if tower has some target it can shoot
        if(target){    
            // rotate tower in direction of enemy
            Vector3 vectorToTarget = target.position - transform.position;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;
            
            Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
            
            towerRotate.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            // check if tower can fire according to cooldown
            if (fireCountdown <= 0f) {
                // shoot after enemy
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        // create new bullet object
        GameObject BulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        TowerBulletController bullet = BulletGO.GetComponent<TowerBulletController>();

        // set private values to bullet based on tower stats
        bullet.SetDamage(damage);
        bullet.setSlowDuration(slowDuration);
        bullet.setSlowMultiplier(slowMultiplier);
        bullet.SetExplosionRadius(splashArea);

        fireSound.Play();

        // chase current target
        if (bullet != null){
            bullet.Chase(target);
        }
    }
}
