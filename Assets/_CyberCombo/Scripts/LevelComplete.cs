using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    public GameObject completionScreen;
    
    public void LevelCompleted()
    {
        completionScreen.SetActive(true);
        Time.timeScale = 0f;

        AudioManager _audio = FindObjectOfType<AudioManager>();
        if (null != _audio) _audio.Play("LevelComplete");
        // Save progress if necessary
        int highestLevel = PlayerPrefs.GetInt("HighestLevel");
        string scene = SceneManager.GetActiveScene().name;
        int currentLevel = int.Parse(scene.Substring(6));
        
        if (highestLevel <= currentLevel && currentLevel < 13) // We will have 13 levels
            PlayerPrefs.SetInt("HighestLevel", currentLevel + 1);
    }
}
