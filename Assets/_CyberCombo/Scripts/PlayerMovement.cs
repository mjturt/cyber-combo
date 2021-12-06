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
    public bool iceBoots;
    public bool fireBullet;
    public bool iceBullet;

    public bool Fire = false;
    public bool Magnet = false;
    public bool Ice = false;
    public bool Gun = false;

    private bool hasFired = false;
    public Rigidbody2D bullet;
    public Sprite iceSprite;
    private Vector3 shootTargetPos;
    public float bulletSpeed;

    private Restart _restart;

    private bool icy;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        _restart = GetComponent<Restart>();
    }
    
    void Update()
    {
        
        //Kill player on falling out of map
        if (gameObject.transform.localPosition.y < -10)
        {
            _restart.Death();
        }
        
        horizontal = Input.GetAxis("Horizontal");
        
        if (icy)
        {
            rb.AddForce(new Vector2(horizontal * speed * 3, rb.velocity.y));
        }
        else
        {
            //Left-right movement
            rb.velocity = new Vector2(horizontal * speed,rb.velocity.y); 
        }

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

        /*//Double jump toggle (for testing)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rocketBoots = !rocketBoots;
        }*/

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            hasFired = true;
            shootTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootTargetPos.z = 0f;                                    
            shootTargetPos = shootTargetPos - gameObject.transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        //Jump/doublejump physics
        if (isGrounded())
            doubleJump = true;

        if (hasJumped && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            hasJumped = false;
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        else if (hasJumped && doubleJump && rocketBoots)
        {
            FindObjectOfType<AudioManager>().Play("DoubleJump");
            doubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            hasJumped = false;
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        else if (hasJumped)
        {
            hasJumped = false;
        }

        // Shooting
        if (hasFired && Gun)
        {
            Shoot();
            hasFired = false;
        }
    }

    //Function to check if player is touching ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        if (raycastHit.normal.y > 0 && raycastHit.transform.tag != "Danger")
            return true;
        else
            return false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ice"))
            icy = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ice"))
            icy = false;
    }


    private void Shoot()
    {
        shootTargetPos.Normalize();
        Debug.Log("ShootVector: " + shootTargetPos);
        Rigidbody2D bulletItem = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;        
        bulletItem.velocity = shootTargetPos * bulletSpeed; // Change multiplier to a suitable bullet speed
        FindObjectOfType<AudioManager>().Play("Shoot");
    }
}
