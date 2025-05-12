using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public PlayerMove playerMove;
    public Combat combatScript;
    public Aim aimScript;

    public static bool IsPaused { get; private set; } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);

        if (playerMove != null) playerMove.isPaused = true;
        if (combatScript != null) combatScript.isPaused = true;
        if (aimScript != null) aimScript.isPaused = true;
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);

        if (playerMove != null) playerMove.isPaused = false;
        if (combatScript != null) combatScript.isPaused = false;
        if (aimScript != null) aimScript.isPaused = false;
    }
}
