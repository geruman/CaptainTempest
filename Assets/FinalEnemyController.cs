using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnemyController : Enemy
{
    public float moveSpeed = 3f;
    private float DEFAULT_DELAY_FOR_SHOT = 0.1f;
    public GameObject enemyBulletPrefab;
    private float _delayShot;
    private EnemyGunController _enemyGunController;
    float _rotationFireDegrees = 0;
    float _backwards = 1;
    GunController gunController;
    private float _elapsedTime = 0;
    private float _secondPhase = 0;
    private bool firstShot;

    override public bool isActive()
    {
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        gunController = GetComponentInChildren<GunController>();
        _delayShot = DEFAULT_DELAY_FOR_SHOT;
        _enemyGunController = GetComponentInChildren<EnemyGunController>();
    }

    // Update is called once per frame
    void Update()
    {
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

    private void SecondPhaseShots()
    {
        float fireDegrees = 360/10;
        if (firstShot)
        {
            for (float i = 0; i <360; i = i + fireDegrees)
            {
                _enemyGunController.transform.localEulerAngles = (new Vector3(0, 0, i));
                _enemyGunController.Shoot();
                _delayShot = 0.5f;
            }
            firstShot = false;
        }
        else
        {
            for (float i = 0; i <360; i = i + fireDegrees)
            {
                _enemyGunController.transform.localEulerAngles = (new Vector3(0, 0, i+18));
                _enemyGunController.Shoot();
                _delayShot = 0.5f;
            }
            firstShot = true;
        }
    }

    private void FirstPhaseShots()
    {
        float fireDegrees = 360/10;
        for (float i = 0; i <360; i = i + fireDegrees)
        {
            _enemyGunController.transform.localEulerAngles = (new Vector3(0, 0, i));
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
}
