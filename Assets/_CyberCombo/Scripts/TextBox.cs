using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBox : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private GameObject continueIcon;
    private bool initialized;

    // Needed for textComponent to initialize before our initialization
    private void LateInitialize()
    {
        Time.timeScale = 0f;
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        continueIcon = transform.GetChild(2).gameObject;
        if (textComponent.textInfo.pageCount > 1)
            continueIcon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
        {
            Invoke("LateInitialize", 0.01f);
            initialized = true;
        }
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.pageToDisplay < textComponent.textInfo.pageCount)
            {
                textComponent.pageToDisplay++;
                if (textComponent.pageToDisplay == textComponent.textInfo.pageCount)
                    continueIcon.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
                this.gameObject.SetActive(false);
            }
        }
    }
}
