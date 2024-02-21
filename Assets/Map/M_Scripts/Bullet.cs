using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    void Start()
    {
        
    }
    Vector3 dir;
    // Update is called once per frame
    void Update()
    {
        dir = Vector3.forward;
        transform.position += dir * 10*Time.deltaTime;
    }
}
