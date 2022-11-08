using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector3 mousePos;
    private Rigidbody2D rb;
    public float force;
    public float bulletDamage = 34;
    public float defaultAdd;
    public float costOfAmmo;
    public string nameOfWeapon;
    private GameObject player;


    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
    }
    
    void Start()
    {
        //code from - https://www.youtube.com/watch?v=-bkmPm_Besk&ab_channel=MoreBBlakeyyy
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
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
