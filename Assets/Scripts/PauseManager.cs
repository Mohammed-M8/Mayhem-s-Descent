using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Menus")]
    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;     // ← Drag your DeathMenu panel here
    public GameObject victoryMenuUI;   // ← Drag your VictoryMenu panel here

    [Header("Gameplay Scripts")]
    public PlayerMove playerMove;
    public Combat combatScript;
    public Aim aimScript;

    public static bool IsPaused { get; private set; }

    void Awake()
    {
        // Always start un-paused
        IsPaused = false;
        Time.timeScale = 1f;

        // Ensure all menus are hidden at start
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (deathMenuUI != null) deathMenuUI.SetActive(false);
        if (victoryMenuUI != null) victoryMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;

        if (pauseMenuUI != null) pauseMenuUI.SetActive(true);
        if (playerMove != null) playerMove.isPaused = true;
        if (combatScript != null) combatScript.isPaused = true;
        if (aimScript != null) aimScript.isPaused = true;
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;

        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);
        if (playerMove != null) playerMove.isPaused = false;
        if (combatScript != null) combatScript.isPaused = false;
        if (aimScript != null) aimScript.isPaused = false;
    }

    /// <summary>
    /// Call this when the player dies to show the Death screen.
    /// </summary>
    public void ShowDeathScreen()
    {
        Time.timeScale = 0f;
        if (deathMenuUI != null) deathMenuUI.SetActive(true);
    }

    /// <summary>
    /// Call this when the player wins to show the Victory screen.
    /// </summary>
    public void ShowVictoryScreen()
    {
        Time.timeScale = 0f;
        if (victoryMenuUI != null) victoryMenuUI.SetActive(true);
    }
}
