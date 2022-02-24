using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Disable the levels that have not been unlocked yet
public class LevelEnabler : MonoBehaviour
{
    public Color redLocked;
    public Color redUnlocked;
    public Color blueLocked;
    public Color blueUnlocked;
    public Color purpleLocked;
    public Color purpleUnlocked;
    public Color greenLocked;
    public Color greenUnlocked;

    public float lineWidthLocked;
    public float lineWidthUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        Button[] buttons = this.gameObject.GetComponentsInChildren<Button>();
        int highestLevel = PlayerPrefs.GetInt("HighestLevel"); // 0 if no saved progress
        
        // Deactivate buttons
        int levelIndex = 0; // Only count levelbuttons
        foreach (Button button in buttons)
        {
            if (button.transform.name.Contains("LevelButton"))
            {
                if (levelIndex > highestLevel)
                {
                    button.interactable = false;
                    if (levelIndex < 4)
                        button.GetComponent<Image>().color = redLocked;
                    else if (levelIndex < 7)
                        button.GetComponent<Image>().color = blueLocked;
                    else if (levelIndex < 10)
                        button.GetComponent<Image>().color = purpleLocked;
                    else
                        button.GetComponent<Image>().color = greenLocked;
                }
                else
                {
                    if (levelIndex < 4)
                        button.GetComponent<Image>().color = redUnlocked;
                    else if (levelIndex < 7)
                        button.GetComponent<Image>().color = blueUnlocked;
                    else if (levelIndex < 10)
                        button.GetComponent<Image>().color = purpleUnlocked;
                    else
                        button.GetComponent<Image>().color = greenUnlocked;
                }

                levelIndex++;
            }
        }

        int linesToActivate = highestLevel;
        if (highestLevel >= 4)
            linesToActivate++;
        if (highestLevel >= 7)
            linesToActivate++;
        if (highestLevel >= 10)
            linesToActivate++;

        int lineIndex = 0;
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Line"))
            {
                RectTransform rect = child.GetComponent<RectTransform>();

                if (lineIndex < linesToActivate)
                {
                    // Bold
                    if (rect.anchorMax.x - rect.anchorMin.x > rect.anchorMax.y - rect.anchorMin.y)
                    {
                        rect.anchorMin = new Vector2(rect.anchorMin.x, (rect.anchorMax.y + rect.anchorMin.y) / 2 - lineWidthUnlocked / 200);
                        rect.anchorMax = new Vector2(rect.anchorMax.x, (rect.anchorMax.y + rect.anchorMin.y) / 2 + lineWidthUnlocked / 200);
                    }
                    else
                    {
                        rect.anchorMin = new Vector2((rect.anchorMax.x + rect.anchorMin.x) / 2 - lineWidthUnlocked / 360, rect.anchorMin.y);
                        rect.anchorMax = new Vector2((rect.anchorMax.x + rect.anchorMin.x) / 2 + lineWidthUnlocked / 360, rect.anchorMax.y);
                    }

                    // Color
                    if (lineIndex < 4)
                        child.GetComponent<Image>().color = redUnlocked;
                    else if (lineIndex < 8)
                        child.GetComponent<Image>().color = blueUnlocked;
                    else if (lineIndex < 12)
                        child.GetComponent<Image>().color = purpleUnlocked;
                    else
                        child.GetComponent<Image>().color = greenUnlocked;
                }
                else
                {
                    if (rect.sizeDelta.x == 5)
                        rect.sizeDelta = new Vector2(lineWidthLocked, rect.sizeDelta.y);
                    else
                        rect.sizeDelta = new Vector2(rect.sizeDelta.x, lineWidthLocked);

                    if (lineIndex < 4)
                        child.GetComponent<Image>().color = redLocked;
                    else if (lineIndex < 8)
                        child.GetComponent<Image>().color = blueLocked;
                    else if (lineIndex < 12)
                        child.GetComponent<Image>().color = purpleLocked;
                    else
                        child.GetComponent<Image>().color = greenLocked;
                }

                lineIndex++;
            }
        }
    }
}
