using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public BulletType1Mono bullet;
    Vector2 direction;
    public bool isEnabled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = (transform.localRotation * Vector2.right).normalized;
    }
    public void Shoot()
    {
        if (isEnabled)
        {
            GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
            BulletType1Mono goBullet = go.GetComponent<BulletType1Mono>();
            goBullet.direction = direction;
        }
    }
    public void Shoot(Quaternion rotation)
    {
        if (isEnabled)
        {
            GameObject go = Instantiate(bullet.gameObject, transform.position, rotation);
            BulletType1Mono goBullet = go.GetComponent<BulletType1Mono>();
            goBullet.direction = direction;
        }
    }
}
