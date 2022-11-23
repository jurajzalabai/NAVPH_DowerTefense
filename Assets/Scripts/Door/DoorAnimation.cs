using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }
}