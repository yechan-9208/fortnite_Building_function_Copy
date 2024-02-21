//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Fort : MonoBehaviour
//{
    
//    public static Fort instance;

//    private void Awake()
//    {
//        instance = this;
//    }

//    public GameObject previewPrefab;  // 배치할 장애물 프리팹
//    public GameObject realPrefab;
//    public GameObject StandpreviewPrefab;
//    public GameObject StandrealviewPrefab;
//    public GameObject StairrealviewPrefab;

//    public enum State
//    {
//        Idle,
//        Floor,
//        Stand,
//        Stair
//    }

//    public State state;
 
//    void Start()
//    {
//        state = State.Idle;
//    }


   

//    // Update is called once per frame
//    void Update()
//    {

//        if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            state = State.Floor;
//        }

//        if (Input.GetKeyDown(KeyCode.Alpha2))
//        {
//            state = State.Stand;
//        }

//        if (Input.GetKeyDown(KeyCode.Alpha3))
//        {
//            state = State.Stair;
//        }



//        if (Input.GetKeyDown(KeyCode.Alpha0))
//        {
//            WallManager.instance.Destroy();
//            state = State.Idle;
//        }
        
//        switch (state)
//        {
//            case State.Idle:
//                //WallManager.instance.Destroy();
//                UpdateIdle();
//                break;
//            case State.Floor:
//                //WallManager.instance.Destroy();
//                UpdateFloor();
//                break;
//            case State.Stand:
//                //WallManager.instance.Destroy();
//                UpdateStand();
//                break;
//            case State.Stair:
//                //WallManager.instance.Destroy();
//                UpdateStair();
//                break;

//            default:
//                break;
//        }

//    }

//    private void UpdateStair()
//    {
//        Ray ray1 = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//        RaycastHit hitInfo;
//        int layer = (1 << LayerMask.NameToLayer("CreateWall"));

//        if (Physics.Raycast(ray1, out hitInfo, 6f, layer))
//        {
//            Vector3 dir= new Vector3();
//            Quaternion rot = new Quaternion();

//            Transform parent = hitInfo.transform.parent;
//            float angle1 = Vector3.Angle(transform.forward, hitInfo.transform.forward);
//            float angle2 = Vector3.Angle(transform.forward, hitInfo.transform.right);

//            dir = parent.Find("FStairColider").transform.position;

//            if (0 <= angle1 && angle1 < 45)
//            {

//                rot = parent.Find("FStairColider").transform.rotation;
       
  
//            }
//            else if ((45 <= angle1 && angle1 < 135) && (0 <= angle2 && angle2 < 45))
//            {
  
//                rot = parent.Find("RStairColider").transform.rotation;


//            }
//            else if ((45 <= angle1 && angle1 < 135) && (135 <= angle2 && angle2 < 180))
//            {

//                rot = parent.Find("LStairColider").transform.rotation;
         
//            }
//            else if (135 <= angle1 && angle1 < 180)
//            {
    
//                rot = parent.Find("BStairColider").transform.rotation;
    
//            }

//            //if (WallManager.instance.CheckCanBuild(rot))
//            //{
//            //    WallManager.instance.Destroy();
//            //    GameObject StandWall = Instantiate(StandpreviewPrefab);
//            //    StandWall.transform.position = hitInfo.collider.transform.position;
//            //    StandWall.transform.rotation = hitInfo.collider.transform.rotation;

//            //    WallManager.instance.BuildWall(StandWall);
//            //}



//            if (Input.GetKeyDown(KeyCode.Q))  // 2번을 눌렀을때 실제 게임오브젝트 배치
//            {
//                GameObject StairWall = Instantiate(StairrealviewPrefab);
//                StairWall.transform.position =dir;
//                StairWall.transform.rotation = rot;
//                //WallManager.instance.BuildWall(StairWall);
//            }


//        }
//    }
//    private void UpdateStand()
//    {
//        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//        RaycastHit hitInfo;
//        int layer = (1 << LayerMask.NameToLayer("StandWall"));
//        if (Physics.Raycast(ray, out hitInfo, 6f, layer))
//        {
//            Vector3 adjustedPosition = hitInfo.collider.transform.position;

//            if (WallManager.instance.CheckCanBuild(adjustedPosition))
//            {
//                WallManager.instance.Destroy();
//                GameObject StandWall = Instantiate(StandpreviewPrefab);
//                StandWall.transform.position = hitInfo.collider.transform.position;
//                StandWall.transform.rotation = hitInfo.collider.transform.rotation;
                
//                WallManager.instance.BuildWall(StandWall);
//            }

//            if (Input.GetKeyDown(KeyCode.Q))  // 2번을 눌렀을때 실제 게임오브젝트 배치
//            {
//                GameObject StandWall = Instantiate(StandrealviewPrefab);
//                StandWall.transform.position = hitInfo.collider.transform.position;
//                StandWall.transform.rotation = hitInfo.collider.transform.rotation;
                
//                WallManager.instance.BuildWall(StandWall);
//            }

//        }
//    }

//    private void UpdateFloor()
//    {
//        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
//        RaycastHit hitInfo;
//        int layer = (1 << LayerMask.NameToLayer("CreateWall"));
//        int layer2 = (1 << LayerMask.NameToLayer("StandWall"));
//        int layer3 = (1 << LayerMask.NameToLayer("StairWall"));
//        if (Physics.Raycast(ray, out hitInfo, 6f, ~(layer2|layer|layer3)))
//        {
//            Vector3 adjustedPosition = AdjustToNearestMultiple(hitInfo.point, 4f);

//            //print(adjustedPosition);

//            if (WallManager.instance.CheckCanBuild(adjustedPosition))  //마우스 포인터 위치에 생성된 게임오브젝트있는지 확인해서 중복 생성 제한
//            {
//                WallManager.instance.Destroy();
//                GameObject GO = Instantiate(previewPrefab, adjustedPosition, Quaternion.identity);
//                WallManager.instance.BuildWall(GO);
             
//            }



//            if (Input.GetKeyDown(KeyCode.Q))  // 2번을 눌렀을때 실제 게임오브젝트 배치
//            {
//                GameObject GO = Instantiate(realPrefab, adjustedPosition, Quaternion.identity);
//                WallManager.instance.BuildWall(GO);
//            }
//        }
//    }

//    private void UpdateIdle()
//    {
      
//    }

   

//    Vector3 AdjustToNearestMultiple(Vector3 position, float multiple)  // 마우스 포인터를 기준으로 그리드 설정
//    {
//        float adjustedX = Mathf.Round(position.x / multiple) * multiple;
//        float adjustedY = Mathf.Round(position.y / multiple) * multiple ;
//        float adjustedZ = Mathf.Round(position.z / multiple) * multiple ;

//        //return new Vector3(adjustedX, 0.5f, adjustedZ);
//        //print(previewPrefab.transform.localScale.y);
//        return new Vector3(adjustedX, adjustedY + 0.1f, adjustedZ);
//    }
//}
