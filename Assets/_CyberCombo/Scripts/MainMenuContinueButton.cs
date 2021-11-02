using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Disable continue button if there is no saved progress
public class MainMenuContinueButton : MonoBehaviour
{
    private int highestLevel;

    void Start()
    {
        highestLevel = PlayerPrefs.GetInt("HighestLevel");
        if (highestLevel < 1)
            this.gameObject.GetComponent<Button>().interactable = false;
    }

    public void Activate()
    {
        SceneManager.LoadScene("Level " + highestLevel);
    }
}
