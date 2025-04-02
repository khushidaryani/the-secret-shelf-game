using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void HowToPlayGame()
    {
        SceneManager.LoadScene("How_To_Play");
    }

    public void QuitGame()
    {
        // Cierra la aplicación en un build real 
        Application.Quit();

        // Detiene la ejecución del juego en el editor
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
