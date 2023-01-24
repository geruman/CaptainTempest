using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1MovementController : MonoBehaviour
{
    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 newPosition = new Vector2(-1*speed*Time.fixedDeltaTime,0);
        Vector2 position = transform.position;
        transform.position = position+newPosition;
        if (transform.position.x<=-18)
        {
            Destroy(gameObject);
        }
    }
}
