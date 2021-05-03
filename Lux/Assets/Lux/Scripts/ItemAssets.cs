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

public Sprite torchSprite;
public Sprite charcoalSprite;
public Sprite stonesSprite;
public Sprite metalNetSprite;
public Sprite rubyGemSprite;
//public Sprite hatSprite;
public Sprite sticksSprite;
public Sprite scarfSprite;
public Sprite frozenCarrotBlockSprite;
public Sprite thawedCarrotSprite;
public Sprite lanternSprite;
public Sprite iceFlowerSprite;
}






