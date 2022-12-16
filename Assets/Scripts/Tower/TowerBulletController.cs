using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletController : MonoBehaviour
{

    private Transform target;

    public float speed = 70.0f;

    private float bulletDamage;

    private float _slowMultiplier = 0f;

    private float _slowDuration = 2.0f;

    private float explosionRadius = 0.0f;

    public GameObject explosionEffect;

    public void SetDamage(float towerDamage)
    {
        bulletDamage = towerDamage;
    }

    public void setSlowMultiplier(float slowMultiplier)
    {
        _slowMultiplier = slowMultiplier;
    }
    public void setSlowDuration(float slowDuration)
    {
        _slowDuration = slowDuration;
    }

    public void SetExplosionRadius(float explosionRadius)
    {
        this.explosionRadius = explosionRadius;
    }

    // tower bullets should always hit enemy they fire after, this function is used for setting target of bullet
    // bullet then chases this target until it is hit by this bullet or enemy is dead
    public void Chase(Transform _target)
    {
        target = _target;

    }

    void Update()
    {
        // if target died, destroy bullets chasing him
        if(target == null) 
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Untagged")
        {
            // on collision with other objects that have collisions enabled, destroy tower bullet
            if (collision.gameObject.tag != "Enemy")
            {
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                // if tower has some particle effect to be used upon explosion create it
                if(explosionEffect){
                    GameObject explosion = (GameObject) Instantiate(explosionEffect, transform.position, transform.rotation);
                    Destroy(explosion, 1f);
                }
                // check if damage from tower should have area effect (splash damage)
                if(explosionRadius == 0.0f){
                    // id explosion radius equals 0, tower can hit only one target
                    collision.gameObject.GetComponent<EnemyController>().Damaged(bulletDamage);
                    
                    // check if weapon should also slow enemies, if yes slow hit enemy by slow multiplier for slowDuration
                    if(_slowMultiplier > 0.0f){
                        collision.gameObject.GetComponent<EnemyController>().Slow(_slowMultiplier, _slowDuration);
                    }
                } else {
                    // tower has explosion radius bigger than 0, damage from tower has area effect
                    Explode();
                }
                Destroy(this.gameObject);
            }
        }
    }

    private void Explode()
    {
        // get all colliders in radius of explosion
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach(Collider2D collider in colliders){
            // for each collider in array, check if it is object of enemy type, if yes damage enemy
            if(collider.gameObject.tag == "Enemy"){
                EnemyController enemyController = collider.gameObject.GetComponent<EnemyController>();
                enemyController.Damaged(bulletDamage);
                // check if turret has also slow effect (we can have area slow towers)
                if(_slowMultiplier > 0.0f){
                    enemyController.Slow(_slowMultiplier, _slowDuration);
                }
            }
        }
    }
}
