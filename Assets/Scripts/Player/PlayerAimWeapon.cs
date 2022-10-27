using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    //code from https://www.youtube.com/watch?v=fuGQFdhSPg4&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1&t=68s&ab_channel=CodeMonkey


    private Transform aimTransform;

    public Animator aimAnimator;
    public Animator pointAnimator;

    public GameObject barell;
    public GameObject bullet;
    // Start is called before the first frame update
    void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraController.tabPress == false)
        {
        HandleAiming();
        HandleShooting();

        }
    }

    private void HandleShooting()
    {
        //if (Input.GetMouseButton(0))
        if (Input.GetMouseButton(0) && aimAnimator.GetCurrentAnimatorStateInfo(0).IsName("weapon_idle"))
        {
            aimAnimator.SetTrigger("Shoot");
            GameObject _bullet = Instantiate(bullet, barell.transform.position, Quaternion.identity);
        }
    }

    private void HandleAiming()
    {

        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //Debug.Log(angle + 180);
        pointAnimator.SetFloat("Angle", angle + 180);
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
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
