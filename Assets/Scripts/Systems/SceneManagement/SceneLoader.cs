using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private string currentSceneName;

    public string CurrentSceneName => currentSceneName;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
