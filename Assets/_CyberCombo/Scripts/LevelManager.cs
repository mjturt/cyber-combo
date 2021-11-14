using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public GameObject pauseScreen;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeLevel();
        }
    }

    public void ChangeLevel(int level)
    {
        SceneManager.LoadScene("Level " + level);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Main menu");
    }

    public void ResumeLevel()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

}
