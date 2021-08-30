using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySave : MonoBehaviour
{
    
    static bool created =false;
   void Awake()
   {
       if(!created)
       {
           Debug.Log("created");
            DontDestroyOnLoad(this.gameObject);
            created = true;
           
           
       }
       else
       {
           Debug.Log("destroy");
           Destroy(this.gameObject);
       }
      
   }
}
