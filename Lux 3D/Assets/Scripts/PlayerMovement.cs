using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    Vector2 input;
    bool isNormalMovement = true; //false would be skating
    bool justCollidedWithIceBlock = false; //only allow skate input after a block collision
   

    // Update is called once per frame
    void Update()
    {
        //Input
        

        /*
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        */
        if (isNormalMovement)
        {
            NormalMovement();
        }
        else
        {
            SkatingMovement();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void NormalMovement()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
        movement = input;
    }

    void SkatingMovement()
    {
        if (justCollidedWithIceBlock)
        {
            //Debug.Log("Collecting skating movement");
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            
            //only clear the justcollided flag if the player pressed something
            if (input.magnitude > 0.01f)
            {
                movement = input;
                justCollidedWithIceBlock = false;
            }
        }
        
        animator.SetFloat("Horizontal", input.x);
        animator.SetFloat("Vertical", input.y);
        animator.SetFloat("Speed", input.sqrMagnitude);
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "IceFloor")
        {
            isNormalMovement = false;
            justCollidedWithIceBlock = false;
            //movement = Vector2.zero;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "IceFloor")
        {
            isNormalMovement = true;
            //movement = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Collider2D col = collision.collider;
        Debug.Log("Collided with " + col.tag);
        if (col.tag == "IceBlock")
        {
            justCollidedWithIceBlock = true;
            //movement = Vector2.zero;
        }
    }

}
