using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public float height = 2;
    public float width = 2;
    public float gridHeightStart = -2;
    public float gridWidthStart = -2;

    public GameObject obj;

    public float gridSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        /*gridHeightStart -= 0.5f;
        gridWidthStart -= 0.5f;
        for (float x = gridWidthStart + 1.0f; x <= width; x += gridSize)
        {
            for (float y = gridHeightStart + 1.0f; y <= height; y += gridSize)
            {
                GameObject obje = Instantiate(obj, new Vector3(x, y, 0), Quaternion.identity);
                Debug.Log(x + " " + y);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
