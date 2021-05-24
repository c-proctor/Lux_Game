using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScene : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("FireDoor"))
        {
            Debug.Log("FIREWORLD");
            SceneManager.LoadScene(3);
            
        }
        if(col.gameObject.CompareTag("IceDoor"))
        {
            Debug.Log("ICEWORLD");
            SceneManager.LoadScene(2);
        }
        if(col.gameObject.CompareTag("Exit"))
        {
            Debug.Log("EXIT");
            SceneManager.LoadScene(1);
        }
       
    }
    
}
