using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    InventorySlot inventory;
    public ItemType.ItemTypes item; // MAKE SURE TO SET
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inventory.slots.Add(item);
            FindObjectOfType<GameplayUI>().ItemGot();
            Destroy(gameObject);
        }
    }
}
