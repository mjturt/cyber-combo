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
    }

    public void setIsHostile(bool isHostile)
    {
        this.fromEnemy = isHostile;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy ( " +fromEnemy+ " ) shot at " + collision.gameObject.name);
        if (fromEnemy) // Enemy can only harm players
        {            
            if (collision.gameObject.name.Equals("Player"))
            {
                _restart.Death();
                Destroy(this.gameObject);
            }
            else if (!collision.gameObject.name.StartsWith("Enemy"))
            {
                Destroy(this.gameObject); // shoot through other mobs but not walls
            }
        }
        else if (!collision.gameObject.name.Equals("Player"))
        { 
            Destroy(this.gameObject);
            if(collision.gameObject.CompareTag("Danger") && this.gameObject.GetComponent<SpriteRenderer>().sprite != iceSprite)
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Danger") && this.gameObject.GetComponent<SpriteRenderer>().sprite == iceSprite)
            {
                collision.gameObject.GetComponent<Enemy>().enabled = false;
                collision.gameObject.tag = "Frozen";
                collision.gameObject.GetComponent<Animator>().SetBool("frozen", true);
            }
            else if (collision.gameObject.CompareTag("Frozen") && this.gameObject.GetComponent<SpriteRenderer>().sprite != iceSprite)
            {
                collision.GetComponent<Enemy>().enabled = true;
                collision.gameObject.tag = "Danger";
                collision.gameObject.GetComponent<Animator>().SetBool("frozen", false);
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
        //return (Vector3.Distance(rb.position, initialLocation) > 20.0f);        
        return true;
    }
}
