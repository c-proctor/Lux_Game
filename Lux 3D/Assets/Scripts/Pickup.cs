using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    InventorySlot inventory;
    public GameObject itemButton;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySlot>();

    }
    void OnTriggerEnter(Collider col)
    {
        /*
        if(col.CompareTag("Player"))
        {
            Debug.Log("pickup");
            for(int i = 0; i<inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform,false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
