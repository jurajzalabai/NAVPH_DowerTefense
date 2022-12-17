using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// indicator in tutorial to show player what to do
public class IndicatorController : MonoBehaviour
{

    public Transform target;

    private void Update()
    {
        var dir = target.position - transform.position;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
