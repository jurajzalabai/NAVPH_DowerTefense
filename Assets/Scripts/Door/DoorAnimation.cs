using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    public Animator animator;

    void Awake()
    {
        // set animator for door animations when door object is created
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        // open door through animation
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        // close door through animation
        animator.SetBool("Open", false);
    }
}