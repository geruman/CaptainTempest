using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    
    virtual public bool isActive()
    {
        return (transform.position.x < 9.3f);
    }
    public abstract void Damaged(int amount);
}
