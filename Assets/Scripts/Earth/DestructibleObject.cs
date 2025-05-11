using System.Collections;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [Header("Destructible Settings")]
    [Tooltip("Number of hits required before this object breaks")]
    [SerializeField] private int hitsToBreak = 2;

    [Tooltip("Chance [0–1] to spawn a health pack when breaking")]
    [SerializeField] [Range(0f, 1f)] private float healthPackSpawnChance = 0.5f;

    [Tooltip("Prefab of the health pack to spawn")]
    [SerializeField] private GameObject healthPackPrefab;

    [Header("Animation Settings")]
    [Tooltip("Animator with 'getHit' and 'die' triggers defined")]
    [SerializeField] private Animator animator;
    [Tooltip("Delay in seconds before the object is destroyed after 'die' animation")]
    [SerializeField] private float destroyDelay = 0.7f;

    [Header("Hit Feedback")]
    [Tooltip("Color used when flashing on hit")]
    [SerializeField] private Color flashColor = Color.white;
    [Tooltip("Multiplier for emission intensity during flash")]
    [SerializeField] private float flashIntensity = 2f;
    [Tooltip("Duration of the flash in seconds")]
    [SerializeField] private float flashDuration = 0.1f;

    // inside DestructibleObject class
    [HideInInspector] public int spawnIndex;


    private int hitCount;

    // Renderers and original colors
    private Renderer[] renderers;
    private Color[] originalColors;

    void Awake()
    {
        // Cache renderers and store original colors
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }

    /// <summary>
    /// Called when the object is hit by the player weapon.
    /// </summary>
    public void TakeHit()
    {
        hitCount++;

        if (hitCount < hitsToBreak)
        {
            animator?.SetTrigger("getHit");
            StartCoroutine(FlashWhite());
        }
        else
        {
            animator?.SetTrigger("die");
            StartCoroutine(BreakRoutine());
        }
    }

    private IEnumerator FlashWhite()
    {
        // Apply flash color and emission
        for (int i = 0; i < renderers.Length; i++)
        {
            var mat = renderers[i].material;
            mat.color = flashColor;
            if (mat.HasProperty("_EmissionColor"))
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", flashColor * flashIntensity);
            }
        }

        yield return new WaitForSeconds(flashDuration);

        // Restore original colors and remove emission
        for (int i = 0; i < renderers.Length; i++)
        {
            var mat = renderers[i].material;
            mat.color = originalColors[i];
            if (mat.HasProperty("_EmissionColor"))
            {
                mat.SetColor("_EmissionColor", Color.black);
            }
        }
    }

    private IEnumerator BreakRoutine()
    {

        yield return new WaitForSeconds(destroyDelay);

        // at the start of BreakRoutine, after animation delay:
        if (ChestSpawner.Instance != null)
        {
            ChestSpawner.Instance.NotifyChestDestroyed(spawnIndex);
        }


        if (Random.value <= healthPackSpawnChance && healthPackPrefab != null)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.y = 1f;
            Instantiate(healthPackPrefab, spawnPos, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
            TakeHit();
    }
}
