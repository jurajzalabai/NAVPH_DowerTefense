using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static bool tabPress;
    public GameObject camera;
    public GameObject player;
    public PlayerWeaponController pwc;
    public bool isShaking = false;
    private float magnitude;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            // set camera to tab mode
            tabPress = true;
            Camera.main.GetComponent<Camera>().orthographicSize = 8;
            transform.position = new Vector3(0, 0, -12);

        }
        else
        {
            // set camera to normal mode
            Camera.main.GetComponent<Camera>().orthographicSize = 3;
            tabPress = false;
        }
    }

    private void LateUpdate()
    {
        // move camera after player moved
        if (tabPress == false && player != null)
        {
                transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 8.0f);
        }
        else
        {
            tabPress = true;
            Camera.main.GetComponent<Camera>().orthographicSize = 11;
            transform.position = new Vector3(0, 0, -14);
        }
    }


    // this function was not used in the end (maybe in the future)
    IEnumerator Shake(float duration, float magnitude)
    {
        isShaking = true;
        Vector3 originalPosition = camera.transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;


            camera.transform.localPosition = new Vector3(player.transform.position.x + x, player.transform.position.y + y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;

        }
        isShaking = false;
        camera.transform.localPosition = originalPosition;
    }
}
