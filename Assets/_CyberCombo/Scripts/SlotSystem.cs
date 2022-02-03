using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class SlotSystem : MonoBehaviour
{
    public Image[] imgs;
    private Image uImg1;
    private Image uImg2;
    private Image uImg3;
    private Image dImg1;
    private Image dImg2;
    private Image dImg3;

    private PlayerMovement pM;
    private RotateGun rG;

    public int uClickCount;
    public int dClickCount = 1;

    private AudioManager _audio;
    
    void Start()
    {
        imgs = GetComponentsInChildren<Image>();
        uImg1 = imgs[2];
        uImg2 = imgs[3];
        uImg3 = imgs[4];
        dImg1 = imgs[8];
        dImg2 = imgs[9];
        dImg3 = imgs[10];

        //pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        rG = GameObject.Find("/Player").GetComponentInChildren<RotateGun>();

        _audio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        //Upper row Logic
        if (Input.GetKeyDown(KeyCode.Q) && pM.onGround)
        {
            uClickCount++;
            
            

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
                        uImg1.enabled = false;
                        uImg2.enabled = false;
                        uImg3.enabled = true;

                        pM.rocketBoots = true;
                        pM.iceBoots = false;
                        pM.magnetBoots = false;

                        dImg1.enabled = true;
                        dImg2.enabled = false;
                        dImg3.enabled = false;

                        pM.iceBullet = true;
                        pM.magnetBullet = false;
                        pM.fireBullet = false;

                        rG.gunSprite.sprite = rG.iceSprite;
                        Debug.Log("ice");
                        break;
                    case 1:
                        uImg1.enabled = true;
                        uImg2.enabled = false;
                        uImg3.enabled = false;

                        pM.rocketBoots = false;
                        pM.iceBoots = true;
                        pM.magnetBoots = false;

                        dImg1.enabled = false;
                        dImg2.enabled = true;
                        dImg3.enabled = false;

                        pM.iceBullet = false;
                        pM.magnetBullet = true;
                        pM.fireBullet = false;
                        
                        rG.gunSprite.sprite = rG.magnetSprite;
                        break;
                    case 2:
                        uImg1.enabled = false;
                        uImg2.enabled = true;
                        uImg3.enabled = false;

                        pM.rocketBoots = false;
                        pM.iceBoots = false;
                        pM.magnetBoots = true;

                        dImg1.enabled = false;
                        dImg2.enabled = false;
                        dImg3.enabled = true;

                        pM.iceBullet = false;
                        pM.magnetBullet = false;
                        pM.fireBullet = true;
                        
                        rG.gunSprite.sprite = rG.fireSprite;
                        Debug.Log("fire");
                        break;
                }
            }
            
            else if (pM.Gun)
            {
                if (_audio != null) _audio.Play("ChangeItem");
                switch (uClickCount % 2)
                {
                    case 1:
                        uImg1.enabled = true;
                        uImg3.enabled = false;

                        dImg1.enabled = false;
                        dImg3.enabled = true;

                        pM.rocketBoots = false;
                        pM.iceBoots = true;

                        pM.fireBullet = true;
                        pM.iceBullet = false;
                        
                        rG.gunSprite.sprite = rG.fireSprite;
                        Debug.Log("fire");
                        break;
                    case 0:
                        uImg1.enabled = false;
                        uImg3.enabled = true;

                        dImg1.enabled = true;
                        dImg3.enabled = false;

                        pM.rocketBoots = true;
                        pM.iceBoots = false;

                        pM.fireBullet = false;
                        pM.iceBullet = true;
                        
                        rG.gunSprite.sprite = rG.iceSprite;
                        Debug.Log("ice");
                        break;
                }
            }
            
            else if (pM.Fire && pM.Ice)
            {
                if (_audio != null) _audio.Play("ChangeItem");

                switch (uClickCount % 2)
                {
                    case 1:
                        uImg1.enabled = true;
                        uImg3.enabled = false;

                        pM.rocketBoots = false;
                        pM.iceBoots = true;
                        break;
                    case 0:
                        uImg1.enabled = false;
                        uImg3.enabled = true;

                        pM.rocketBoots = true;
                        pM.iceBoots = false;
                        break;
                }
            }
        }
        //Second row logic
        if (Input.GetKeyDown(KeyCode.E) && pM.onGround)
        {
            dClickCount++;

            if (pM.Fire && pM.Ice && pM.Magnet)
            {
                //Skip element if its already assigned to other slot
                if (dClickCount % 3 == uClickCount % 3)
                {
                    dClickCount++;
                }

                if (_audio != null) _audio.Play("ChangeItem");
                switch (dClickCount % 3)
                {
                    case 0:
                        dImg1.enabled = true;
                        dImg2.enabled = false;
                        dImg3.enabled = false;
                        break;
                    case 1:
                        dImg1.enabled = false;
                        dImg2.enabled = true;
                        dImg3.enabled = false;
                        break;
                    case 2:
                        dImg1.enabled = false;
                        dImg2.enabled = false;
                        dImg3.enabled = true;
                        break;
                }
            }
        }
    }
}
