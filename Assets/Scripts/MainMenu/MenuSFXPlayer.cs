using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSFXPlayer : MonoBehaviour
{
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        if (SoundManager.Instance != null && clickSound != null)
        {
            SoundManager.Instance.PlaySound(clickSound);
        }
    }
}