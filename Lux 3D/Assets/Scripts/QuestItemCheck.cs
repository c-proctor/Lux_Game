using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemCheck : MonoBehaviour
{
    public ItemType.ItemTypes[] QuestItems;
    bool AllFound = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            List<ItemType.ItemTypes> PlayerItemsList = other.gameObject.GetComponent<InventorySlot>().slots;
            for(int ii = 0; ii < QuestItems.Length; ii++)
            {
                if(PlayerItemsList.Contains(QuestItems[ii]))
                {
                    AllFound = true;
                }
                else
                {
                    AllFound = false;
                }
            }
        }
    }

    public bool GetQuestCompletionState()
    {
        return (AllFound);
    }
}
