using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int childrens = GetComponentsInChildren<Enemy>().Length;
        if ( childrens == 0)
        {
            Destroy(gameObject);
        }
    }
}
