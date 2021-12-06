using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Disable the levels that have not been unlocked yet
public class LevelDisabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button[] buttons = this.gameObject.GetComponentsInChildren<Button>();
        // int highestLevel = PlayerPrefs.GetInt("HighestLevel"); // 0 if no saved progress
        int highestLevel = 9;
        
        // Disable levels for the end until a level that has been reached is encountered
        for (int i = buttons.Length - 2; i >= highestLevel; i--) // -1 for the back button
        {
            if (i != 0)
                buttons[i].interactable = false;
        }
    }
}
