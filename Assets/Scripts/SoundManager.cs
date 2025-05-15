using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource sfxSource;   
    private AudioSource musicSource; 

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
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }

    private IEnumerator FadeInNewMusic(AudioClip newClip, float duration)
    {
        float startVolume = musicSource.volume;
        float currentTime = 0f;

        // Fade out
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in
        currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, startVolume, currentTime / duration);
            yield return null;
        }

        musicSource.volume = startVolume; // ensure exact reset
    }

}

