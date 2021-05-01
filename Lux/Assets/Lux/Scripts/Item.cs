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
}
