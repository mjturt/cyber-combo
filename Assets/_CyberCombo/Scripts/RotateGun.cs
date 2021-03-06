using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class RotateGun : MonoBehaviour
{
    private Vector3 mouse_pos;
    [SerializeField] private Transform target; //Assign to the object you want to rotate
    private Vector3 object_pos;
    private float angle;

    public SpriteRenderer gunSprite;
    public Sprite fireSprite;
    public Sprite iceSprite;
    public Sprite magnetSprite;
    private PlayerMovement pM;
    public Transform playerPos;


    private void Start()
    {
        pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        gunSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate ()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        
        
        //Normal player rotation
        if (playerPos.rotation.z < 0.05 && playerPos.rotation.z > -0.05)
        {
            transform.rotation = Quaternion.Euler( new Vector3(0, 0, angle));
            gunSprite.flipX = false;
            if (angle < -90f || angle > 90f)
            {
                gunSprite.flipY = true;
                transform.position = new Vector3(playerPos.position.x - 0.2f, playerPos.position.y, 0);
            }
            else
            {
                gunSprite.flipY = false;
                transform.position = new Vector3(playerPos.position.x + 0.2f, playerPos.position.y, 0);
            }
        }
        //Player upside down
        else if(playerPos.rotation.z < -0.95 || playerPos.rotation.z > 0.95)
        {
            transform.rotation = Quaternion.Euler( new Vector3(0, 0, angle));
            gunSprite.flipX = true;
            if (angle < -90f || angle > 90f)
            {
                gunSprite.flipY = false;
                transform.position = new Vector3(playerPos.position.x + 0.2f, playerPos.position.y, 0);
            }
            else
            {
                gunSprite.flipY = true;
                transform.position = new Vector3(playerPos.position.x - 0.2f, playerPos.position.y, 0);
            }
        }
        //Player sideways +90 degree
        else if(playerPos.rotation.z < 0.95 && playerPos.rotation.z > 0.05)
        {
            transform.rotation = Quaternion.Euler( new Vector3(0, 0, angle +90f));
            gunSprite.flipX = false;
            if (angle < -90f || angle > 90f)
            {
                gunSprite.flipY = true;
                transform.position = new Vector3(playerPos.position.x, playerPos.position.y - 0.2f, 0);
            }
            else
            {
                gunSprite.flipY = false;
                transform.position = new Vector3(playerPos.position.x, playerPos.position.y + 0.2f, 0);
            }
        }
        //Player sideways -90 degree
        else if(playerPos.rotation.z > -0.95 || playerPos.rotation.z < -0.05)
        {
            transform.rotation = Quaternion.Euler( new Vector3(0, 0, angle +90f));
            gunSprite.flipX = true;
            if (angle < -90f || angle > 90f)
            {
                gunSprite.flipY = false;
                transform.position = new Vector3(playerPos.position.x, playerPos.position.y + 0.2f, 0);
            }
            else
            {
                gunSprite.flipY = true;
                transform.position = new Vector3(playerPos.position.x, playerPos.position.y - 0.2f, 0);
            }
        }
        
    }
}
