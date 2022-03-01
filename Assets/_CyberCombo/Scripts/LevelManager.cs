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
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level " + level);
        AudioManager _audio = FindObjectOfType<AudioManager>();
        if (level == 9) {
            if (null != _audio) _audio.ChangeSong("BossMusic");
        } else {
            if (null != _audio) _audio.ChangeSong("LevelMusic");
        }
        if (null != _audio) _audio.Play("StartLevel");
    }

    public void MenuScene()
    {
        Time.timeScale = 1f;
        AudioManager _audio = FindObjectOfType<AudioManager>();
        SceneManager.LoadScene("Main menu");
        if (null != _audio) _audio.ChangeSong("MenuMusic");
    }

    public void ResumeLevel()
    {
        pauseScreen.SetActive(!pauseScreen.activeSelf);
    }

    public void DeleteSaves()
    {
        PlayerPrefs.DeleteAll();
    }

}
