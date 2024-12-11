using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 2f;
     void Start()
    {
        Destroy(gameObject, lifeTime);
    }

}
