using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ChangeLevel(int level)
    {
        SceneManager.LoadScene("Level " + level);
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Main menu");
    }

}
