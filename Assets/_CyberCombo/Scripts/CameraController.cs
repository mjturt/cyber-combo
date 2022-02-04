using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera playerCam;
    private Camera fullCam;
    
    //Camera borders
    public float minXPosition = -7f; // left border
    public float maxXPosition = 8f; //  right border
    public float minYPosition = -5f; // down border
    public float maxYPosition = 5f; //  up border

    private PlayerMovement pM;
    private Rigidbody2D rb;
    private Animator anim;
    
    private GameObject following;
    private float interested;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GetComponent<Camera>();
        fullCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        pM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
        following = GameObject.Find("Player");
        interested = 1f;
    }

    void Update()
    {
        //Camera restriction
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPosition, maxXPosition),Mathf.Clamp(transform.position.y, minYPosition, maxYPosition),-3);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Mathf.Clamp(following.transform.position.x,minXPosition,maxXPosition),Mathf.Clamp(following.transform.position.y,minYPosition,maxYPosition),-3f), interested);

        //Camera switching
        if(Input.GetKeyDown(KeyCode.C))
        {
            playerCam.enabled = false;
            fullCam.enabled = true;
            
            pM.enabled = false;
            //pM.accelerationSpeed = 0;
            //pM.jumpForce = 0;
            rb.velocity = new Vector2(0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            anim.enabled = false;
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            playerCam.enabled = true;
            fullCam.enabled = false;
            
            pM.enabled = true;
            //pM.accelerationSpeed = 120f;
            //pM.jumpForce = 500f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.enabled = true;
        }
    }
    
}
