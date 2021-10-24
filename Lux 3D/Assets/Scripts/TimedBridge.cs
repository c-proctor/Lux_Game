using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedBridge : MonoBehaviour
{

    public float Timer = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this, Timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
