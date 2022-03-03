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

    private AudioManager _audio;

    public bool requireMultipleButtons;
    public int buttonsLeft;

    private Collider2D currentButton;
    
    void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        _restart = GetComponent<Restart>();
        slots = _slotSystem.GetComponent<SlotSystem>();
        _audio = FindObjectOfType<AudioManager>();
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
                    if (null != _audio) _audio.Play("GetKey");
                }
                else
                {
                    // level 12 with multiple buttons logic
                    if (requireMultipleButtons)
                    {
                        buttonsLeft--;
                        bool completed = (buttonsLeft == 0);

                        currentButton.GetComponent<SpriteRenderer>().sprite = pressedButton;
                        if (null != _audio) _audio.Play("GetKey");
                        currentButton.enabled = false; // Disable trigger

                        if (completed)
                        {
                            _locked = false;
                            print("Door unlocked!");
                            goalPortal.GetComponent<Animator>().SetBool("open", true);
                        }
                    }

                    //normal logic
                    else
                    {
                        _locked = false;
                        print("Door unlocked!");
                        goalPortal.GetComponent<Animator>().SetBool("open", true);
                        unlockButton.GetComponent<SpriteRenderer>().sprite = pressedButton;
                        if (null != _audio) _audio.Play("GetKey");
                    }
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
            if (null != _audio) _audio.Play("GetFire");
            pMovement.Fire = true;
            _slotSystem.SetActive(true);
            pMovement.rocketBoots = true;
        }
        else if (other.gameObject.CompareTag("IceElement"))
        {
            if (null != _audio) _audio.Play("GetIce");
            Destroy(other.gameObject);
            pMovement.Ice = true;
        }
        else if (other.gameObject.CompareTag("MagnetPickup"))
        {
            //if (null != _audio) _audio.Play("GetMagnet");  <----Magnet sounds here...
            Destroy(other.gameObject);
            pMovement.Magnet = true;
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
            if (null != _audio) _audio.Play("Info");
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            _canPressButton = true;
            currentButton = other;
        }
        //Pop info text
        else if (other.gameObject.CompareTag("Info"))
        {
            if (null != _audio) _audio.Play("Info");
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Gun"))
        {
            if (null != _audio) _audio.Play("GetGun");
            Destroy(other.gameObject);
            pMovement.Gun = true;
            pMovement.gunPos.GetComponent<SpriteRenderer>().enabled = true;

            slots.transform.GetChild(1).gameObject.SetActive(true); //gun
            
            // Change icon if necessary
            if (slots.ice1.activeInHierarchy)
            {
                slots.ice2.SetActive(false);
                slots.fire2.SetActive(true);
                pMovement.iceBullet = false;
                pMovement.fireBullet = true;
            }

        }
       
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Out of key trigger area logic
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            _canPressButton = false;
            currentButton = null;
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
