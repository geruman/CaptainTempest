using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuns : Enemy
{
    [SerializeField]
    GameObject _player;
    EnemyGunController _gunController;
    float MAX_DELAY = 1;
    float _shootDelay;
    int _life;
    private const int MAX_LIFE = 3;
    [SerializeField]
    bool _canShoot = true;
    private void OnEnable()
    {
        _life = MAX_LIFE;
    }
    public override void Damaged(int amount)
    {
        _life--;
        if(_life <= 0)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Die");
            // Destroy(gameObject, 0.7f); ;
            Invoke("SetDisabled", 0.7f);
        }
    }
    void SetDisabled()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        _gunController = GetComponent<EnemyGunController>();
        _shootDelay = MAX_DELAY;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        var direction = transform.position+Vector3.left - current;
        var angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        int randomFire = Random.Range(0, 50);
        if(randomFire> 25&&_shootDelay<=0&&_canShoot)
        {
            _shootDelay = MAX_DELAY;
            _gunController.Shoot(Vector2.left);
        }
        _shootDelay -=Time.deltaTime;
    }
}
