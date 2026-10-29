using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void ChangeScene(Scene scene)
    {
        LoadScene(scene.name);
    }

    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
        {
            Debug.LogError("Scene name cannot be empty.");
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentScene()
    {
        Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        LoadScene(activeScene.name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
