using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private bool hasJumped;
    private float accelerationSpeed = 120.0f;
    private float maximumSpeed = 5.0f;
    public float jumpForce = 500f;
    private BoxCollider2D bc;
    [SerializeField] private LayerMask groundLayer;
    public bool onGround;
    private bool attractedToMetal;
    private float rotationSpeed = 10f;
    private bool doubleJump;
    public bool rocketBoots;
    public bool iceBoots;
    public bool magnetBoots;
    public bool fireBullet;
    public bool iceBullet;
    public bool magnetBullet;
    public GameObject rocketBootsEffect;
    private float effectDeleteTimer;
    private float effectHeight = 0.45f;

    public bool Fire = false;
    public bool Magnet = false;
    public bool Ice = false;
    public bool Gun = false;

    private bool hasFired = false;
    public Bullet bullet;
    public Sprite iceSprite;
    private Vector3 shootTargetPos;
    public float bulletSpeed;

    public GameObject gunPos;
    

    private Restart _restart;
    private AudioManager _audio;
    private GameObject doubleJumpEffect;

    private bool icy;

    private AudioSource walkingSource;
    public Animator animator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        _restart = GetComponent<Restart>();
        _audio = FindObjectOfType<AudioManager>();
        walkingSource = _audio.GetSource("Walk");
    }
    
    void Update()
    {
        
        if (effectDeleteTimer > -1 && effectDeleteTimer < 0)
        {
            Destroy(doubleJumpEffect);
            effectDeleteTimer = -1f;
        }
        if (effectDeleteTimer > 0)
        {
            Vector3 effectPosition = transform.position;
            effectPosition.y -= effectHeight;
            if (doubleJumpEffect)
                doubleJumpEffect.transform.position = effectPosition;
            effectDeleteTimer = effectDeleteTimer - 1*Time.deltaTime;
        }


        //Kill player on falling out of map
        if (gameObject.transform.localPosition.y < -10)
        {
            _restart.Death();
        }

        moveLeft = Input.GetAxisRaw("Horizontal") < 0;
        moveRight = Input.GetAxisRaw("Horizontal") > 0;

        //Jump animations and triggers
        if (Input.GetButtonDown("Jump"))
        {
            hasJumped = true;
            
        }

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            hasFired = true;
            shootTargetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootTargetPos.z = 0f;                                    
            shootTargetPos = shootTargetPos - gunPos.transform.position;
        }
    }

    private void FixedUpdate()
    {
        onGround = touchesGround();

        if (!attractedToMetal)
            rotatePlayer();
        else
            onGround = true;


        moveHorizontally();

        //Jump/doublejump physics
        if (onGround)
            doubleJump = true;
        

        if (hasJumped && onGround && !attractedToMetal)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            hasJumped = false;
            if (null != _audio) _audio.Play("Jump");
        }
        else if (hasJumped && doubleJump && rocketBoots && !attractedToMetal)
        {
            if (null != _audio) _audio.Play("DoubleJump");
            doubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);
            hasJumped = false;

            //Double jump effect
            Vector3 effectPosition = transform.position;
            effectDeleteTimer = 0.5f;
            effectPosition.y -= effectHeight;
            if (doubleJumpEffect)
                Destroy(doubleJumpEffect);
            else
                doubleJumpEffect = Instantiate(rocketBootsEffect, effectPosition, transform.rotation);
        }
        else if (hasJumped)
        {
            hasJumped = false;
        }

        // Shooting
        if (hasFired && Gun)
        {            
            hasFired = false;
            Shoot();
        }

        // Walking sound
        if (_audio != null) {
            if (isWalking() && !walkingSource.isPlaying) {
                walkingSource.Play();
            }
            if (!isWalking()) walkingSource.Stop();
        }

        // Player animations
        animator.SetFloat("Speed", Input.GetAxisRaw("Horizontal"));
        animator.SetBool("isGrounded", onGround);
        animator.SetBool("doubleJump",doubleJump);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Metal" && magnetBoots)
        {
            rb.gravityScale = 0;
            attractedToMetal = true;
            Vector2 closestPoint = collision.ClosestPoint(bc.bounds.center);
            Vector2 actualVector = new Vector2(closestPoint.x - bc.bounds.center.x, closestPoint.y - bc.bounds.center.y);
            actualVector.Normalize();

            // Attract
            rb.AddForce(9.8f * 180 * new Vector2(closestPoint.x - bc.bounds.center.x, closestPoint.y - bc.bounds.center.y));

            // Rotate
            rotateToDegree(actualVector);
        }
        else
        {
            attractedToMetal = false;
            rb.gravityScale = 1.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attractedToMetal = false;
        rb.gravityScale = 1.5f;
    }

    /*
     * Rotates the player to the direction opposite to the vector
     */
    private void rotateToDegree(Vector2 reverseDirection)
    {
        float angle = (Mathf.Atan2(reverseDirection.y, reverseDirection.x) / (2 * Mathf.PI) * 360);

        float playerAngle = transform.eulerAngles.z;
        
        if (angle < 0)
            angle += 360;

        angle -= 270;

        if (angle < 0)
            angle += 360;

        if ((playerAngle > angle + 5.5f && playerAngle <= angle + 180) || playerAngle < angle - 180)
        {
            transform.Rotate(Vector3.forward, -rotationSpeed);
        }
        else if ((playerAngle < angle - 5.5f && playerAngle >= angle - 180) || playerAngle > angle + 180)
        {
            transform.Rotate(Vector3.forward, rotationSpeed);
        }
    }

    private void rotatePlayer()
    {
        if (transform.eulerAngles.z > 5f && transform.eulerAngles.z < 180f)
            transform.Rotate(Vector3.forward, -rotationSpeed);
        else if (transform.eulerAngles.z > 180f && transform.eulerAngles.z < 355f)
            transform.Rotate(Vector3.forward, rotationSpeed);
        else // shouldn't happen. I don't know whether this works in resetting rotation.
            transform.rotation.Set(0, 0, 0, transform.rotation.w);
    }

    private void moveHorizontally()
    {
        // Magnetic movement
        if (attractedToMetal)
        {
            float magneticSpeedMultiplier = 0.7f;
            if (moveRight)
            {
                if (rb.transform.right.x * rb.velocity.x + rb.transform.right.y * rb.velocity.y < -1f)
                    rb.velocity = new Vector2(rb.velocity.x * 0.75f, rb.velocity.y * 0.75f);
                else if (rb.transform.right.x * rb.velocity.x + rb.transform.right.y * rb.velocity.y < maximumSpeed)
                    rb.AddRelativeForce(new Vector2(accelerationSpeed * magneticSpeedMultiplier, 0));
            }
            else if (moveLeft)
            {
                if (-rb.transform.right.x * rb.velocity.x + -rb.transform.right.y * rb.velocity.y < -1f)
                    rb.velocity = new Vector2(rb.velocity.x * 0.75f, rb.velocity.y * 0.75f);
                else if (-rb.transform.right.x * rb.velocity.x + -rb.transform.right.y * rb.velocity.y < maximumSpeed)
                    rb.AddRelativeForce(new Vector2(-accelerationSpeed * magneticSpeedMultiplier, 0));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x * 0.75f, rb.velocity.y * 0.75f);
            }
        }

        // Non magnetic movement
        else if (moveLeft || moveRight) // accelerate
        {
            if (!onGround) // air
            {
                if (moveRight)
                {
                    if (rb.velocity.x < 0)
                        rb.AddRelativeForce(new Vector2(accelerationSpeed, 0));
                    else if (rb.velocity.x < maximumSpeed)
                        rb.AddRelativeForce(new Vector2(accelerationSpeed * Mathf.Sqrt((maximumSpeed - rb.velocity.x) / maximumSpeed), 0));
                }
                else if (moveLeft)
                {
                    if (rb.velocity.x > 0)
                        rb.AddRelativeForce(new Vector2(-accelerationSpeed, 0));
                    else if (rb.velocity.x > -maximumSpeed)
                        rb.AddRelativeForce(new Vector2(-accelerationSpeed * Mathf.Sqrt((maximumSpeed + rb.velocity.x) / maximumSpeed), 0));
                }
            }
            else if (!icy || iceBoots) // ground
            {
                if (moveRight)
                {
                    if (rb.velocity.x < -1)
                        rb.velocity = new Vector2(rb.velocity.x * 0.82f, rb.velocity.y); // Prevent acceleration being smaller than deceleration
                    else if (rb.velocity.x < 0)
                        rb.AddRelativeForce(new Vector2(accelerationSpeed, 0));
                    else if (rb.velocity.x < maximumSpeed)
                        rb.AddRelativeForce(new Vector2(accelerationSpeed * Mathf.Sqrt((maximumSpeed - rb.velocity.x) / maximumSpeed), 0));
                }
                else if (moveLeft)
                {
                    if (rb.velocity.x > 1)
                        rb.velocity = new Vector2(rb.velocity.x * 0.82f, rb.velocity.y);
                    else if (rb.velocity.x > 0)
                        rb.AddRelativeForce(new Vector2(-accelerationSpeed, 0));
                    else if (rb.velocity.x > -maximumSpeed)
                        rb.AddRelativeForce(new Vector2(-accelerationSpeed * Mathf.Sqrt((maximumSpeed + rb.velocity.x) / maximumSpeed), 0));
                }
            }
            else // ice
            {
                float iceAccelerationFactor = 0.4f;
                if (moveRight)
                {
                    if (rb.velocity.x < 0)
                        rb.AddRelativeForce(new Vector2(accelerationSpeed * iceAccelerationFactor, 0));
                    else if (rb.velocity.x < maximumSpeed)
                        rb.AddRelativeForce(new Vector2(accelerationSpeed * iceAccelerationFactor * Mathf.Sqrt((maximumSpeed - rb.velocity.x) / maximumSpeed), 0));
                }
                else if (moveLeft)
                    if (rb.velocity.x > 0)
                        rb.AddRelativeForce(new Vector2(-accelerationSpeed * iceAccelerationFactor, 0));
                    else if (rb.velocity.x > -maximumSpeed)
                        rb.AddRelativeForce(new Vector2(-accelerationSpeed * iceAccelerationFactor * Mathf.Sqrt((maximumSpeed + rb.velocity.x) / maximumSpeed), 0));
            }
        }
        else // decelerate
        {
            if (!onGround) // air
            {
                if (Mathf.Abs(rb.velocity.x) < 1)
                    rb.velocity = new Vector2(rb.velocity.x * 0.85f, rb.velocity.y);
                else if (rb.velocity.x > 0)
                    rb.AddRelativeForce(new Vector2(-accelerationSpeed * 0.9f, 0));
                else if (rb.velocity.x < 0)
                    rb.AddRelativeForce(new Vector2(accelerationSpeed * 0.9f, 0));
            }
            else if (!icy || iceBoots) // ground
            {
                if (Mathf.Abs(rb.velocity.x) < 0.5)
                    rb.velocity = new Vector2(rb.velocity.x * 0.75f, rb.velocity.y);
                else
                    rb.velocity = new Vector2(rb.velocity.x * 0.82f, rb.velocity.y);
            }
            else // ice
            {
                if (Mathf.Abs(rb.velocity.x) < 0.3)
                    rb.velocity = new Vector2(rb.velocity.x * 0.90f, rb.velocity.y);
                else
                    rb.velocity = new Vector2(rb.velocity.x * 0.98f, rb.velocity.y);
            }
        }
    }

    //Function to check if player is touching ground
    private bool touchesGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);

        if (raycastHit.normal.y > 0 && raycastHit.transform.tag != "Danger")
            return true;
        else
            return false;
    }

    // Function to check if player is walking (for walking sounds)
    private bool isWalking() {
        if (Input.GetAxis("Horizontal") != 0 && onGround && rb.velocity.magnitude > .2) return true;
        return false;
    }

    // Function to check if player is walking left (for walking animation)
    private bool isMovingLeft() {
        if (0 < Input.GetAxis("Horizontal") && rb.velocity.magnitude > .2) return true;
        return false;
    }

    // Function to check if player is walking right (for walking animation)
    private bool isMovingRight() {
        if (0 > Input.GetAxis("Horizontal") && rb.velocity.magnitude > .2) return true;
        return false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ice"))
            icy = true;

        if (other.relativeVelocity.magnitude > 4)
            if (_audio != null) _audio.Play("Hit");
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
        Bullet bulletItem = Instantiate(bullet, gunPos.transform.position, transform.rotation) as Bullet;        
        bulletItem.rb.velocity = shootTargetPos * bulletSpeed; // Change multiplier to a suitable bullet speed
        if (fireBullet) {
            if (null != _audio) _audio.Play("ShootFire");
        } else if (iceBullet) {
            if (null != _audio) _audio.Play("ShootIce");
        } else {
            if (null != _audio) _audio.Play("ShootFire");
        }
    }
}
