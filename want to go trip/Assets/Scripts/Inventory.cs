using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<Item> items = new List<Item>();

    public bool AddItem(Item _item)
    {
        items.Add(_item);
        return true;
    }
}
