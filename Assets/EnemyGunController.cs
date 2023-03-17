using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    
    public void Shoot()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, transform.rotation);
        Destroy(bullet,3f);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (transform.localRotation * Vector2.right).normalized;
        rb.AddForce(direction * 9, ForceMode2D.Impulse);
        
    }
}
