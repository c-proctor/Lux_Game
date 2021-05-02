using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType
    {
        Torch,
        Charcoal,
        Stones,
        MetalNet,
        RubyGem,
        Hat,
        Sticks,
        Scarf,
        FrozenCarrotBlock,
        ThawedCarrot,
        Lantern,
        IceFlower,
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.Torch:
            return ItemAssets.Instance. TorchSprite;

            case ItemType.Charcoal:
            return ItemAssets.Instance. CharcoalSprite;

            case ItemType.Stones:
            return ItemAssets.Instance. StonesSprite;

            case ItemType.MetalNet:
            return ItemAssets.Instance. MetalNetSprite;

            case ItemType.RubyGem:
            return ItemAssets.Instance. RubyGemSprite;

            case ItemType.Hat:
            return ItemAssets.Instance. HatSprite;

            case ItemType.Sticks:
            return ItemAssets.Instance. SticksSprite;

            case ItemType.Scarf:
            return ItemAssets.Instance. ScarfSprite;

            case ItemType.FrozenCarrotBlock:
            return ItemAssets.Instance. FrozenCarrotBlockSprite;

            case ItemType.ThawedCarrot:
            return ItemAssets.Instance. ThawedCarrotSprite;

            case ItemType.Lantern:
            return ItemAssets.Instance. LanternSprite;

            case ItemType.IceFlower:
            return ItemAssets.Instance. IceFlowerSprite;

        }
    }
}
