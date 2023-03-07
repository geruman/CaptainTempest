using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnemyController : Enemy
{
    public float moveSpeed = 3f;
    private float DEFAULT_DELAY_FOR_SHOT = 1f;
    public GameObject enemyBulletPrefab;
    private float _delayShot;
    GunController gunController;
    // Start is called before the first frame update
    void Start()
    {
        gunController = GetComponentInChildren<GunController>();
        _delayShot = DEFAULT_DELAY_FOR_SHOT;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.x<=6)
        {
            Vector3 rotationToAdd = new Vector3(0, 0, 0.2f);
            transform.Rotate(rotationToAdd);
            _delayShot -= Time.deltaTime;
        }
        else
        {
            Vector2 newPosition = new Vector2(-1*moveSpeed*Time.deltaTime, 0);
            transform.Translate(newPosition);
        }
        if (_delayShot<=0)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * 9, ForceMode2D.Impulse);
            _delayShot = DEFAULT_DELAY_FOR_SHOT;
        }
    }
}
