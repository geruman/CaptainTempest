using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{

    [SerializeField]
    private GameObject laserShot;
    private GameObject laser;
    private int yAmount = 5;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ShootLaser()
    {
        StartCoroutine(Shoot());
    }
    void Update()
    {
        UpdateLaserPosition();
    }
    private IEnumerator Shoot()
    {
        animator.SetBool("isCharging", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("isCharging", false);
        laser = Instantiate(laserShot, transform);
        UpdateLaserPosition();
        yield return new WaitForSeconds(4);
        if (laser!=null)
        {
            Destroy(laser);
        }
    }
    private void UpdateLaserPosition()
    {
        if (laser!=null)
            laser.transform.position = new Vector2(transform.position.x, transform.position.y + yAmount);
    }
}
