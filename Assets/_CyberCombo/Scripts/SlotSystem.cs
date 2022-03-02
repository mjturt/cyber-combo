using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class SlotSystem : MonoBehaviour
{
    public GameObject fire1;
    public GameObject ice1;
    public GameObject magnet1;
    public GameObject fire2;
    public GameObject ice2;
    public GameObject magnet2;

    private PlayerMovement pM;
    private RotateGun rG;

    public int uClickCount;
    public int dClickCount = 1;

    private AudioManager _audio;
    private AnimatorOverrideController fireController;
    private AnimatorOverrideController iceController;
    private AnimatorOverrideController magnetController;
    
    void Start()
    {
        //pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        rG = GameObject.Find("/Player").GetComponentInChildren<RotateGun>();
        fireController = pM.fireController;
        iceController = pM.iceController;
        magnetController = pM.magnetController;

        if (pM)
        {
            if (pM.Fire)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                pM.animator.runtimeAnimatorController = fireController;
            }

            if (pM.Gun)
                transform.GetChild(1).gameObject.SetActive(true);
        }

        _audio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        //Upper row Logic
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Mouse1)) && pM.onGround && pM.inputEnabled)
        {
            uClickCount++;
            
            
            // All powers
            if (pM.Fire && pM.Ice && pM.Magnet)
            {
                if (_audio != null) _audio.Play("ChangeItem");
                /* game design changed
                //Skip element if its already assigned to other slot
                if (uClickCount % 3 == dClickCount % 3)
                {
                    uClickCount++;
                }
                */

                switch (uClickCount % 3)
                {
                    case 0:
                        magnet1.SetActive(false);
                        fire1.SetActive(true);

                        pM.magnetBoots = false;
                        pM.rocketBoots = true;

                        ice2.SetActive(true);
                        fire2.SetActive(false);

                        pM.iceBullet = true;
                        pM.fireBullet = false;

                        rG.gunSprite.sprite = rG.iceSprite;

                        pM.animator.runtimeAnimatorController = fireController;
                        
                        break;
                    case 1:
                        fire1.SetActive(false);
                        ice1.SetActive(true);

                        pM.rocketBoots = false;
                        pM.iceBoots = true;

                        ice2.SetActive(false);
                        magnet2.SetActive(true);

                        pM.iceBullet = false;
                        pM.magnetBullet = true;

                        rG.gunSprite.sprite = rG.magnetSprite;
                        
                        pM.animator.runtimeAnimatorController = iceController;
                        break;
                    case 2:
                        ice1.SetActive(false);
                        magnet1.SetActive(true);

                        pM.iceBoots = false;
                        pM.magnetBoots = true;

                        magnet2.SetActive(false);
                        fire2.SetActive(true);

                        pM.magnetBullet = false;
                        pM.fireBullet = true;
                        
                        rG.gunSprite.sprite = rG.fireSprite;
                        
                        pM.animator.runtimeAnimatorController = magnetController;
                        break;
                }
            }

            // Level 12 with no ice
            else if (pM.Fire && pM.Magnet)
            {
                if (_audio != null) _audio.Play("ChangeItem");
                switch (uClickCount % 2)
                {
                    case 1:
                        fire1.SetActive(false);
                        magnet1.SetActive(true);

                        pM.rocketBoots = false;
                        pM.magnetBoots = true;

                        magnet2.SetActive(false);
                        fire2.SetActive(true);

                        pM.magnetBullet = false;
                        pM.fireBullet = true;

                        rG.gunSprite.sprite = rG.fireSprite;

                        pM.animator.runtimeAnimatorController = magnetController;
                        break;
                    case 0:
                        magnet1.SetActive(false);
                        fire1.SetActive(true);

                        pM.magnetBoots = false;
                        pM.rocketBoots = true;

                        fire2.SetActive(false);
                        magnet2.SetActive(true);

                        pM.fireBullet = false;
                        pM.magnetBullet = true;

                        rG.gunSprite.sprite = rG.magnetSprite;

                        pM.animator.runtimeAnimatorController = fireController;
                        break;
                }
            }
            
            else if (pM.Gun)
            {
                if (_audio != null) _audio.Play("ChangeItem");
                switch (uClickCount % 2)
                {
                    case 1:
                        fire1.SetActive(false);
                        ice1.SetActive(true);

                        pM.rocketBoots = false;
                        pM.iceBoots = true;

                        ice2.SetActive(false);
                        fire2.SetActive(true);

                        pM.iceBullet = false;
                        pM.fireBullet = true;
                        
                        rG.gunSprite.sprite = rG.fireSprite;
                        
                        pM.animator.runtimeAnimatorController = iceController;
                        break;
                    case 0:
                        ice1.SetActive(false);
                        fire1.SetActive(true);

                        pM.iceBoots = false;
                        pM.rocketBoots = true;

                        fire2.SetActive(false);
                        ice2.SetActive(true);

                        pM.fireBullet = false;
                        pM.iceBullet = true;
                        
                        rG.gunSprite.sprite = rG.iceSprite;
                        
                        pM.animator.runtimeAnimatorController = fireController;
                        break;
                }
            }
            
            else if (pM.Fire && pM.Ice)
            {
                if (_audio != null) _audio.Play("ChangeItem");

                switch (uClickCount % 2)
                {
                    case 1:
                        fire1.SetActive(false);
                        ice1.SetActive(true);

                        pM.rocketBoots = false;
                        pM.iceBoots = true;
                        
                        pM.animator.runtimeAnimatorController = iceController;

                        break;
                    case 0:
                        ice1.SetActive(false);
                        fire1.SetActive(true);

                        pM.iceBoots = false;
                        pM.rocketBoots = true;
                        
                        pM.animator.runtimeAnimatorController = fireController;

                        break;
                }
            }
        }
    }
}
