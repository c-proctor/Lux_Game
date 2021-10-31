using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject[] DeactivatedGameObjects;
    public GameObject[] ActivatedGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateBoss(bool status)
    {
        gameObject.SetActive(status);
    }

    public void BossDefeated()
    {
        for(int ii = 0; ii < DeactivatedGameObjects.Length; ++ii)
        {
            DeactivatedGameObjects[ii].SetActive(false);
        }
        for (int ii = 0; ii < ActivatedGameObjects.Length; ++ii)
        {
            ActivatedGameObjects[ii].SetActive(true);
        }
    }
}
