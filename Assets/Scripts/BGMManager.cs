using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip bgmClip;
    public float fadeDuration = 1f;

    void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMusic(bgmClip,fadeDuration);
        }
    }
}
