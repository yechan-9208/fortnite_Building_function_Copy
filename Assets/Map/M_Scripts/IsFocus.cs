using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFocus : MonoBehaviour
{
    public RectTransform crosshairImage;
    bool isFocusItem;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
      

    }

    GameObject hitObject;
 

    public LayerMask itemLayer;
    void Update()
    {
        RayCheckItem();
    }

    float angle;
    private void RayCheckItem()
    {
        ray = Camera.main.ScreenPointToRay(crosshairImage.position);
        if (Physics.Raycast(ray, out hit, 2, itemLayer))
        {
            hitObject = hit.collider.gameObject;
            hitObject.transform.GetChild(0).gameObject.SetActive(true);
           if(Input.GetKeyDown(KeyCode.E))
            {
                InventoryManager.instance.GetItem(hitObject);
                hitObject.SetActive(false);
            }

        }
        else
        {
            if (hitObject != null)
            {
                hitObject.transform.GetChild(0).gameObject.SetActive(false);
            }

        }

    }
}
