using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletType1Mono : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0);
    public float speed = 2;
    public Vector2 velocity;
    private Rigidbody2D rb;
    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3f);
        pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity = direction * speed;
        pos = transform.position;
        pos+= velocity * Time.deltaTime;
        rb.MovePosition(pos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

}
