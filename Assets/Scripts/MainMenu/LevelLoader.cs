using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int levelIndex)
    {
        //SceneManager.LoadScene(sceneName);
        SceneManager.LoadScene(levelIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
