using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Main")]
    public Animator weaponAnimator;
    public GameObject weaponBarrel;
    public GameObject weaponBullet;
    public Sprite weaponImage;

    [Header("Stats")]
    //public float force;
    public float fireRate;
    public float magazineSize;
    public float magazineCount;
    public float cost;
    public float reloadTime;
    public float countAmmo;
    public float costAmmo;

    [Header("Position offset")]
    public float x;
    public float y;

    public AudioSource weaponAudio;

    public AudioSource reloadAudio;

}
