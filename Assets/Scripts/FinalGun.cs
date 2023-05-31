using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGun : MonoBehaviour
{

    [SerializeField]
    private GameObject _player;
    private Vector3 _shootDirection;
    private bool _shooting = true;
    [SerializeField]
    private bool _shotEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 1, 2);
    }
    private void Shoot()
    {
        if (_shooting&&_shotEnabled)
            StartCoroutine(BurstShot(8));
    }
    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("Enable");
    }
    private IEnumerator BurstShot(int maxShot)
    {
        for (int i = 0; i<maxShot; i++)
        {
            GetComponentInChildren<EnemyGunController>().Shoot(_shootDirection.normalized);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
            return;
        Vector3 current = transform.position;
        _shootDirection = _player.transform.position - current;
        var angle = Mathf.Atan2(_shootDirection.y, _shootDirection.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    public void Dissapear()
    {
        GetComponent<Animator>().SetTrigger("Disable");
        _shooting = false;
    }
    public void Reappear()
    {
        GetComponent<Animator>().SetTrigger("Enable");
        _shooting = true;
    }
}
