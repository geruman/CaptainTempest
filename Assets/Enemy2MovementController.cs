using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy2MovementController : MonoBehaviour
{
    public float speed = 3f;
    public float startingY;
    public float amplitude = 1;
    public float frequency = 1;
    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        float x = -1*speed*Time.fixedDeltaTime;
        float y = Mathf.Sin(position.x * frequency) * amplitude;
        Vector2 movement = new Vector2(x, y);
        transform.position = new Vector2(movement.x+position.x,movement.y+startingY);
        //transform.Translate(new Vector2(x,y-transform.position.y));
        if (transform.position.x<-18)
        {
            Destroy(gameObject);
        }
    }
}
