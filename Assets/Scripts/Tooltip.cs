using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Tooltip canvas when statistics from shop can be shown
public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI headerField;
    public GameObject image;
    public TextMeshProUGUI[] text;
    public TextMeshProUGUI[] values;


    // Change tooltip position accordingly to mouse position
    void Update()
    {
        Vector2 position = Input.mousePosition;

        // set position to current mouse position + little offset so new position is in rigtht bottom corner of coursor
        transform.position = new Vector2(position.x + 10, position.y - 10);
    }
}
