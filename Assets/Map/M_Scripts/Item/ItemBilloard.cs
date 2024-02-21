using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBilloard : MonoBehaviour
{
    Transform maincam;
    // Start is called before the first frame update
    void Start()
    {
        maincam = Camera.main.transform;
    }

    // Update is called once per frame
    Vector3 dir;
    void Update()
    {
        dir = transform.position - maincam.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = lookRotation;
        
    }
}
