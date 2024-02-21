using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    float h, v;
    public float jumpPower = 10f;
    public float gravity = 9.81f;
    int jumpCount = 0;
    public int maxJumpCount = 2;
    float yVelocity;
    CharacterController cc;


    Vector3 dir;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        jumpPower = 5f;
        cam = Camera.main;
        cc = GetComponent<CharacterController>();
        cc.minMoveDistance = 0;
    }


    // Update is called once per hrame
    void Update()
    {
        yVelocity -= gravity*Time.deltaTime;


        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = jumpPower;
            jumpCount++;
        }

        if (cc.isGrounded)
        {
            jumpCount = 0;
            yVelocity = 0;
        }


        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        //dir = Vector3.forward * v + Vector3.right * h;
        dir = new Vector3(h, 0, v);
        dir = cam.transform.TransformDirection(dir);  // 카메라 방향에따라 앞뒤체크
        dir.y = 0;
        dir.Normalize();

        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;

        Vector3 camdir = cam.transform.forward;
        camdir.y = 0;
        transform.forward = camdir;

        //transform.position += velocity * Time.deltaTime;


        cc.Move(velocity * Time.deltaTime);

    }
}
