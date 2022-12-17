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

    void Awake()
    {
        // set aim point
        aimTransform = transform.Find("Aim");
    }

    void Update()
    {
        // if player is in tab mode, he can't aim or shoot
        if (CameraController.tabPress == false)
        {
            // if player is in secret path, he can aim but cannot shoot
            if (SecretPathPlayerCollision.playerIn == false)
            {
                HandleShooting();
            }
            HandleAiming();
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0) && aimAnimator.GetCurrentAnimatorStateInfo(0).IsName("weapon_idle") && PlayerController.builderMode == false)
        {
            // trigger animator when shoot function is called
            aimAnimator.SetTrigger("Shoot");
            
            // create bullet object
            GameObject _bullet = Instantiate(bullet, barell.transform.position, Quaternion.identity);
        }
    }

    //code from https://www.youtube.com/watch?v=fuGQFdhSPg4&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1&t=68s&ab_channel=CodeMonkey
    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        pointAnimator.SetFloat("Angle", angle + 180);
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    //code from https://www.youtube.com/watch?v=fuGQFdhSPg4&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1&t=68s&ab_channel=CodeMonkey
    public static Vector3 GetMouseWolrdPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    //code from https://www.youtube.com/watch?v=fuGQFdhSPg4&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1&t=68s&ab_channel=CodeMonkey
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWolrdPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    //code from https://www.youtube.com/watch?v=fuGQFdhSPg4&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1&t=68s&ab_channel=CodeMonkey
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWolrdPositionWithZ(Input.mousePosition, Camera.main);
    }

    //code from https://www.youtube.com/watch?v=fuGQFdhSPg4&list=RDCMUCFK6NCbuCIVzA6Yj1G_ZqCg&start_radio=1&t=68s&ab_channel=CodeMonkey
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWolrdPositionWithZ(Input.mousePosition, worldCamera);
    }

   
}
