using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Controller _playerController;
    private bool _locked = true;
    void Start()
   {
       _playerController = GetComponent<Controller>();
   } 
    
    private void OnTriggerEnter2D(Collider2D other)
   {
       //Handles picking up rocket boots
       if(other.gameObject.CompareTag("RocketBoots"))
       {
           Destroy(other.gameObject);
           _playerController.rocketBoots = true;
       }
       else if (other.gameObject.CompareTag("Goal") && _locked == false)
       {
           print("You Win!");
           _locked = true;
       }
       else if(other.gameObject.CompareTag("Key"))
       {
            Destroy(other.gameObject);
            _locked = false;
            print("Door unlocked!");

       }
   }
}
