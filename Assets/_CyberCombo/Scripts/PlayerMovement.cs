using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private bool hasJumped;
    public float speed = 10.0f;
    public float jumpForce = 500f;
    private BoxCollider2D bc;
    [SerializeField] private LayerMask groundLayer;
    private bool doubleJump;
    public bool rocketBoots;
    
    public bool Fire = false;
    public bool Magnet = false;
    public bool Ice = false;
    
    private Restart _restart;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        _restart = GetComponent<Restart>();
    }
    
    void Update()
    {
        
        if (gameObject.transform.localPosition.y < -10)
        {
            _restart.Death();
        }
        
        horizontal = Input.GetAxis("Horizontal");

        /*Crouch Animation
        if (Input.GetButton("Crouch"))
        {
            bc.size = new Vector2(2.1f,1.5f);
            jumpAnim.SetBool("isCrouched", true);
        }
        else
        {
            bc.size = new Vector2(2.1f,2.4f);
            jumpAnim.SetBool("isCrouched", false);
        }*/
            
        //Jump animations and triggers
        if (Input.GetButtonDown("Jump"))
        {
            hasJumped = true;
        }

        //Double jump toggle
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rocketBoots = !rocketBoots;
        }
    }

    private void FixedUpdate()
    {
        //Left-right movement
        rb.velocity = new Vector2(horizontal * speed,rb.velocity.y);
        
        //Jump/doublejump physics
        if (hasJumped && isGrounded())
        { 
            doubleJump = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            hasJumped = false;
        }
        else if (hasJumped && doubleJump && rocketBoots)
        {
            doubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            hasJumped = false;
        }
        else if (hasJumped)
        {
            hasJumped = false;
        }
    }

    //Function to check if player is touching ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
