using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParalaxMono : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x<=-21.29)
        {
            transform.position = new Vector2(21.29f, 0);
        }
        Vector2 newPosition = new Vector2(transform.position.x+ (-3*Time.deltaTime), 0);
        transform.position = newPosition;
    }
    
}
