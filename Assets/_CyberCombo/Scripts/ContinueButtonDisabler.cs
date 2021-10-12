using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Disable continue button if there is no saved progress
public class ContinueButtonDisabler : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("HighestLevel") < 1)
            this.gameObject.GetComponent<Button>().interactable = false;
    }
}
