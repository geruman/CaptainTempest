using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExplotionsMono : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyMe());
    }
    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
