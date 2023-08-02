using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
   [SerializeField] float timeDestroy = 3f;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }
    
}
