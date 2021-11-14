using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float walkSpeed = 1.0f;      // Walkspeed
    private float wallLeft = 0.0f;       // Define wallLeft
    private float wallRight = 5.0f;      // Define wallRight
    float walkingDirection = 1.0f;
    Vector2 walkAmount;
    
    //Walk Width defines how far enemy goes from left wall
    public float walkWidth;


    void Start () {
        wallLeft = transform.position.x;
        wallRight = transform.position.x + walkWidth;
    }

    // Update is called once per frame
    void Update () {
        walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;
        if (walkingDirection > 0.0f && transform.position.x >= wallRight) {
            walkingDirection = -1.0f;
        } else if (walkingDirection < 0.0f && transform.position.x <= wallLeft) {
            walkingDirection = 1.0f;
        }
        transform.Translate(walkAmount);
    }
}