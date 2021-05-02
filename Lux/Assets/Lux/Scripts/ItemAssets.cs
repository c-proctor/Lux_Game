using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
   public static ItemAssets Instance{get; private set;}

   private void Awake()
   {
       Instance = this; 
   }
public Transform pfItemWorld;

   public Sprite TorchSprite;
public Sprite CharcoalSprite;
public Sprite StonesSprite;
public Sprite MetalNetSprite;
public Sprite RubyGemSprite;
public Sprite HatSprite;
public Sprite SticksSprite;
public Sprite ScarfSprite;
public Sprite FrozenCarrotBlockSprite;
public Sprite ThawedCarrotSprite;
public Sprite LanternSprite;
public Sprite IceFlowerSprite;
}






