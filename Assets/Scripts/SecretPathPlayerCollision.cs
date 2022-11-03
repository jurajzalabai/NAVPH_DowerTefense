using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPathPlayerCollision : MonoBehaviour
{
    public static bool playerIn;
    private static bool playerContact;
    private GameObject player;

    public Transform[] secretPaths;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        secretPaths = new Transform[transform.childCount];

        for (int i = 0; i < secretPaths.Length; i++)
        {
            secretPaths[i] = transform.GetChild(i);
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), secretPaths[i].GetComponent<Collider2D>());
        }
    }

    void Update()
    {
        playerContact = false;

        for (int i = 0; i < secretPaths.Length; i++)
        {
            if (Vector3.Distance(player.transform.position, secretPaths[i].transform.position) <= 0.75f)
            {
                playerContact = true;
                break;
            }
        }

        playerIn = playerContact;
        //Debug.Log("Player IN: " + playerIn);
    }
}