using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    private PlayerMovement pMovement;
    public GameObject _slotSystem;
    public bool _locked = true;
    private bool _canPressButton;
    public bool pressed = false;
    private Restart _restart;
    private SlotSystem slots;

    public GameObject goalPortal;
    public GameObject unlockButton;
    public GameObject gameManager;
    public Sprite pressedButton;
    
   
    
    void Start()
   {
       pMovement = GetComponent<PlayerMovement>();
       _restart = GetComponent<Restart>();
       slots = _slotSystem.GetComponent<SlotSystem>();

   }

    private void Update()
    {   //Key picking logic
        if (_canPressButton)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (GameObject.Find("ButtonBase"))
                {
                    pressed = true;
                    unlockButton.GetComponent<SpriteRenderer>().sprite = pressedButton;
                    FindObjectOfType<AudioManager>().Play("GetKey");
                }
                else
                {
                    _locked = false;
                    print("Door unlocked!");
                    goalPortal.GetComponent<Animator>().SetBool("open", true);
                    unlockButton.GetComponent<SpriteRenderer>().sprite = pressedButton;
                    FindObjectOfType<AudioManager>().Play("GetKey");
                }

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
       else if (other.gameObject.CompareTag("IceElement"))
       {
           Destroy(other.gameObject);
           pMovement.Ice = true;
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
       //Pop info text
       else if (other.gameObject.CompareTag("Info"))
       {
           other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
       }
       else if (other.gameObject.CompareTag("Gun"))
       {
           Destroy(other.gameObject);
           pMovement.Gun = true;

           slots.imgs[6].enabled = true; //left white bg
           slots.imgs[7].enabled = true; //right white bg
           slots.imgs[11].enabled = true;//line
           slots.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<RawImage>().enabled = true; //gun
           
           if (slots.imgs[2].enabled == false)
               slots.imgs[8].enabled = true; //ice power
           else
               slots.imgs[10].enabled = true; //fire power

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
        else if (other.gameObject.CompareTag("Info"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            _restart.Death();
        }
        else if (other.gameObject.CompareTag("Lava") && !pMovement.iceBoots)
        {
            _restart.Death();
        }
    }
}
