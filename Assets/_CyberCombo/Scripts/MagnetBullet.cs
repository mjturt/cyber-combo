using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 initialLocation;
    public PlayerMovement pM;
    private AudioManager _audio;
    private LineRenderer rope;
    private Transform ropeEndPoint;
    private Transform ropeStartPoint;
    // Start is called before the first frame update
    void Start()
    {
        initialLocation = rb.position;
        rope = GetComponent<LineRenderer>();
        ropeEndPoint = transform.GetChild(0);
        ropeStartPoint = pM.gameObject.transform.Find("Gun");
        _audio = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject target = collision.gameObject;

        // Players can't shoot themselves nor enemy bullets
        if (!target.name.Equals("Player") && !target.name.ToLower().Contains("bullet"))
        {
            if (!target.CompareTag("Metal"))
            {
                Destroy(this.gameObject);
                if (pM.secondMagnetBulletFired)
                    pM.secondMagnetBulletFired = false;
                else
                    pM.magnetBulletFired = false;
            }
            else // Bullet hit metal
            {
                pM.bulletTimer = 5; // use timer to keep some record of previous grapple speed
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                rb.velocity = new Vector2(0f, 0f);
                pM.isGrappling = true;
                if (pM.secondMagnetBulletFired)
                {
                    pM.mBulletInstance.DestroyMagnetBullet();
                    pM.mBulletInstance = this;
                    pM.mBulletInstance2 = null;
                    pM.secondMagnetBulletFired = false;
                    pM.touchesGrapple = false;
                    pM.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePosition;
                }
            }
        }

        // Above check doesn't work for magnet bullets, so let's check them here
        if (target.name.Equals("Bullet Magnet(Clone)"))
        {
            if (target.transform.Equals(pM.mBulletInstance.transform)) // Only delete second magnet bullet
            {
                pM.secondMagnetBulletFired = false;
                Destroy(this.gameObject);
            }
        }
    }

    public void DestroyMagnetBullet()
    {
        if (pM.secondMagnetBulletFired)
            pM.secondMagnetBulletFired = false;
        else
            pM.magnetBulletFired = false;

        Destroy(this.gameObject);
    }

    void Update()
    {
        rope.SetPosition(0, ropeStartPoint.position);
        rope.SetPosition(1, ropeEndPoint.position);

        // Destroy offscreen bullets
        if (!stillOnScreen())
        {
            if (pM.secondMagnetBulletFired)
                pM.secondMagnetBulletFired = false;
            else
                pM.magnetBulletFired = false;
            Destroy(this.gameObject);
        }
    }

    bool stillOnScreen()
    {
        //Debug.Log("bullet distance to init: " + Vector3.Distance(rb.position, initialLocation));
        return (Vector3.Distance(rb.position, initialLocation) < 30.0f);
    }
}
