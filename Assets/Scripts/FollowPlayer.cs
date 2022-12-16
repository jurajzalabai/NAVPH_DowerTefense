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

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            basee = GameObject.FindGameObjectWithTag("Base");
            aIDestination = GetComponent<AIDestinationSetter>();
            aIDestination.target = basee.transform;
        }

        void Update()
        {
            if (player != null)
            {
                // To store linecast info
                RaycastHit2D hit;

                /* collisionlayer - what specific layer our linecast will detect (in other words it will ignore any other layers) */
                hit = Physics2D.Linecast(transform.position, player.transform.position, collisionLayer);

                if (Vector3.Distance(player.transform.position, transform.position) <= 3)
                {
                    if (hit.collider != null)
                    {

                        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                        {
                            aIDestination.target = player.transform;
                            followingPlayer = true;
                        }
                        else
                        {
                            if (followingPlayer)     // And previously we did chase
                            {
                                followingPlayer = false;
                                aIDestination.target = basee.transform;
                            }
                        }
                    }
                }
            }
        }
    }
}