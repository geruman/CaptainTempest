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
        
        if (transform.position.x<=-16.9f)
        {
            transform.position = new Vector2(30.9f, transform.position.y);
        }
        Vector2 newPosition = new Vector2(transform.position.x+ (-3*Time.deltaTime), transform.position.y);
        transform.position = newPosition;
    }
    
}
