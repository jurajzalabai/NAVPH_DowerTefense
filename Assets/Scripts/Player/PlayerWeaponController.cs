using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerWeaponController : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=fuGQFdhSPg4&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1&t=68s&ab_channel=CodeMonkey


    private Transform aimTransform;
 
    public Animator pointAnimator;
    public GameObject reloadUI;
    public GameObject magazineUI;
    public GameObject HUDUI;

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

    private int index = 0;


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
            if (SecretPathPlayerCollision.playerIn == false)
            {
                //Debug.Log("here");
                HandleShooting();
                reloadUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
            }
            else
            {
                reloadUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
            }

            HandleAiming();

            if (!isReloading)
            {
                HandleInventory();
            }
            HandleReloading();

        }
    }

    public void GetWeapon()
    {
        //Debug.Log(currWeapon.GetComponent<WeaponController>().countAmmo)
        currWeapon = transform.Find("Aim").transform.GetChild(index);
        currWeaponAnimator = currWeapon.GetComponent<WeaponController>().weaponAnimator;
        currWeaponBarrel = currWeapon.GetComponent<WeaponController>().weaponBarrel;
        currWeaponBullet = currWeapon.GetComponent<WeaponController>().weaponBullet;
        currWeaponFireRate = currWeapon.GetComponent<WeaponController>().fireRate;
        currWeaponMagazineSize = currWeapon.GetComponent<WeaponController>().magazineSize;
        currWeaponReloadTime = currWeapon.GetComponent<WeaponController>().reloadTime;
        currWeaponMagazineCount = currWeapon.GetComponent<WeaponController>().magazineCount;
        currWeaponCountAmmo = currWeapon.GetComponent<WeaponController>().countAmmo;
        magazineUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = currWeaponMagazineCount.ToString() + "/" + currWeaponCountAmmo.ToString();

    }

    private void HandleReloading()
    {
       /* if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());

        }*/

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());

        }

        //if (isReloading && currWeaponCountAmmo > 0)
        //{
        //    StartCoroutine(Reload());
        //}
    }

    private void HandleInventory()
    {
        if (!PlayerController.builderMode) {
            reloadUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HUDUI.GetComponent<HUDController>().setActiveSlot(index);
                SetGun(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetGun(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetGun(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetGun(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetGun(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SetGun(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SetGun(6);
            }
        }
        else
        {
            reloadUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
        }
    }

    private void SetGun(int param)
    {
        if (transform.Find("Aim").transform.childCount >= param + 1)
        {
            HUDUI.GetComponent<HUDController>().setActiveSlot(param);
            currWeapon.gameObject.SetActive(false);
            index = param;
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

        if (Input.GetMouseButton(0) && canFire && !isMouseOver && currWeaponMagazineCount > 0 && !isReloading && !IsPointerOverUIElement() && PlayerController.builderMode == false)
        {
            canFire = false;
            currWeaponMagazineCount -= 1;
            currWeapon.GetComponent<WeaponController>().magazineCount -= 1;
            magazineUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = currWeaponMagazineCount.ToString() + "/" + currWeaponCountAmmo.ToString();
            currWeaponAnimator.SetTrigger("Shoot");
            GameObject _bullet = Instantiate(currWeaponBullet, currWeaponBarrel.transform.position, Quaternion.identity);
        }

        if (currWeaponMagazineCount <= 0 && currWeaponCountAmmo > 0 && !isReloading)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
   
        
    }

    IEnumerator Reload()
    {
        while (!(timerReload > currWeaponReloadTime))
        {
            timerReload += Time.deltaTime;
            reloadUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 1 - (timerReload / currWeaponReloadTime);
            yield return null;
        }
        //timerReload += Time.deltaTime;
        //reloadUI.transform.GetChild(0).transform.localScale = new Vector3(timerReload, 1, 1);
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
            reloadUI.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
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
            //yield return null;
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


    //code from https://answers.unity.com/questions/1095047/detect-mouse-events-for-ui-canvas.html
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
