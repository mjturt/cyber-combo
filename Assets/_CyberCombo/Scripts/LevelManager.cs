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
        if (GameObject.Find("DialogueDisabler"))
        {
            Destroy(GameObject.Find("DialogueDisabler"));
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("Level " + level);
        AudioManager _audio = FindObjectOfType<AudioManager>();
        if (level == 0 || level == 13)
        {
            if (null != _audio) _audio.ChangeSong("MenuMusic");
        }
        else if (level == 12)
        {
            if (null != _audio) _audio.ChangeSong("BossMusic");
        } 
        else
        {
            if (null != _audio) _audio.ChangeSong("LevelMusic");
        }
        if (null != _audio) _audio.Play("StartLevel");
    }

    public void MenuScene()
    {
        if (GameObject.Find("DialogueDisabler"))
        {
            Destroy(GameObject.Find("DialogueDisabler"));
        }

        Time.timeScale = 1f;
        AudioManager _audio = FindObjectOfType<AudioManager>();
        SceneManager.LoadScene("Main menu");
        if (null != _audio) _audio.ChangeSong("MenuMusic");
    }

    public void ResumeLevel()
    {
        if (Time.timeScale == 1f)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;

        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }

}
