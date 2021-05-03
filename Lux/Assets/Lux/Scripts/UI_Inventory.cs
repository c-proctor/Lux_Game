using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Inventory : MonoBehaviour
{
    Inventory inventory;
    Transform itemSlotContainer;
    Transform itemSlotTemplate;

    // Start is called before the first frame update
    void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInventory(Inventory inventory)
    {
        this. inventory = inventory;
        RefreshInventroyItems();


    }
    void RefreshInventroyItems()
    {
    int x = 0;
    int y = 0;
    float itemSlotCellSize = 100f;
    
        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2 (x* itemSlotCellSize, y*itemSlotCellSize);
            Image image =  itemSlotRectTransform.Find("image").GetComponent<Image>();
            //image.sprite = item.GetSprite();
            x++;
            if(x > 4)
            {
                x = 0;
                y--;
            }
        }
    }
}
