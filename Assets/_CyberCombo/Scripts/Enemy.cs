using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float walkSpeed = 1.0f;      // Walkspeed
    private float wallLeft = 0.0f;       // Define wallLeft
    private float wallRight = 5.0f;      // Define wallRight
    float walkingDirection = 1.0f;
    Vector2 walkAmount;

    public bool canShoot = false;  // can the enemy shoot or not
    public Bullet bullet;
    public float bulletSpeed = 5.0f;
    public float rateOfFire = 5.0f; // how many seconds between shots 
    private float gunCoolDown = 0f;  // is it time to shoot yet?

    private SpriteRenderer sprite;
    
    //Walk Width defines how far enemy goes from left wall
    public float walkWidth;


    void Start () {
        wallLeft = transform.position.x;
        wallRight = transform.position.x + walkWidth;
        sprite = GetComponent<SpriteRenderer>();
        gunCoolDown = rateOfFire;
    }

    // Update is called once per frame
    void Update () {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        this.gunCoolDown = this.gunCoolDown - Time.deltaTime;
        if (walkingDirection > 0.0f && transform.position.x >= wallRight) {
            walkingDirection = -1.0f;
            sprite.flipX = false;

        } else if (walkingDirection < 0.0f && transform.position.x <= wallLeft)
        {
            walkingDirection = 1.0f;
            sprite.flipX = true;
        }
        transform.Translate(walkAmount);

        //Debug.Log("gunCooldown: " + gunCoolDown);
        if (canShoot && 0 > gunCoolDown)
        {
            Shoot();
            this.gunCoolDown = rateOfFire;            
        }
        
    }

    void Shoot()
    {
        Bullet bulletItem = Instantiate(bullet, transform.position, transform.rotation) as Bullet;
        
        // Shoot towards the direction entity is facing
        bulletItem.rb.velocity = new Vector2(walkingDirection * bulletSpeed, 0);        
        bulletItem.setIsHostile(true);
        
    }
}