using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public Text ItemsCollected;
    public Text PlayerHealth;
    InventorySlot playerInv;
    ThirdPersonPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        playerInv = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySlot>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerHealth.text = "Health: " + player.GetHealth().ToString();
    }
    public void ItemGot()
    {
        ItemsCollected.text = "";
        foreach (ItemType.ItemTypes item in playerInv.slots)
        {
            ItemsCollected.text = ItemsCollected.text + '\n' + item.ToString();
        }
    }
}
