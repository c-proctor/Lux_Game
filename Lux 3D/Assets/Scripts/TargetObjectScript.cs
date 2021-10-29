using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectScript : MonoBehaviour
{
    Camera cam;
    bool addOnce;
    GameObject player;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        //Debug.Log(cam);
        addOnce = true;
        player = GameObject.FindWithTag("Player");
    }

    private void OnDestroy()
    {
        ThirdPersonPlayer.nearbyTargets.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = cam.WorldToViewportPoint(gameObject.transform.position);

        bool onScreen = targetPos.z > 0 && targetPos.x > 0 && targetPos.x < 1 && targetPos.y > 0 && targetPos.y < 1;

        //Determine if this can actually be added to potential targets
        Vector3 fromPos = transform.position;
        Vector3 toPos = player.transform.position;
        Vector3 direction = toPos - fromPos;


        Physics.Raycast(fromPos, direction, out hit);

        if (onScreen && addOnce && hit.transform.tag == "Player" && Vector3.Distance(toPos,fromPos) < 50.0)
        {
            addOnce = false;
            ThirdPersonPlayer.nearbyTargets.Add(this);
        }
        else if (!onScreen)
        {
            addOnce = true;
            ThirdPersonPlayer.nearbyTargets.Remove(this);
        }
    }
}
