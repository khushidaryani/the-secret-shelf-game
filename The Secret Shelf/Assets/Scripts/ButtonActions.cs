using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

    public void ExitGame()
    {
        if (!string.IsNullOrEmpty(PlayerCleanup.Instance.playerName))
        {
            // Llamar al método para eliminar al jugador de la base de datos
            StartCoroutine(PlayerCleanup.Instance.DeletePlayerFromDatabase(PlayerCleanup.Instance.playerName));
            Debug.Log("Jugador eliminado de la base de datos");
        }

        // Cierra la aplicación en un build real 
        Application.Quit();

        // Detiene la ejecución del juego en el editor
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void ReturnToMainMenu()
    {
        if (!string.IsNullOrEmpty(PlayerCleanup.Instance.playerName))
        {
            // Llamar al método para eliminar al jugador de la base de datos
            StartCoroutine(PlayerCleanup.Instance.DeletePlayerFromDatabase(PlayerCleanup.Instance.playerName));
            Debug.Log("Jugador eliminado de la base de datos");
        }

        SceneManager.LoadScene("MainMenu");
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
