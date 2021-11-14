using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 initialLocation;

    // Start is called before the first frame update
    void Start()
    {
        initialLocation = rb.position;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.name.Equals("Player"))
        { 
            Destroy(this.gameObject);
            if(collision.gameObject.name.StartsWith("Enemy"))
            {
                Destroy(collision.gameObject);
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
