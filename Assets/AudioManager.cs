using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public float masterVolume = .75f;
    public static AudioManager instance;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.volume = masterVolume * s.volume;
        }
        Play("MenuMusic");
    }

    public void Play (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public AudioSource GetSource (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source;
    }

    public void ChangeSong (string name) {
        foreach (Sound s in sounds) {
            s.source.Stop();
        }
        Play(name);
    }

    public void SetMasterVolume (float vol) {
        masterVolume = vol;
        foreach (Sound s in sounds) {
            s.source.volume = masterVolume * s.volume;
        }
    }
}
