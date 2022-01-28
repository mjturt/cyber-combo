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
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerCam = GetComponent<Camera>();
        fullCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        pM = GameObject.Find("Player").GetComponent<PlayerMovement>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    void Update()
    {
        //Camera restriction
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXPosition, maxXPosition),Mathf.Clamp(transform.position.y, minYPosition, maxYPosition),-3);
        
        //Camera switching
        if(Input.GetKeyDown(KeyCode.C))
        {
            playerCam.enabled = false;
            fullCam.enabled = true;
            
            pM.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            anim.enabled = false;
        }
        if(Input.GetKeyUp(KeyCode.C))
        {
            playerCam.enabled = true;
            fullCam.enabled = false;
            
            pM.enabled = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.enabled = true;
        }
    }
    
}
