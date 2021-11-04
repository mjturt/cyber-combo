using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private PlayerMovement pMovement;
    public GameObject _slotSystem;
    private bool _locked = true;
    private bool _canPressButton;
    private Restart _restart;

    public GameObject goalPortal;
    public GameObject unlockButton;
    public GameObject gameManager;
    public Sprite pressedButton;
    void Start()
   {
       pMovement = GetComponent<PlayerMovement>();
       _restart = GetComponent<Restart>();
   }

    private void Update()
    {   //Key picking logic
        if (_canPressButton)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _locked = false;
                print("Door unlocked!");
                goalPortal.GetComponent<Animator>().SetBool("open", true);
                unlockButton.GetComponent<SpriteRenderer>().sprite = pressedButton;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
   {
       //Handles picking up rocket boots
       if(other.gameObject.CompareTag("FireElement"))
       {
           Destroy(other.gameObject);
           pMovement.Fire = true;
           _slotSystem.SetActive(true);
           pMovement.rocketBoots = true;
       }
       //End of level handling
       else if (other.gameObject.CompareTag("Goal") && _locked == false)
       {
           print("You Win!");
           gameManager.GetComponent<LevelComplete>().LevelCompleted();

       }
       //On key trigger area logic
       else if(other.gameObject.CompareTag("Key"))
       {
           other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
           _canPressButton = true;
       }
       
       
       
   }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Out of key trigger area logic
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            _canPressButton = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            _restart.Death();
        }
    }
}
