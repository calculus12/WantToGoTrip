using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public static ItemData instance;
    
    void Awake()
    {
        instance = this;
    }
    public List<Item> itemData = new List<Item>();
}
