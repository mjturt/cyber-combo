using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{

    public GameObject completionScreen;
    
    public void LevelCompleted()
    {
        completionScreen.SetActive(true);
    }
}
