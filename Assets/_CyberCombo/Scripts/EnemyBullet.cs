using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 initialLocation;        
    public Sprite enemySprite;
    private PlayerMovement pM;

    private GameObject owner;

    private Restart _restart;
    private AudioManager _audio;

    // Start is called before the first frame update
    void Start()
    {
        initialLocation = rb.position; 
        pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = enemySprite;

        _restart = pM.GetComponent<Restart>(); // needed so player can be killed
        _audio = FindObjectOfType<AudioManager>();
    }

    public void setOwner(GameObject trigger)
    {
        this.owner = trigger;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        Debug.Log(  "Enemy " + this.gameObject.name + " shooting at " + target.name 
                  + " target.equals(shooter) = " + target.Equals(this.owner));
              
        if (target.name.Equals("Player"))
        {
            _restart.Death();
            Destroy(this.gameObject);
        }        
        else if (!target.Equals(this.owner) && !target.name.Contains("Bullet")) 
        {
            Destroy(this.gameObject); // stop the bullet
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
