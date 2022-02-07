using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBox : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private GameObject continueIcon;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Time.timeScale);
        Time.timeScale = 0f;
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        continueIcon = transform.GetChild(2).gameObject;
        if (textComponent.textInfo.pageCount == 1)
            continueIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
        {
            if (textComponent.pageToDisplay < textComponent.textInfo.pageCount)
            {
                textComponent.pageToDisplay++;
                if (textComponent.pageToDisplay == textComponent.textInfo.pageCount)
                    transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 1f;
                this.gameObject.SetActive(false);
            }
        }
    }
}
