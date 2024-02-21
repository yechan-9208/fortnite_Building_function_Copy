using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{ 
    public static InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    //public delegate void OnSlotCountChange(int val);
    //public OnSlotCountChange onSlotCountChnage;

    public delegate void OnChnageItem();
    public OnChnageItem onChnageItem;


    public GameObject[] items;
    public List<Item> itemsList = new List<Item>();
 

    void Start()
    {
        items = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(Item _item)
    {
        if(itemsList.Count <4)
        {
            itemsList.Add(_item);
            //if(onChnageItem != null)
            //onChnageItem.Invoke();
            return true;
        }
        return false;
    }


    public void GetItem(GameObject item)
    {
        for(int i=0; i<5; i++)
        {
            if(items[i]==null)
            {
                items[i] = item;
                DefaultUIManager.instance.equment[i].transform.GetChild(1).gameObject.SetActive(false);
                return;
            }
        }

        FieldItem fieldItems = item.GetComponent<FieldItem>();
        if(AddItem(fieldItems.GetItem()))
        {
            fieldItems.DestroyItem();
        }
    }

}
