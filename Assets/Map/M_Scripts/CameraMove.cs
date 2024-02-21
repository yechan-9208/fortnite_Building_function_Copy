using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotSpeed = 200f;

    float rx, ry;



    void Start()
    {
        rotSpeed = 1000f;

    }

    float mx, my;

    // Update is called once per frame
    void Update()
    {
        mx = Input.GetAxis("Mouse X");
        my = Input.GetAxis("Mouse Y");

        ry += mx * rotSpeed * Time.deltaTime;
        rx += my * rotSpeed * Time.deltaTime;

        rx = Mathf.Clamp(rx, -75, 75);

        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
