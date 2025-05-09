using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class BookButton : MonoBehaviour
{
    public TextMeshProUGUI bookDetailsText;
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;

    private Book book;
    private int totalAttempts = 3;
    private int attempts = 0;
    private bool alreadyGuessedCorrectly = false;

    public GameObject finishPanel;
    public TextMeshProUGUI finishText;

    void OnEnable()
    {
        // Reset state for each client
        attempts = 0;
        alreadyGuessedCorrectly = false;

        // Load current coins from the server to update UI
        FindAnyObjectByType<PlayerCoins>()?.LoadCoinsFromServer();
    }

    public void SetBook(Book bookData)
    {
        book = bookData;

        if (bookDetailsText != null)
        {
            bookDetailsText.text = $"Title: {book.title}\nAuthor: {book.author}\nGenre: {book.genre}";
        }

        var button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        attempts = 0;
        alreadyGuessedCorrectly = false;

        // Debug the attempt count
        attempts++;
        Debug.Log($"[BookButton] Attempt #{attempts}");

        string currentHint = DialogueManager.lastLine?.dialogueText;
        if (string.IsNullOrEmpty(currentHint))
        {
            Debug.LogWarning("No hint available for comparison");
            return;
        }

        bool isCorrect = false;
        foreach (string hint in book.hints)
        {
            if (hint == currentHint)
            {
                isCorrect = true;
                break;
            }
        }

        if (isCorrect)
        {
            alreadyGuessedCorrectly = true;

            // Assign points based on the attempt number
            int coinsToAdd = attempts == 1 ? 10 : attempts == 2 ? 5 : 2;
            ShowAndSendResult(true, coinsToAdd);
        }
        else if (attempts >= 3)
        {
            ShowAndSendResult(false, -5);
        }
        else
        {
            int remaining = totalAttempts - attempts;
            ShowMessage($"Wrong book! You have {remaining} more attempt{(remaining == 1 ? "" : "s")}!");
        }
    }

    private void ShowAndSendResult(bool correct, int coinChange)
    {
        if (correct)
        {
            ShowMessage($"Correct book! You have earned {coinChange} coins!");
        }
        else
        {
            ShowMessage("Wrong book! You lose 5 coins.");
        }

        UpdateCoinsOnServer(coinChange);
        DisableAllBookButtons();
        StartCoroutine(LoadNextClientScene(2f));
    }

    void DisableAllBookButtons()
    {
        BookButton[] allButtons = FindObjectsByType<BookButton>(FindObjectsSortMode.None);
        foreach (BookButton bookButton in allButtons)
        {
            var btn = bookButton.GetComponent<Button>();
            if (btn != null)
                btn.interactable = false;
        }
    }

    IEnumerator LoadNextClientScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        string currentScene = SceneManager.GetActiveScene().name;
        string prefix = new string(currentScene.TakeWhile(char.IsLetter).ToArray());
        string numberPart = new string(currentScene.SkipWhile(c => !char.IsDigit(c)).ToArray());

        if (int.TryParse(numberPart, out int sceneNumber))
        {
            string nextSceneName = prefix + (sceneNumber + 1);
            if (SceneExists(nextSceneName))
                SceneManager.LoadScene(nextSceneName);
            else
                ShowFinalMessage("Felicidades, has terminado el juego. ¡Gracias por jugar!");
        }
        else
        {
            Debug.LogError($"Could not parse scene number from name: {currentScene}");
        }
    }

    bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            if (System.IO.Path.GetFileNameWithoutExtension(path) == sceneName)
                return true;
        }
        return false;
    }

    void ShowFinalMessage(string message)
    {
        if (finishPanel != null && finishText != null)
        {
            finishText.text = message;
            finishPanel.SetActive(true);
            Invoke(nameof(ReturnToMenu), 3f);
        }
        else
        {
            Debug.LogWarning("Finish panel or text not assigned.");
        }
    }

    void ReturnToMenu() => SceneManager.LoadScene("MainMenu");

    void ShowMessage(string message)
    {
        if (messagePanel != null && messageText != null)
        {
            messageText.text = message;
            messagePanel.SetActive(true);
            CancelInvoke(nameof(HideMessage));
            Invoke(nameof(HideMessage), 3f);
        }
    }

    void HideMessage() => messagePanel.SetActive(false);

    void UpdateCoinsOnServer(int amount)
    {
        if (PlayerCleanup.Instance == null || string.IsNullOrEmpty(PlayerCleanup.Instance.playerName))
        {
            Debug.LogError("No player name available.");
            return;
        }

        string url = $"{Endpoints.BaseUrl}/players/{PlayerCleanup.Instance.playerName}/coins";
        StartCoroutine(SendCoinUpdateRequest(url, amount));
    }

    IEnumerator SendCoinUpdateRequest(string url, int coinsChange)
    {
        string jsonData = $"{{\"coinsChange\":{coinsChange}}}";
        var request = new UnityWebRequest(url, "PATCH");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError("Failed to update coins: " + request.error);
        else
        {
            Debug.Log("Coins updated on server.");
            FindAnyObjectByType<PlayerCoins>()?.LoadCoinsFromServer();
        }
    }
}
