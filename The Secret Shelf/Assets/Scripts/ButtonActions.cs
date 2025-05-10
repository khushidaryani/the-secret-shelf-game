using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UIElements;
using UnityEditor;

public class ButtonActions : MonoBehaviour
{
    public GameObject pausePanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("PlayerSignUp");
    }

    public void HowToPlayGame()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        StartCoroutine(DeleteAndQuit());

    }
    public void ReturnToMainMenu()
    {
        StartCoroutine(DeleteAndQuit());
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator DeleteAndQuit()
    {
        if (!string.IsNullOrEmpty(PlayerCleanup.Instance.playerName))
        {
            yield return StartCoroutine(PlayerCleanup.Instance.DeletePlayerFromDatabase(PlayerCleanup.Instance.playerName));
        }

        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
