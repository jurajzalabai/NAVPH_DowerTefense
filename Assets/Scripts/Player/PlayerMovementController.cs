using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rigidBody;
    public Animator feetAnimator;


    private Vector2 movement;

    void Update()
    {
        if (CameraController.tabPress == false)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

            // set animator according to movement direction
            feetAnimator.SetFloat("Horizontal", movement.x);
            feetAnimator.SetFloat("Vertical", movement.y);
            feetAnimator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            // if player is in tab mode he can't move
            movement.x = 0;
            movement.y = 0;

            feetAnimator.SetFloat("Horizontal", movement.x);
            feetAnimator.SetFloat("Vertical", movement.y);
            feetAnimator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        if (CameraController.tabPress == false)
        {
            // move player by distance travelled from last fixed update
            rigidBody.MovePosition(rigidBody.position + movement * movementSpeed * Time.fixedDeltaTime);
        }
    }


}
