using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonCommands : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR

        EditorApplication.isPlaying = false;
    #else

        Application.Quit();

    #endif
    }

}
