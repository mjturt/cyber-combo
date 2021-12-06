using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 initialLocation;
    public Sprite iceSprite;
    public Sprite fireSprite;
    private PlayerMovement pM;

    private bool fromEnemy = false; // who's the shooter?

    private Restart _restart;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Start()
    {
        initialLocation = rb.position; 
        pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        iceSprite = pM.iceSprite;
        if (pM.iceBullet)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = iceSprite;
        }

        _restart = pM.GetComponent<Restart>(); // needed so player can be killed
        _audio = FindObjectOfType<AudioManager>();
    }

    public void setIsHostile(bool isHostile)
    {
        this.fromEnemy = isHostile;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        
        if (fromEnemy) // Enemy can only harm players
        {            
            if (target.name.Equals("Player"))
            {
                _restart.Death();
                Destroy(this.gameObject);
            }
            else if (!target.name.StartsWith("Enemy"))
            {
                Destroy(this.gameObject); // shoot through other mobs but not walls
            }
        }
        else if (!target.name.Equals("Player")) // Players can't shoot themselves
        {
            Sprite currentSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
            Destroy(this.gameObject);
            // handle enemies, except turrets which are invincible
            if (target.CompareTag("Danger") && !target.name.Contains("Turret"))
            {
                if (currentSprite == iceSprite)
                {
                    target.GetComponent<Enemy>().Freeze();
                    if (null != _audio) _audio.Play("Freeze");
                    //FindObjectOfType<AudioManager>().Play("Freeze");
                }                
                else
                {                    
                    Destroy(target);
                    if (null != _audio) _audio.Play("EnemyDeath");
                    //FindObjectOfType<AudioManager>().Play("EnemyDeath");                 
                }
            }
            else if (target.CompareTag("Frozen") && currentSprite != iceSprite)
            {
                target.GetComponent<Enemy>().UnFreeze();
                if (null != _audio) _audio.Play("UnFreeze");
                //FindObjectOfType<AudioManager>().Play("UnFreeze");
            }
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!stillOnScreen())
        {
            Destroy(this.gameObject);
        }
    }

    bool stillOnScreen()
    {        
        //Debug.Log("bullet distance to init: " + Vector3.Distance(rb.position, initialLocation));
        return (Vector3.Distance(rb.position, initialLocation) < 30.0f);                       
    }
}
