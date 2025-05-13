using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public GameObject confirmPanel;

    public void ShowConfirmPanel()
    {
        confirmPanel.SetActive(true);
    }

    public void ConfirmReturnToMenu()
    {
        Time.timeScale = 1f; // just in case the game is paused
        SceneManager.LoadScene("MainMenu"); // replace with your actual main menu scene name
    }

    public void CancelReturn()
    {
        confirmPanel.SetActive(false);
    }
}