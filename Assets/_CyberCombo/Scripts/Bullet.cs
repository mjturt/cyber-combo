using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 initialLocation;

    private PlayerMovement pM;

    // Start is called before the first frame update
    void Start()
    {
        initialLocation = rb.position; 
        pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Equals("Player"))
        { 
            Destroy(this.gameObject);
            if(collision.gameObject.CompareTag("Danger") && pM.fireBullet)
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Danger") && pM.iceBullet)
            {
                collision.gameObject.GetComponent<Enemy>().enabled = false;
                collision.gameObject.tag = "Untagged";
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
        return true;
        //return (Vector3.Distance(rb.position, initialLocation) > 100);        
    }
}
