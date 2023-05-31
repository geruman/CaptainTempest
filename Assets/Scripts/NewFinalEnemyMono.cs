using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class NewFinalEnemyMono : Enemy
{
    private const float MINIMUN_POSITION = 5.8f;
    private const float MAX_Y = 2.12f;
    private const float MIN_Y = -1.7f;
    private const float ROTATION_SPEED = 35;
    [SerializeField]
    GameObject _gunsHolder;
    [SerializeField]
    private bool _goingUp = false;

    Animator _animator;
    private bool _vulnerable = false;
    [SerializeField]
    private int _life = 16;
    private int _moveSpeed = 3;
    private FinalEnemyFases _currentFase = FinalEnemyFases.ZERO;
    [SerializeField]
    private GameObject[] guns;
    [SerializeField]
    private GameObject _finalGun;
    private int _currentLaserShot;
    [SerializeField]
    private EnemyLaser[] lasers;
    [SerializeField]
    private GameObject _bigExplotion;
    private int _lasersFired;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentLaserShot = lasers.Length-1;
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentFase)
        {
            case FinalEnemyFases.ZERO:
                ZeroFaseMotions();
                break;
            case FinalEnemyFases.FIRST:
                FirstFaseMotions();
                break;
            case FinalEnemyFases.SECOND:
                SecondFaseMotion();
                break;
            case FinalEnemyFases.THIRD:
                MoveUpAndDown();
                break;
        }
    }

    private void SecondFaseMotion()
    {
        MoveUpAndDown();
    }

    private void BackToInvulnerable()
    {

        _vulnerable = false;
        if (_life > 8)
        {
            _animator.SetTrigger("BackToInvulnerable");

        }
        else
        {
            _animator.SetTrigger("SecondFase");
        }
    }


    private void ZeroFaseMotions()
    {
        if (transform.position.x>MINIMUN_POSITION)
        {
            RotateGunsHolder();
            MoveLeft();
        }
        else
        {
            foreach (GameObject gun in guns)
            {
                gun.SetActive(true);
            }
            if (lasers[0].gameObject.transform.parent !=null)
            {
                InvokeRepeating("HandleLasers", 0, 7);
            }
            for (int i = 0; i<lasers.Length; i++)
            {
                lasers[i].gameObject.transform.SetParent(null);
            }
            _currentFase = FinalEnemyFases.FIRST;
        }
    }

    private void MoveLeft()
    {
        Vector2 newPosition = new Vector2(-1*_moveSpeed*Time.deltaTime, 0);
        transform.Translate(newPosition);
    }

    private void FirstFaseMotions()
    {
        RotateGunsHolder();
        MoveUpAndDown();
        bool noActiveGun = true;
        foreach (GameObject gun in guns)
        {
            if (gun.activeInHierarchy)
            {
                noActiveGun = false;
            }
        }
        if (noActiveGun)
        {
            _currentFase = FinalEnemyFases.SECOND;
            _animator.SetTrigger("BecomeVulnerable");
            _vulnerable = true;
            Invoke("BackToInvulnerable", 3f);
        }
    }

    private void RotateGunsHolder()
    {
        _gunsHolder.transform.Rotate(new Vector3(0, 0, ROTATION_SPEED*Time.deltaTime));
    }

    private void MoveUpAndDown()
    {
        float newY = transform.position.y;
        if (_goingUp)
        {
            if (transform.position.y<=MAX_Y-0.01)
            {
                newY = Mathf.Lerp(transform.position.y, MAX_Y, Time.deltaTime);

            }
            else
            {
                _goingUp = false;
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
                _goingUp = true;
            }
        }
        transform.position = new Vector2(transform.position.x, newY);
    }

    public override void Damaged(int amount)
    {
        if (_vulnerable)
        {
            _vulnerable = false;
            _animator.SetTrigger("Hurt");
            _life--;
            if (_life<=0)
            {
                _bigExplotion.transform.SetParent(null);
                _bigExplotion.SetActive(true);
                foreach(EnemyLaser laser in lasers)
                {
                    laser.GetComponent<Animator>().SetTrigger("Explode");
                }
                Destroy(gameObject);
            }
        }
    }
    public void BecomeVulnerable()
    {
        _vulnerable = true;
    }

    public void ReturnToZeroMotion()
    {
        _currentFase = FinalEnemyFases.ZERO;
    }
    public void SecondFaseStart()
    {
        _currentFase = FinalEnemyFases.SECOND;
        _finalGun.SetActive(true);
        _finalGun.GetComponent<FinalGun>().Reappear();
        StartCoroutine(ThirdStage());
    }
    private IEnumerator ThirdStage()
    {
        yield return new WaitForSeconds(7f);
        GetComponentInChildren<FinalGun>().Dissapear();
        _currentFase = FinalEnemyFases.THIRD;
        _animator.SetTrigger("BecomeVulnerable");
        _vulnerable = true;
        Invoke("BackToInvulnerable", 3f);

    }
    private void HandleLasers()
    {

        if (_lasersFired<4||true)
        {
            lasers[_currentLaserShot].ShootLaser();
            _currentLaserShot++;
            if (_currentLaserShot >= lasers.Length)
                _currentLaserShot = 0;
        }
        else
        {
            for (int i = 0; i<lasers.Length-1; i++)
            {
                _currentLaserShot++;
                if (_currentLaserShot>=lasers.Length)
                    _currentLaserShot = 0;
                lasers[_currentLaserShot].ShootLaser();

            }
        }
        _lasersFired += 1;

        if (_lasersFired==8)
        {
            _lasersFired = 0;
        }

    }

}
public enum FinalEnemyFases
{
    ZERO, FIRST, SECOND, THIRD
}