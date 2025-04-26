using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

   public void QuitGame()
    {
        Application.Quit();
    }
}
