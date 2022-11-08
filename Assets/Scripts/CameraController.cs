using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static bool tabPress;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            tabPress = true;
            Camera.main.GetComponent<Camera>().orthographicSize = 8;
            transform.position = new Vector3(0, 0, -12);

        }
        else
        {
            Camera.main.GetComponent<Camera>().orthographicSize = 3;
            tabPress = false;
        }
    }

    private void LateUpdate()
    {
        if (tabPress == false && player != null)
        {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 8.0f);
        }
        else
        {
            tabPress = true;
            Camera.main.GetComponent<Camera>().orthographicSize = 8;
            transform.position = new Vector3(0, 0, -12);
        }
    }
}
