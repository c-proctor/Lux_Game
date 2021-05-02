using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        AddItem(new Item { itemType = Item.ItemType.Torch, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Charcoal, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Stones, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.MetalNet, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.RubyGem, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Hat, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Sticks, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Scarf, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.FrozenCarrotBlock, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.ThawedCarrot, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.Lantern, amount = 1 });
        AddItem(new Item { itemType = Item.ItemType.IceFlower, amount = 1 });
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
    public List<Item>GetItemList()
    {
        return itemList;
    }
}
