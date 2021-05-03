using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item 
{
    public enum ItemType
    {
        Torch,
        Charcoal,
        Stones,
        MetalNet,
        RubyGem,
       /* Hat,
        Sticks,
        Scarf,
        FrozenCarrotBlock,
        ThawedCarrot,
        Lantern,
        IceFlower,*/
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite() {
        switch (itemType) {
        default:
        case ItemType.Torch:        return ItemAssets.Instance.torchSprite;    
        case ItemType.Charcoal:     return ItemAssets.Instance.charcoalSprite; 
        case ItemType.Stones:       return ItemAssets.Instance.stonesSprite;   
        case ItemType.MetalNet:     return ItemAssets.Instance.metalNetSprite; 
        case ItemType.RubyGem:      return ItemAssets.Instance.rubyGemSprite; 
        }
    }
}
