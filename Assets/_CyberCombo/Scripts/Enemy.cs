using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float walkSpeed = 1.0f;      // Walkspeed
    protected float walkingDirection = 1.0f;
    Vector2 walkAmount;

    public bool canShoot = false;  // can the enemy shoot or not
    public EnemyBullet bullet;
    public float bulletSpeed = 5.0f;
    public float rateOfFire = 5.0f; // how many seconds between shots 
    protected float gunCoolDown = 0f;  // is it time to shoot yet?
    public GameObject bulletSpawnPoint;

    public float freezeTimer = 10.0f; // Time spent frozen
    protected float timeToMelt = 0.0f;
    public bool permaFrost = false; // is freezing permanent
    protected bool frozen = false;

    protected SpriteRenderer sprite;
    
    private AudioManager _audio;
    
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        gunCoolDown = rateOfFire;
        _audio = FindObjectOfType<AudioManager>();
    }

    void Update () {
        if (!permaFrost && frozen && timeToMelt < 0)
        {
            UnFreeze();
        }
        else if (!frozen)
        {
            bool turnAround = false;

            // Check for obstacle
            BoxCollider2D bc = transform.GetComponent<BoxCollider2D>();
            RaycastHit2D[] results = new RaycastHit2D[5];
            int hitCount = Physics2D.BoxCastNonAlloc(bc.bounds.center, new Vector2(bc.bounds.size.x, bc.bounds.size.y * 0.9f), // 0.9f to avoid detecting floor as wall when falling
                                                        0f, new Vector2(walkingDirection, 0), results, 0.1f);

            for (int i = 0; i < hitCount; i++)
            {
                if (results[i].transform) // Hits something
                {
                    string name = results[i].transform.name;
                    if (name.Equals("Metal") || name.Equals("Ice") || name.Equals("Lava") || name.Contains("Ground") || (name.Contains("Turret") && !name.Contains("bullet")))
                        turnAround = true;
                    else if ((results[i].transform.tag == "Danger" || results[i].transform.tag == "Frozen") && name != transform.name && !transform.name.ToLower().Contains("bullet"))
                    {
                        turnAround = true;
                    }
                }
            }

            // Check for fall
            Collider2D[] fallResults = new Collider2D[5];
            Physics2D.OverlapBoxNonAlloc(new Vector2(bc.bounds.center.x + bc.bounds.size.x * walkingDirection * 0.75f, bc.bounds.center.y - bc.bounds.size.y / 2), new Vector2(bc.bounds.size.x * 0.25f, 0.25f), 0f, fallResults);

            bool onGround = false;
            for (int i = 0; i < fallResults.Length; i++)
            {
                if (fallResults[i] && fallResults[i].transform) // Hits something
                {
                    if (fallResults[i].isTrigger == false && fallResults[i].transform.tag != "Danger")
                        onGround = true;
                }
            }

            if (onGround == false)
            {
                if (transform.GetComponent<Rigidbody2D>().velocity.y < 0.0001f && transform.GetComponent<Rigidbody2D>().velocity.y > -0.0001f)
                {
                    turnAround = true;
                }
            }

            // Move
            walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
            transform.Translate(walkAmount);

            // Turn
            // Turning is done after moving to make sure that colliding enemies both change direction
            if (turnAround)
                TurnAround();

            // Shoot
            this.gunCoolDown = this.gunCoolDown - Time.deltaTime;            
            if (canShoot && 0 > gunCoolDown)
            {
                Shoot();
                this.gunCoolDown = rateOfFire;            
            }
        }
        else // Melt
        {
            this.timeToMelt = this.timeToMelt - Time.deltaTime;
        }

    }

    public void TurnAround()
    {
        walkingDirection *= -1;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * walkingDirection, transform.localScale.y, transform.localScale.z);
    }

    protected void Shoot()
    {
        if (null != _audio) _audio.Play("EnemyShoot");
        EnemyBullet bulletItem = Instantiate(bullet, bulletSpawnPoint.transform.position, transform.rotation) as EnemyBullet;
        bulletItem.setOwner(this.gameObject);
        
        // Shoot towards the direction entity is facing
        bulletItem.rb.velocity = new Vector2(walkingDirection * bulletSpeed, 0);         
    }

    public void Freeze()
    {
        if (null != _audio) _audio.Play("Freeze");
        this.gameObject.tag = "Frozen";
        this.GetComponent<Animator>().SetBool("frozen", true);                
        this.timeToMelt = freezeTimer;
        this.gunCoolDown = rateOfFire;
        this.frozen = true;
    }

    public void UnFreeze()
    {
        if (null != _audio) _audio.Play("UnFreeze");
        this.gameObject.tag = "Danger";
        this.GetComponent<Animator>().SetBool("frozen", false);                
        this.timeToMelt = 0.0f;
        this.frozen = false;
    }
}
