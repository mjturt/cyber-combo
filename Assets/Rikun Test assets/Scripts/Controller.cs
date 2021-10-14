using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private bool hasJumped = false;
    public float speed = 10.0f;
    public float jumpForce = 500f;
    public Animator jumpAnim;
    private BoxCollider2D bc;
    [SerializeField] private LayerMask groundLayer;
    private bool doubleJump = false;
    public bool rocketBoots = false;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpAnim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        //Crouch Animation
        if (Input.GetButton("Crouch"))
        {
            bc.size = new Vector2(2.1f,1.5f);
            jumpAnim.SetBool("isCrouched", true);
        }
        else
        {
            bc.size = new Vector2(2.1f,2.4f);
            jumpAnim.SetBool("isCrouched", false);
        }
            
        //Jump animations and triggers
        if (Input.GetButtonDown("Jump") && jumpAnim.GetBool("isWalkingRight")==true)
        {
            hasJumped = true;
            jumpAnim.Play("Base Layer.Jump_Right_Animation",0,0.5f);
        }
        else if (Input.GetButtonDown("Jump") && jumpAnim.GetBool("isWalkingLeft") == true)
        {
            hasJumped = true;
            jumpAnim.Play("Base Layer.Jump_Left_Animation",0,0.5f);
        }
        else if(Input.GetButtonDown("Jump"))
        {
            hasJumped = true;
            jumpAnim.Play("Base Layer.Jump_Idle_Animation",0,0.5f);
        }

        //Walking animations
        if (horizontal>0)
        {
            jumpAnim.SetBool("isWalkingRight",true);
        }
        else if(horizontal<0)
        {
            jumpAnim.SetBool("isWalkingLeft", true);
        }
        else
        {
            jumpAnim.SetBool("isWalkingRight",false);
            jumpAnim.SetBool("isWalkingLeft", false);
        }

        if (rocketBoots)
        {
            jumpAnim.SetBool("rocketBoots",true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rocketBoots = !rocketBoots;
            jumpAnim.SetBool("rocketBoots", rocketBoots);
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
