using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public TMP_Dropdown screenModeDropdown;
    public Slider masterVol, musicVol, SFXVol;
    public AudioMixer mainAudioMixer;

    public void ChangeScreenMode(int modeIndex)
    {
        switch (modeIndex)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case 1: Screen.SetResolution(1920, 1080, FullScreenMode.Windowed); break;
            case 2: Screen.SetResolution(1280, 720, FullScreenMode.Windowed); break;
        }
    }

    public void ChangeMasterVolume()
    {
        mainAudioMixer.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        mainAudioMixer.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSFXVolume()
    {
        mainAudioMixer.SetFloat("SFXVol", SFXVol.value);
    }

    void Start()
    {
        // Optional: Auto-select current mode
        screenModeDropdown.value = GetCurrentModeIndex();
        screenModeDropdown.onValueChanged.AddListener(ChangeScreenMode);
    }

    int GetCurrentModeIndex()
    {
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.FullScreenWindow: return 0;
            case FullScreenMode.Windowed: return 1;
            case FullScreenMode.MaximizedWindow: return 2;
            default: return 0;
        }
    }

    void Update() { }
}
