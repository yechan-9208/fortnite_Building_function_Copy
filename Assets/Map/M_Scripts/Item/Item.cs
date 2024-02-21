﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class Item 
{
    public ItemType itemType;
    public string itemName;
    public Material itemMaterial;
  
    public bool Use()
    {
        return false;
    }

}
