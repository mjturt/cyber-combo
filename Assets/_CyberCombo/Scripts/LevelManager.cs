using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Lvl1()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void Lvl2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Main menu");
    }

}
