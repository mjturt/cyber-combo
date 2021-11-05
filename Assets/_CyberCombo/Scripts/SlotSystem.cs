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

    private int uClickCount;
    private int dClickCount = 1;
    
    void Start()
    {
        imgs = GetComponentsInChildren<Image>();
        uImg1 = imgs[2];
        uImg2 = imgs[3];
        uImg3 = imgs[4];
        dImg1 = imgs[9];
        dImg2 = imgs[10];
        dImg3 = imgs[11];

        //pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();
        pM = GameObject.Find("/Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {

        //Upper row Logic
        if (Input.GetKeyDown(KeyCode.Q))
        {
            uClickCount++;
            
            

            if (pM.Fire && pM.Ice && pM.Magnet)
            {
                //Skip element if its already assigned to other slot
                if (uClickCount % 3 == dClickCount % 3)
                {
                    uClickCount++;
                }
                switch (uClickCount % 3)
                {
                    case 0:
                        uImg1.enabled = true;
                        uImg2.enabled = false;
                        uImg3.enabled = false;

                        pM.rocketBoots = false;
                        break;
                    case 1:
                        uImg1.enabled = false;
                        uImg2.enabled = true;
                        uImg3.enabled = false;

                        pM.rocketBoots = false;
                        break;
                    case 2:
                        uImg1.enabled = false;
                        uImg2.enabled = false;
                        uImg3.enabled = true;

                        pM.rocketBoots = true;
                        break;
                }
            }
            
            else if (pM.Gun)
            {
                switch (uClickCount % 2)
                {
                    case 1:
                        uImg1.enabled = true;
                        uImg3.enabled = false;

                        dImg1.enabled = false;
                        dImg3.enabled = true;

                        pM.rocketBoots = false;
                        pM.iceBoots = true;
                        break;
                    case 0:
                        uImg1.enabled = false;
                        uImg3.enabled = true;

                        dImg1.enabled = true;
                        dImg3.enabled = false;

                        pM.rocketBoots = true;
                        pM.iceBoots = false;
                        break;
                }
            }
            
            else if (pM.Fire && pM.Ice)
            {

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            dClickCount++;

            if (pM.Fire && pM.Ice && pM.Magnet)
            {
                //Skip element if its already assigned to other slot
                if (dClickCount % 3 == uClickCount % 3)
                {
                    dClickCount++;
                }

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
