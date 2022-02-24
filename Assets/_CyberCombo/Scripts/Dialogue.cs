using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    private TextBox[] textBoxes;
    private int currentTextBox;
    private LevelManager gameManager;

    public bool loadNextLevelAfterReading;
    public bool returnToMainMenuAfterReading;

    // Start is called before the first frame update
    void Start()
    {
        currentTextBox = 0;
        textBoxes = this.GetComponentsInChildren<TextBox>(true);
        textBoxes[0].gameObject.SetActive(true);
        for (int i = 1; i < textBoxes.Length; i++)
        {
            textBoxes[i].gameObject.SetActive(false);
        }

        gameManager = Resources.FindObjectsOfTypeAll<LevelManager>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (textBoxes.Length == currentTextBox) // Change level or continue game
        {
            if (loadNextLevelAfterReading)
            {
                gameManager.ChangeLevel(int.Parse(SceneManager.GetActiveScene().name.Substring(6)) + 1);
            }
            else if (returnToMainMenuAfterReading)
            {
                gameManager.MenuScene();
            }
        }
        else if (textBoxes[currentTextBox].gameObject.activeInHierarchy == false) // next textbox
        {
            currentTextBox++;
            if (textBoxes.Length > currentTextBox)
                textBoxes[currentTextBox].gameObject.SetActive(true);
        }
    }
}
