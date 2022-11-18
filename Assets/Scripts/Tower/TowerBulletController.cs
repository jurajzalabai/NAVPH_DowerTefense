using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletController : MonoBehaviour
{

    private Transform target;

    public float speed = 70.0f;

    private float bulletDamage;

    public void SetDamage(float towerDamage)
    {
        bulletDamage = towerDamage;
    }

    public void Chase(Transform _target)
    {
        target = _target;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            if (collision.gameObject.tag != "Enemy")
            {
                Destroy(this.gameObject);
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyController>().Damaged(bulletDamage);
                Destroy(this.gameObject);
            }
        }
    }
}
