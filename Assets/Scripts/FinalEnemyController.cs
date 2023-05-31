using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinalEnemyController : Enemy
{
    [SerializeField]
    private int _life = 50;
    [SerializeField]
    private float ROTATION_SPEED = 35;
    public float moveSpeed = 3f;
    private float DEFAULT_DELAY_FOR_SHOT = 0.1f;
    public GameObject enemyBulletPrefab;
    private float _delayShot;
    private float _delayLazer;
    [SerializeField]
    private EnemyGunController _enemyGunController;
    float _rotationFireDegrees = 0;
    float _backwards = 1;

    private float _elapsedTime = 0;
    private float _secondPhase = 0;
    private bool firstShot;
    private int _currentLaserShot;
    [SerializeField]
    private EnemyLaser[] lasers;
    [SerializeField]
    private PlayerMono player;
    [SerializeField]
    private GameObject guns;
    private int _shotsFired;
    private BossGuns[] _gunsList;
    private readonly float MAX_Y = 2.12f;
    private readonly float MIN_Y = -1.7f;
    private bool goingUp = true;
    private Animator _animator;
    private bool _vulnerable;
    [SerializeField]
    private GameObject finalGun;

    public void EnableFinalGun()
    {
        finalGun.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        _vulnerable = false;
        _animator = GetComponent<Animator>();
        _delayShot = DEFAULT_DELAY_FOR_SHOT;
        _delayLazer = 1f;
        _currentLaserShot = lasers.Length-1;
        _shotsFired = 0;
        _gunsList = guns.GetComponentsInChildren<BossGuns>();
        for (int i = 0; i<lasers.Length; i++)
        {
            lasers[i].gameObject.transform.SetParent(null);
        }
    }
    private void FixedUpdate()
    {
        
    }

    private void RotateFirstStageGuns(float delta)
    {
        guns.transform.Rotate(new Vector3(0, 0, ROTATION_SPEED*delta));
    }

    // Update is called once per frame
    void Update()
    {
        _delayLazer -= Time.deltaTime;
        RotateFirstStageGuns(Time.deltaTime);
        HandleLazers();
        MoveUpAndDown();
        WillBecomeVulnerable();
        IfBackToInvulnerable();
        return;
        _rotationFireDegrees = _rotationFireDegrees+0.7f;

        if (_rotationFireDegrees>360)
        {
            _rotationFireDegrees = 0;
        }
        if (transform.position.x<=6)
        {
            /*Vector3 rotationToAdd = new Vector3(0, 0, 0.2f);
            transform.Rotate(rotationToAdd);*/
            _delayShot -= Time.deltaTime;
            _delayLazer -= Time.deltaTime;
            _elapsedTime +=Time.deltaTime;
            _secondPhase +=Time.deltaTime;
        }
        else
        {
            Vector2 newPosition = new Vector2(-1*moveSpeed*Time.deltaTime, 0);
            transform.Translate(newPosition);
        }
        if (_delayShot<=0)
        {
            if (_elapsedTime>5f)
            {
                _backwards *= -1;
                _elapsedTime = 0;
            }
            if (_secondPhase<20f)
            {
                FirstPhaseShots();

            }
            else if (_secondPhase<30)
            {
                SecondPhaseShots();
            }
            else
            {
                ThirdPhaseShots();
            }
        }



    }

    private void IfBackToInvulnerable()
    {
        if (_life==25)
        {
            _life--;
            _animator.SetTrigger("BackToInvulnerable");
        }
    }

    private void WillBecomeVulnerable()
    {
        if (guns.GetComponentsInChildren<BossGuns>().Length == 0&&!_vulnerable && _life == 50)
        {
            _vulnerable = true;
            _animator.SetTrigger("BecomeVulnerable");
        }
    }

    private void MoveUpAndDown()
    {
        float newY = transform.position.y;
        if (goingUp)
        {
            if (transform.position.y<=MAX_Y-0.01)
            {
                newY = Mathf.Lerp(transform.position.y, MAX_Y, Time.deltaTime);

            }
            else
            {
                goingUp = false;
            }
        }
        else
        {
            if (transform.position.y>=MIN_Y+0.01)
            {
                newY = Mathf.Lerp(transform.position.y, MIN_Y, Time.deltaTime);
            }
            else
            {
                goingUp = true;
            }
        }
        transform.position = new Vector2(transform.position.x, newY);
    }

    void HandleLazers()
    {
        if (_delayLazer<=0)
        {
            _delayLazer  = 7f;
            if (_shotsFired<4)
            {
                lasers[_currentLaserShot].ShootLaser();

            }
            else
            {
                StartCoroutine(SimpleShot(_currentLaserShot));


            }
            _shotsFired += 1;
            _currentLaserShot++;
            if (_currentLaserShot >= lasers.Length)
            {
                _currentLaserShot = 0;

            }
            if (_shotsFired==8)
            {
                _shotsFired = 0;
            }
        }
    }

    private IEnumerator SimpleShot(int _currentLaserShot)
    {
        for (int i = 0; i<lasers.Length-1; i++)
        {
            _currentLaserShot++;
            if (_currentLaserShot>=lasers.Length)
                _currentLaserShot = 0;
            lasers[_currentLaserShot].ShootLaser();

        }

        yield return null;
    }
    private void SecondPhaseShots()
    {
        float fireDegrees = 360/10;
        Vector2 direction = _enemyGunController.transform.position - player.transform.position;
        float magnitudY = direction.y>0 ? direction.y : -direction.y;
        float magnitudX = direction.x>0 ? direction.x : -direction.x;
        float degreesForRedirect = Mathf.Rad2Deg * Mathf.Atan(magnitudY/magnitudX);
        float sumarGrados = 0;
        if (direction.x>0)
        {
            if (direction.y>0)
            {

            }
            else
            {
                sumarGrados = 270;
            }
        }
        else
        {
            if (direction.y>0)
            {
                sumarGrados = 90;
                degreesForRedirect = 90 - degreesForRedirect;
            }
            else
            {
                sumarGrados = 180;
            }
        }
        if (firstShot)
        {
            for (float i = 0; i <360; i = i + fireDegrees)
            {
                _enemyGunController.transform.localEulerAngles = (new Vector3(0, 0, i+sumarGrados+degreesForRedirect));
                _enemyGunController.Shoot();
                _delayShot = 0.5f;
            }
            firstShot = false;
        }
        else
        {
            for (float i = 0; i <360; i = i + fireDegrees)
            {
                _enemyGunController.transform.localEulerAngles = (new Vector3(0, 0, i+18+sumarGrados+degreesForRedirect));
                _enemyGunController.Shoot();
                _delayShot = 0.5f;
            }
            firstShot = true;
        }
    }

    private void FirstPhaseShots()
    {
        float fireDegrees = 360/10;
        Vector2 direction = _enemyGunController.transform.position - player.transform.position;
        float magnitudY = direction.y>0 ? direction.y : -direction.y;
        float magnitudX = direction.x>0 ? direction.x : -direction.x;
        float degreesForRedirect = Mathf.Rad2Deg * Mathf.Atan(magnitudY/magnitudX);
        float sumarGrados = 0;
        if (direction.x>0)
        {
            if (direction.y>0)
            {

            }
            else
            {
                sumarGrados = 270;
            }
        }
        else
        {
            if (direction.y>0)
            {
                sumarGrados = 90;
                degreesForRedirect = 90 - degreesForRedirect;
            }
            else
            {
                sumarGrados = 180;
            }
        }
        for (float i = 0; i <360; i = i + fireDegrees)
        {
            _enemyGunController.transform.localEulerAngles = (new Vector3(0, 0, i+sumarGrados+degreesForRedirect));
            _enemyGunController.Shoot();
            _delayShot = 1f;
        }
    }

    private void ThirdPhaseShots()
    {
        float fireDegrees = 360/5;
        for (float i = 0; i <360; i = i + fireDegrees)
        {
            _enemyGunController.transform.localEulerAngles = (new Vector3(0, 0, i+(_rotationFireDegrees*_backwards)));
            _enemyGunController.Shoot();
            _delayShot = DEFAULT_DELAY_FOR_SHOT;
        }
    }

    public override void Damaged(int amount)
    {
        if (_vulnerable)
        {
            _vulnerable = false;
            _animator.SetTrigger("Hurt");
            _life--;
            Invoke("HurtNoMore",0.7f);
        }
    }
    public void HurtNoMore()
    {
        _vulnerable = true;
        _animator.SetTrigger("HurtNoMore");
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (_gunsList!=null)
        {
            foreach(BossGuns gun in _gunsList)
            {
                Gizmos.DrawLine(gun.transform.position, Vector3.left+gun.transform.position);
                //Gizmos.DrawLine(gun.transform.position, player.transform.position - (gun.transform.position+guns.transform.position));

            }
        }
        if(player!=null)
        Gizmos.DrawLine(transform.position,player.transform.position);
            //Gizmos.DrawLine(transform.position, shotDirection+transform.position);   
    }
}
