using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemType : MonoBehaviour
{

    // This list determines the type of items we have. It can be used for pickups as well as usable equipment (e.g. the fire lantern)
    public enum ItemTypes
    {
        Tophat,
        Sticks,
        Rocks,
        Carrot,
        Coal,
        Stone,
        Grill,
        FireLantern,
        IceLantern
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
