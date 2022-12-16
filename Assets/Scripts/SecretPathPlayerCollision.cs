using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPathPlayerCollision : MonoBehaviour
{
    // static variable indicating whether player is currently in secret path or not
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
            // ignore collisions between all secret paths and player
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), secretPaths[i].GetComponent<Collider2D>());
        }
    }

    void Update()
    {
        if (player != null)
        {
            playerContact = false;

            for (int i = 0; i < secretPaths.Length; i++)
            {
                // check if player is currently in any secret path
                if (Vector2.Distance(player.transform.position, secretPaths[i].transform.position) <= 0.75f)
                {
                    playerContact = true;
                    break;
                }
            }

            playerIn = playerContact;
        }
    }
}