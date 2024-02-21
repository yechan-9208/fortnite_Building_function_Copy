using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int wallHP = 100;
    public int maxWallHP = 100;

    public int WallHP 
    {
        get { return wallHP; }
        set { wallHP = value; }
    }


    void onDamaged(int damage =10)
    {
        wallHP -= damage;
        if(wallHP<=0)
        {
            Destroy(transform.parent.gameObject);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.name.Contains("Bullet"))
        {
            onDamaged();
        }
    }



}

