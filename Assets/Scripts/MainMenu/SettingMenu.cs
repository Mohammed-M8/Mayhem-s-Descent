using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingMenu : MonoBehaviour
{
    public TMP_Dropdown screenModeDropdown;
    public Slider masterVol, musicVol, SFXVol;

    void Start()
    {
        // Dropdown logic
        screenModeDropdown.value = GetCurrentModeIndex();
        screenModeDropdown.onValueChanged.AddListener(ChangeScreenMode);

        // Load saved values
        float savedMaster = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float savedMusic = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float savedSFX = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterVol.value = savedMaster;
        musicVol.value = savedMusic;
        SFXVol.value = savedSFX;

        SoundManager.Instance.SetMusicVolume(savedMusic);
        SoundManager.Instance.SetSFXVolume(savedSFX);

        // Hook slider events
        masterVol.onValueChanged.AddListener(ChangeMasterVolume);
        musicVol.onValueChanged.AddListener(ChangeMusicVolume);
        SFXVol.onValueChanged.AddListener(ChangeSFXVolume);
    }
    public void ChangeScreenMode(int modeIndex)
    {
        switch (modeIndex)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case 1: Screen.SetResolution(1920, 1080, FullScreenMode.Windowed); break;
            case 2: Screen.SetResolution(1280, 720, FullScreenMode.Windowed); break;
        }
    }

    public void ChangeMasterVolume(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
        SoundManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void ChangeMusicVolume(float value)
    {
        SoundManager.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ChangeSFXVolume(float value)
    {
        SoundManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
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
}