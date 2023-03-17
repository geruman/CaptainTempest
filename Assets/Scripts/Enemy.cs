using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    virtual public bool isActive()
    {
        return (transform.position.x < 9.3f);
    }
}
