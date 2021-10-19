using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SlotSystem : MonoBehaviour
{
    private Image[] imgs;
    private Image uImg1;
    private Image uImg2;
    private Image uImg3;
    private Image dImg1;
    private Image dImg2;
    private Image dImg3;

    public GameObject player;
    private bool rocketBoots;

    private int uClickCount;
    private int dClickCount = 1;
    
    void Start()
    {
        imgs = GetComponentsInChildren<Image>();
        uImg1 = imgs[2];
        uImg2 = imgs[3];
        uImg3 = imgs[4];
        dImg1 = imgs[8];
        dImg2 = imgs[9];
        dImg3 = imgs[10];

        rocketBoots = player.GetComponent<PlayerMovement>().rocketBoots;

    }

    // Update is called once per frame
    void Update()
    {
        //Upper row Logic
        if (Input.GetKeyDown(KeyCode.Q))
        {
            uClickCount++;
            
            //Skip element if its already assigned to other slot
            if (uClickCount % 3 == dClickCount % 3)
            {
                uClickCount++;
            }
            
            switch (uClickCount%3)
            {
                case 0:
                    uImg1.enabled = true;
                    uImg2.enabled = false;
                    uImg3.enabled = false;

                    player.GetComponent<PlayerMovement>().rocketBoots = false;
                    break;
                case 1:
                    uImg1.enabled = false;
                    uImg2.enabled = true;
                    uImg3.enabled = false;

                    player.GetComponent<PlayerMovement>().rocketBoots = false;
                    break;
                case 2:
                    uImg1.enabled = false;
                    uImg2.enabled = false;
                    uImg3.enabled = true;

                    player.GetComponent<PlayerMovement>().rocketBoots = true;
                    break;
            }
        }
        //Second row logic
        if (Input.GetKeyDown(KeyCode.E))
        {
            dClickCount++;
            
            //Skip element if its already assigned to other slot
            if (dClickCount % 3 == uClickCount % 3)
            {
                dClickCount++;
            }
            switch (dClickCount%3)
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
