using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManager : MonoBehaviour
{

    public GameObject lightningEffectPrefab;
    public float strikeInterval = 5f;
    public float fadeDuration = 1.5f;
    public AudioSource lightningAudio;

    private GameObject currentStrike;

    void Start()
    {
        StartCoroutine(StrikeRoutine());
        StartCoroutine(PlayLightningSoundRoutine());
    }

    IEnumerator StrikeRoutine()
    {
        while (true)
        {
            Vector3 strikePosition = GetRandomPosition();
            currentStrike = Instantiate(lightningEffectPrefab, strikePosition, Quaternion.identity);

            // Randomly enable point light
            Light light = currentStrike.GetComponentInChildren<Light>();
            if (light != null)
            {
                StartCoroutine(BlinkLight(light));
            }


            SetAlpha(currentStrike, 0f);
            yield return FadeIn(currentStrike);
            yield return new WaitForSeconds(3f);
            yield return FadeOut(currentStrike);
            Destroy(currentStrike);

            yield return new WaitForSeconds(strikeInterval);
        }
    }

    IEnumerator PlayLightningSoundRoutine()
    {
        while (true)
        {
            lightningAudio?.Play();
            yield return new WaitForSeconds(18f);
        }
    }

    void SetAlpha(GameObject obj, float alpha)
    {
        var renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            foreach (var mat in r.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color c = mat.color;
                    c.a = alpha;
                    mat.color = c;
                    mat.SetFloat("_Mode", 2);
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;
                }
            }
        }
    }

    IEnumerator FadeIn(GameObject obj)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            SetAlpha(obj, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        SetAlpha(obj, 1f);
    }

    IEnumerator FadeOut(GameObject obj)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            SetAlpha(obj, 1f - (time / fadeDuration));
            time += Time.deltaTime;
            yield return null;
        }
        SetAlpha(obj, 0f);
    }

    IEnumerator BlinkLight(Light light)
    {
        light.enabled = true;
        yield return new WaitForSeconds(3f);

        light.enabled = false;
        yield return new WaitForSeconds(3f);
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-50f, 50f); // -50 + 100 = 50
        float z = Random.Range(-50f, 50f); // -50 + 100 = 50
        float y = 0f; // assuming lightning strikes ground level

        return new Vector3(x, y, z);
    }

}