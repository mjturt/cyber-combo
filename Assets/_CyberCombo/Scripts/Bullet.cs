using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 initialLocation;
    public Sprite iceSprite;    
    private PlayerMovement pM;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        // Players can't shoot themselves nor enemy bullets
        if (!target.name.Equals("Player") && !target.name.ToLower().Contains("bullet")) 
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
                }                
                else
                {                    
                    Destroy(target);
                    if (null != _audio) _audio.Play("EnemyDeath");
                }
            }
            else if (target.CompareTag("Frozen") && currentSprite != iceSprite)
            {
                target.GetComponent<Enemy>().UnFreeze();
                if (null != _audio) _audio.Play("UnFreeze");
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
