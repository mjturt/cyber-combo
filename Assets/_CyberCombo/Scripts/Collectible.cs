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

    public GameObject goalPortal;
    public GameObject unlockButton;
    public GameObject gameManager;
    public Sprite openPortal;
    public Sprite pressedButton;
    void Start()
   {
       pMovement = GetComponent<PlayerMovement>();
   }

    private void Update()
    {
        if (_canPressButton)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _locked = false;
                print("Door unlocked!");
                goalPortal.GetComponent<SpriteRenderer>().sprite = openPortal;
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
       else if (other.gameObject.CompareTag("Goal") && _locked == false)
       {
           print("You Win!");
           gameManager.GetComponent<LevelComplete>().LevelCompleted();
           PlayerPrefs.SetInt("HighestLevel",1);

       }
       else if(other.gameObject.CompareTag("Key"))
       {
           other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
           _canPressButton = true;
       }
       
   }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            _canPressButton = false;
        }
    }

    
}
