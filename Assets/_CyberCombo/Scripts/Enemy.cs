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

    public float freezeTimer = 10.0f; // Time spent frozen
    private float timeToMelt = 0.0f;
    public bool permaFrost = false; // is freezing permanent
    private bool frozen = false;

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
        if (!permaFrost && frozen && timeToMelt < 0)
        {
            UnFreeze();
        }
        else if (!frozen)
        { 
            walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
            
            if (walkingDirection > 0.0f && transform.position.x >= wallRight) {
                walkingDirection = -1.0f;
                sprite.flipX = false;

            } else if (walkingDirection < 0.0f && transform.position.x <= wallLeft)
            {
                walkingDirection = 1.0f;
                sprite.flipX = true;
            }
            transform.Translate(walkAmount);

            this.gunCoolDown = this.gunCoolDown - Time.deltaTime;
            //Debug.Log("gunCooldown: " + gunCoolDown);
            if (canShoot && 0 > gunCoolDown)
            {
                Shoot();
                this.gunCoolDown = rateOfFire;            
            }
        }
        else
        {
            this.timeToMelt = this.timeToMelt - Time.deltaTime;
        }

    }

    void Shoot()
    {
        Bullet bulletItem = Instantiate(bullet, transform.position, transform.rotation) as Bullet;
        
        // Shoot towards the direction entity is facing
        bulletItem.rb.velocity = new Vector2(walkingDirection * bulletSpeed, 0);        
        bulletItem.setIsHostile(true);
        
    }

    public void Freeze()
    {
        this.gameObject.tag = "Frozen";
        this.GetComponent<Animator>().SetBool("frozen", true);                
        this.timeToMelt = freezeTimer;
        this.gunCoolDown = rateOfFire;
        this.frozen = true;
    }

    public void UnFreeze()
    {
        this.gameObject.tag = "Danger";
        this.GetComponent<Animator>().SetBool("frozen", false);                
        this.timeToMelt = 0.0f;
        this.frozen = false;
    }
}