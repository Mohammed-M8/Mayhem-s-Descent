using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource sfxSource;
    private AudioSource musicSource;
    private float currentMusicVolume = 1f;
    private float currentSFXVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

    }
    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip, Mathf.Clamp01(volume));
    }


    public void PlayMusic(AudioClip music, float fadeDuration = 0f)
    {
        if (music == null || (musicSource.clip == music && musicSource.isPlaying)) return;

        StopAllCoroutines();
        StartCoroutine(FadeInNewMusic(music, fadeDuration));
    }


    public void StopMusic()
    {
        musicSource.Stop();
    }


    public void SetMusicVolume(float volume)
    {
        currentMusicVolume = Mathf.Clamp01(volume);
        musicSource.volume = currentMusicVolume;
        Debug.Log("Set music volume to: " + currentMusicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        currentSFXVolume = Mathf.Clamp01(volume);
        sfxSource.volume = currentSFXVolume;
        Debug.Log("Set SFX volume to: " + currentSFXVolume);
    }


    private IEnumerator FadeInNewMusic(AudioClip newClip, float duration)
    {
        float startVolume = 0f;
        float currentTime = 0f;

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.volume = 0f;
        musicSource.Play();

        // Fade in to the CURRENT saved volume
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, currentMusicVolume, currentTime / duration);
            yield return null;
        }

        musicSource.volume = currentMusicVolume;
    }

}