using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleporter : MonoBehaviour
{
    /// <summary>
    /// Once the player touches this then start the coroutine
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(SceneLoadDelay());
    }

    /// <summary>
    /// Delays the loading of the next scene
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneLoadDelay()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        LoadNextScene();
    }

    /// <summary>
    /// Loads the next scene
    /// </summary>
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    /// <summary>
    /// Loads the scene with the index paramter
    /// </summary>
    /// <param name="levelIndex"></param>
    public void LoadSceneIndex(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    /// <summary>
    /// Reload the scene
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Quit the application
    /// </summary>
    public void QuitApp()
    {
        Application.Quit();
    }
}
