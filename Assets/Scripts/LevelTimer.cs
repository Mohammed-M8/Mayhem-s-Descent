using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public TMP_Text timerText;
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
