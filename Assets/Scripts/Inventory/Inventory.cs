using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static Item;
using UnityEngine.Rendering.Universal;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item> ();

    public Item item;



    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            if (item.isStackable)
            {
                bool itemAlreadyInInventory = false;
                foreach (Item invetoryItem in items)
                {
                    if (invetoryItem.name == item.name)
                    {
                        invetoryItem.amount += 1;
                        itemAlreadyInInventory |= true;
                        Debug.Log(item.amount);
                    }
                }
                if (!itemAlreadyInInventory)
                {
                    items.Add(item);
                    item.amount = 1;
                }
            }
            else
            {
                items.Add(item);
                item.amount = 1;
            }

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            } 
        }
        return true;
    }
    public void Remove (Item item)
    {
        items.Remove (item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
