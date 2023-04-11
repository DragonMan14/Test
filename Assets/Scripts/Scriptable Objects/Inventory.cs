using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public Item currentItem;
    public List<Item> items;
    public int numberOfKeys;

    public void addItem(Item item)
    {
        if (item.isKey)
        {
            numberOfKeys++;
        }
        else if(!items.Contains(item))
        {
            items.Add(item);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        currentItem = null;
        items = new List<Item>();
        numberOfKeys = 1;
    }

    public bool HasKeys()
    {
        return numberOfKeys > 0;
    }
}
