using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI headerField;
    public GameObject image;
    public TextMeshProUGUI[] text;
    public TextMeshProUGUI[] values;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = Input.mousePosition;

        transform.position = new Vector2(position.x + 10, position.y - 10);
    }
}
