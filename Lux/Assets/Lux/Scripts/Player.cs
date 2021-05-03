using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] UI_Inventory uiInventory;

    void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        //ItemWorld.SpawnItemWorld(new Vector3(20,20), new Item{itemType = Item.ItemType.Charcoal,amount = 1});
        //ItemWorld.SpawnItemWorld(new Vector3(-20,-20), new Item{itemType = Item.ItemType.Torch,amount = 1});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
