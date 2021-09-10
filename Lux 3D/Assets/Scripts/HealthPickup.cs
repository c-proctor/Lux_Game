using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAdd = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ThirdPersonPlayer>() != null)
        {
            other.gameObject.GetComponent<ThirdPersonPlayer>().AddHealth(healthAdd);
            Destroy(gameObject);
        }
    }
}
