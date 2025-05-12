using System.Collections;
using UnityEngine;

public class LightningStrikeManager : MonoBehaviour
{
    public GameObject lightningEffectPrefab;
    public float strikeInterval = 8f;
    public float fadeDuration = 2f;
    public AudioSource lightningAudio; // Assign sound source in Inspector

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
            GameObject strike = Instantiate(lightningEffectPrefab, strikePosition, Quaternion.identity);

            // Occasionally enable light
            Light lightFlash = strike.GetComponentInChildren<Light>();
            if (lightFlash != null)
                lightFlash.enabled = (Random.value > 0.5f); // 50% chance

            SetAlpha(strike, 0f);
            yield return FadeIn(strike);
            yield return new WaitForSeconds(3f);
            yield return FadeOut(strike);
            Destroy(strike);

            yield return new WaitForSeconds(strikeInterval);
        }
    }


    IEnumerator PlayLightningSoundRoutine()
    {
        while (true)
        {
            lightningAudio?.Play();
            yield return new WaitForSeconds(13f);
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
                    mat.SetFloat("_Mode", 2); // Transparent
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
            float alpha = time / fadeDuration;
            SetAlpha(obj, alpha);
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
            float alpha = 1f - (time / fadeDuration);
            SetAlpha(obj, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        SetAlpha(obj, 0f);
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)); // Adjust range as needed
    }
}
