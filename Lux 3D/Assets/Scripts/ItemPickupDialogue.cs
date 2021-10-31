using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupDialogue : MonoBehaviour
{
    InventorySlot inventory;
    public ItemType.ItemTypes item; // MAKE SURE TO SET
    public DialogueTrigger pairedDialogue;
    private bool AddedItem = false;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pairedDialogue.DialogueCompleted() && !AddedItem)
        {
            inventory.slots.Add(item);
            FindObjectOfType<GameplayUI>().ItemGot();
            AddedItem = true;
        }
    }
}
