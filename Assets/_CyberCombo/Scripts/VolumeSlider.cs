using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
    public Slider volumeSlider;
    void changeVolume(float vol) {
        FindObjectOfType<AudioManager>().SetMasterVolume(vol);
    }
    void OnEnable() {
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(volumeSlider.value); });
    }
}
