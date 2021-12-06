using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{

    public void Death()
    {
        print("Die animation...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager _audio = FindObjectOfType<AudioManager>();
        if (null != _audio) _audio.Play("Death");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager _audio = FindObjectOfType<AudioManager>();
        if (null != _audio) _audio.Play("Reset");
    }
}
