using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    public Item item;
    public MeshRenderer meshRender;


    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemMaterial = _item.itemMaterial;
        item.itemType = _item.itemType;

        meshRender.material = item.itemMaterial;

    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
