using System.Collections;
using System.Linq;
using UnityEngine;
public class Enemy1MovementController : Enemy
{
    public float speed = 3;
    Transform playerTransform;
    // Start is called before the first frame update
    private Vector2 initialPosition;
    private Vector3 shotDirection;
    private EnemyGunController _enemyGunController;
    private Animator animator;

    void Start()
    {
        initialPosition = transform.position;
        _enemyGunController = GetComponentInChildren<EnemyGunController>();
        int willShoot = Random.Range(1, 10);
        int shootIn = Random.Range(2,4);
        animator = GetComponentInChildren<Animator>();
        if (willShoot>5)
        {

            InvokeRepeating("Fire", shootIn, 2);
        }
        
        playerTransform = GameObject.Find("Player").transform;
    }
    private void Fire()
    {
        if (transform.position.x<8.35f&&transform.position.x>-9f&&playerTransform!=null)
        {
            _enemyGunController.Shoot(shotDirection);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPosition = new Vector2(-1*speed*Time.deltaTime, 0);
        Vector2 position = transform.position;
        transform.Translate(newPosition);
       /* if (transform.position.x<=-18)
        {
            //Destroy(gameObject);
            transform.position = initialPosition;
        }*/
        if(playerTransform != null)
            shotDirection = (playerTransform.position-transform.position).normalized;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, Vector3.left+transform.position);
        Gizmos.DrawLine(transform.position,shotDirection+transform.position);
    }

    public override void Damaged(int amount)
    {
        playerTransform = null;
        speed = speed*0.5f;
        gameObject.GetComponent<Collider2D>().enabled = false;

        animator.SetBool("isDead", true);
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.7f);
        
        Destroy(gameObject);
    }
    

}
