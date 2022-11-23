using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private DoorAnimation leftDoor;
    [SerializeField] private DoorAnimation rightDoor;

    private IEnumerator WaitAnimationOverAndDoThings()
    {
        yield return new WaitForSeconds(0.15f);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            leftDoor.OpenDoor();
            rightDoor.OpenDoor();
            StartCoroutine(WaitAnimationOverAndDoThings());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            leftDoor.CloseDoor();
            rightDoor.CloseDoor();
            this.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
