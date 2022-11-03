using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class FollowPlayer : MonoBehaviour
    {
        private GameObject player;
        private GameObject basee;
        AIDestinationSetter aIDestination;
        private bool followingPlayer = false;
        public LayerMask collisionLayer;

        // Start is called before the first frame update
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            basee = GameObject.FindGameObjectWithTag("Base");
            aIDestination = GetComponent<AIDestinationSetter>();
            aIDestination.target = basee.transform;
        }

        // Update is called once per frame
        void Update()
        {
            // To store linecast info
            RaycastHit2D hit;

            /* collisionlayer - what specific layer our linecast will detect (in other words it will ignore any other layers) */
            hit = Physics2D.Linecast(transform.position, player.transform.position, collisionLayer);

            if (Vector3.Distance(player.transform.position, transform.position) <= 3)
            {
                if (hit.collider != null)
                {
                    //Debug.Log("Hit: " + hit.collider.gameObject.name);

                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        //Debug.Log("Chasing player");
                        aIDestination.target = player.transform;
                        followingPlayer = true;
                    }
                    else
                    {
                        if (followingPlayer)     // And previously we did chase
                        {
                            //Debug.Log("Stop chase");
                            followingPlayer = false;
                            aIDestination.target = basee.transform;
                        }
                    }
                }
            }
      
        //Debug.DrawRay(player.transform.position, transform.position, Color.red, 2f);
        }
    }
}