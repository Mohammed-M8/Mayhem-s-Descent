using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeWarningText : MonoBehaviour
{
    public TMP_Text warningText;
    public float duration = 2f;

    private Coroutine hideRoutine;

    void Start()
    {
        if (warningText != null)
            warningText.gameObject.SetActive(false); // Ensures it's hidden at start
    }

    public void Show()
    {
        if (warningText == null)
        {
            Debug.LogWarning("WarningText reference is missing!");
            return;
        }

        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        warningText.gameObject.SetActive(true);           // show it
        warningText.text = "Not enough points!";          // update text if needed
        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSecondsRealtime(duration);
        warningText.gameObject.SetActive(false);          // hide again
    }
}
