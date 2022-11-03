using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Main")]
    public Animator weaponAnimator;
    public GameObject weaponBarrel;
    public GameObject weaponBullet;

    [Header("Stats")]
    //public float force;
    public float fireRate;
    public float magazineSize;
    public float magazineCount;
    public float cost;
    public float reloadTime;
    public float countAmmo;
    public float costAmmo;
    //public float bulletDamage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
