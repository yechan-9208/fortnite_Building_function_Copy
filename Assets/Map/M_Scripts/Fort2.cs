using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fort2 : MonoBehaviour
{

    public static Fort2 instance;
    public Transform rayTransForm;

    private void Awake()
    {
        instance = this;
    }

    public GameObject FakeFloor;  // 배치할 장애물 프리팹
    public GameObject RealFloor;
    public GameObject FakeStand;
    public GameObject RealStand;
    public GameObject FakeStair;
    public GameObject RealStair;


    GameObject Stair;
    GameObject Stand;
    GameObject Floor;
    Vector3 changedHitPoint;

    public enum State
    {
        Idle,
        Floor,
        Stand,
        Stair
    }

    public State state;
    int layer;
    int layer2;
    bool isfrist;

    void Start()
    {
        state = State.Idle;
        layer = (1 << LayerMask.NameToLayer("FakeWall"));
        layer2 = (1 << LayerMask.NameToLayer("Player"));
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            DestroyAllWall();
            isfrist = false;
            state = State.Floor;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            DestroyAllWall();
            isfrist = false;
            state = State.Stand;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            DestroyAllWall();
            isfrist = false;
            state = State.Stair;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DestroyAllWall();
            state = State.Idle;
        }

        switch (state)
        {
            case State.Idle:
                UpdateIdle();
                break;

            case State.Floor:
                UpdateFloor();
                break;

            case State.Stand:
                UpdateStand();
                break;

            case State.Stair:
                UpdateStair();
                break;

            default:
                break;
        }

    }
    

    void DestroyAllWall()
    {
        Destroy(Stair);
        Destroy(Floor);
        Destroy(Stand);
    }

    private void UpdateStair()
    {
        Ray ray = new Ray(rayTransForm.position, rayTransForm.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 7f, ~(layer|layer2)))
        {
            Debug.DrawRay(ray.origin, ray.direction * 6, Color.red);
            changedHitPoint = HitPositionChange(hitInfo.point, 4f);
            float angle = Mathf.Round(transform.eulerAngles.y / 90) * 90;
            Vector3 euelerAngle = new Vector3(0, angle, 0);

            if (!isfrist) //FakeWall 생성
            {
                isfrist = true;
                Stair = Instantiate(FakeStair, changedHitPoint, FakeStair.transform.rotation);
            }
            Stair.transform.rotation = Quaternion.Euler(euelerAngle);
            Stair.transform.position = changedHitPoint;

        }

            if (Input.GetKeyDown(KeyCode.Mouse0))   // RealWall 생성
            {
                GameObject GORealStand = Instantiate(RealStair);
                GORealStand.transform.position = Stair.transform.position;
                GORealStand.transform.rotation = Stair.transform.rotation;
            }

        
    }

    private void UpdateStand()
    {
        Ray ray = new Ray(rayTransForm.position, rayTransForm.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 6f, ~(layer|layer2)))
        {
            Debug.DrawRay(ray.origin, ray.direction * 6, Color.red);
            changedHitPoint = HitPositionChange(hitInfo.point, 4f);
            float angle = Mathf.Round(transform.eulerAngles.y / 90) * 90;
            Vector3 euelerAngle = new Vector3(0, angle, 0);

            if (!isfrist) //FakeWall 생성
            {
                isfrist = true;
                Stand = Instantiate(FakeStand, changedHitPoint, FakeStand.transform.rotation);
            }
            Stand.transform.rotation = Quaternion.Euler(euelerAngle);
            Stand.transform.position = changedHitPoint;
        }

            if (Input.GetKeyDown(KeyCode.Mouse0))  //RealWall 생성
        { 
                GameObject GORealStand = Instantiate(RealStand);
                GORealStand.transform.position = Stand.transform.position;
                GORealStand.transform.rotation = Stand.transform.rotation;
            }

        
    }

   
  
    private void UpdateFloor()
    {
        Ray ray = new Ray(rayTransForm.position, rayTransForm.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 6f, ~(layer|layer2)))
        {
            Debug.DrawRay(ray.origin, ray.direction * 6, Color.red);
            changedHitPoint = HitPositionChange(hitInfo.point, 4f);

            if (!isfrist) //FakeWall 생성
            {
                isfrist = true;
                Floor = Instantiate(FakeFloor, changedHitPoint, FakeFloor.transform.rotation);
            }

            Floor.transform.position = changedHitPoint;
            

        }

 
        if (Input.GetKeyDown(KeyCode.Mouse0))   //RealWall 생성
        {
            GameObject GORealFloor = Instantiate(RealFloor);
            GORealFloor.transform.position = Floor.transform.position;
        }
    }

    private void UpdateIdle()
    {

    }



    Vector3 HitPositionChange(Vector3 position, float multiple)  // 마우스 포인터를 기준으로 그리드 설정
    {
        float adjustedX = Mathf.Round(position.x / multiple) * multiple;
        float adjustedY = Mathf.Round(position.y / multiple) * multiple;
        float adjustedZ = Mathf.Round(position.z / multiple) * multiple;
        return new Vector3(adjustedX, adjustedY + 0.1f, adjustedZ); //0.1f는 wall의 두께의 절반(그라운드 위에 Wall설치하기위해)
    }

}
