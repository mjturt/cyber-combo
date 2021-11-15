using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float walkSpeed = 1.0f;      // Walkspeed
    private float wallLeft = 0.0f;       // Define wallLeft
    private float wallRight = 5.0f;      // Define wallRight
    float walkingDirection = 1.0f;
    Vector2 walkAmount;
    private SpriteRenderer sprite;
    
    //Walk Width defines how far enemy goes from left wall
    public float walkWidth;


    void Start () {
        wallLeft = transform.position.x;
        wallRight = transform.position.x + walkWidth;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update () {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        if (walkingDirection > 0.0f && transform.position.x >= wallRight) {
            walkingDirection = -1.0f;
            sprite.flipX = false;

        } else if (walkingDirection < 0.0f && transform.position.x <= wallLeft)
        {
            walkingDirection = 1.0f;
            sprite.flipX = true;
        }
        transform.Translate(walkAmount);
    }
}