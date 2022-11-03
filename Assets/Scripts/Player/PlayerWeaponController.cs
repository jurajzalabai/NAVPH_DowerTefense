using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerWeaponController : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=fuGQFdhSPg4&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1&t=68s&ab_channel=CodeMonkey


    private Transform aimTransform;
 
    public Animator pointAnimator;
    public GameObject reloadUI;
    public GameObject magazineUI;

    private Transform currWeapon;
    private Animator currWeaponAnimator;
    private GameObject currWeaponBarrel;
    private GameObject currWeaponBullet;
    private float currWeaponFireRate;
    private float currWeaponReloadTime;
    private float currWeaponMagazineSize;
    private float currWeaponMagazineCount;
    private float currWeaponCountAmmo;

    private bool canFire;
    private float timer;
    private float timerReload;


    private float refillAmmo = 0;
    private bool isMouseOver = false;
    private bool isReloading = false;

    //public GameObject barell;
    //public GameObject bullet;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    void Awake()
    {
        GetWeapon();
        aimTransform = transform.Find("Aim");
      
    }

    // Update is called once per frame
    void Update()
    {

        if (CameraController.tabPress == false)
        {
            HandleAiming();
            HandleShooting();
            if (!isReloading)
            {
                HandleInventory();
            }
            HandleReloading();

        }
    }

    private void GetWeapon()
    {
        currWeapon = transform.Find("Aim").transform.GetChild(0);
        currWeaponAnimator = currWeapon.GetComponent<WeaponController>().weaponAnimator;
        currWeaponBarrel = currWeapon.GetComponent<WeaponController>().weaponBarrel;
        currWeaponBullet = currWeapon.GetComponent<WeaponController>().weaponBullet;
        currWeaponFireRate = currWeapon.GetComponent<WeaponController>().fireRate;
        currWeaponMagazineSize = currWeapon.GetComponent<WeaponController>().magazineSize;
        currWeaponReloadTime = currWeapon.GetComponent<WeaponController>().reloadTime;
        currWeaponMagazineCount = currWeapon.GetComponent<WeaponController>().magazineCount;
        currWeaponCountAmmo = currWeapon.GetComponent<WeaponController>().countAmmo;
       /* if (currWeaponCountAmmo > currWeapon.GetComponent<WeaponController>().magazineSize)
        {
            currWeaponCountAmmo = currWeapon.GetComponent<WeaponController>().countAmmo - currWeaponMagazineSize;
        }
        else
        {
            currWeaponCountAmmo = currWeapon.GetComponent<WeaponController>().countAmmo - currWeaponMagazineCount;
        }*/
        magazineUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = currWeaponMagazineCount.ToString() + "/" + currWeaponCountAmmo.ToString();

    }

    private void HandleReloading()
    {
       /* if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());

        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
           
        }

        if (isReloading && currWeaponCountAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    private void HandleInventory()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currWeapon.gameObject.SetActive(false);
            aimTransform.transform.Find("Pistol").transform.SetSiblingIndex(0);
            GetWeapon();
            currWeapon.gameObject.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currWeapon.gameObject.SetActive(false);
            aimTransform.transform.Find("M4").transform.SetSiblingIndex(0);
            GetWeapon();
            currWeapon.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currWeapon.gameObject.SetActive(false);
            aimTransform.transform.Find("Uzi").transform.SetSiblingIndex(0);
            GetWeapon();
            currWeapon.gameObject.SetActive(true);
        }
    }
    
    private void HandleShooting()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > currWeaponFireRate)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire && !isMouseOver && currWeaponMagazineCount > 0 && !isReloading)
        {
            canFire = false;
            currWeaponMagazineCount -= 1;
            currWeapon.GetComponent<WeaponController>().magazineCount -= 1;
            magazineUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = currWeaponMagazineCount.ToString() + "/" + currWeaponCountAmmo.ToString();
            currWeaponAnimator.SetTrigger("Shoot");
            GameObject _bullet = Instantiate(currWeaponBullet, currWeaponBarrel.transform.position, Quaternion.identity);
        }

        if (currWeaponMagazineCount <= 0 && currWeaponCountAmmo > 0)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
   
        
    }

    IEnumerator Reload()
    {
        timerReload += Time.deltaTime;
        reloadUI.transform.GetChild(0).transform.localScale = new Vector3(timerReload, 1, 1);
        if (timerReload > currWeaponReloadTime)
        {
            if (currWeaponCountAmmo > currWeapon.GetComponent<WeaponController>().magazineSize)
            {
                refillAmmo = currWeapon.GetComponent<WeaponController>().magazineSize - currWeaponMagazineCount;
            }
            else
            {
                refillAmmo = currWeapon.GetComponent<WeaponController>().magazineSize - currWeaponMagazineCount;
                if (refillAmmo > currWeaponCountAmmo)
                {
                    refillAmmo = currWeaponCountAmmo;
                } 
            }
            canFire = true;
            timerReload = 0;
            reloadUI.transform.GetChild(0).transform.localScale = new Vector3(0, 1, 1);
            if (currWeaponCountAmmo > currWeapon.GetComponent<WeaponController>().magazineSize)
            {
                currWeaponMagazineCount += refillAmmo;
                currWeapon.GetComponent<WeaponController>().magazineCount += refillAmmo;
                //currWeaponMagazineCount = currWeapon.GetComponent<WeaponController>().magazineSize;
                //currWeapon.GetComponent<WeaponController>().magazineCount = currWeapon.GetComponent<WeaponController>().magazineSize;
                currWeaponCountAmmo -= refillAmmo;
                currWeapon.GetComponent<WeaponController>().countAmmo -= refillAmmo;

            }
            else
            {
                currWeaponMagazineCount += refillAmmo;
                currWeapon.GetComponent<WeaponController>().magazineCount += refillAmmo;
                currWeaponCountAmmo -= refillAmmo;
                currWeapon.GetComponent<WeaponController>().countAmmo -= refillAmmo;
            }
            magazineUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = currWeaponMagazineCount.ToString() + "/" + currWeaponCountAmmo.ToString();
            isReloading = false;
            yield return null;
        }
    }

    private void HandleAiming()
    {

        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //Debug.Log(angle + 180);
        pointAnimator.SetFloat("Angle", angle + 180);
        if (angle + 180 <= 90 || angle + 180 >= 270)
        {
            aimTransform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            aimTransform.localScale = new Vector3(1, 1, 1);
        }
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }


    void OnMouseOver()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }


    public static Vector3 GetMouseWolrdPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWolrdPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWolrdPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWolrdPositionWithZ(Input.mousePosition, worldCamera);
    }

   
}
