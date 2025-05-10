using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

// Handles user sign-up: checks if a player exists and creates one if not
public class SignUpManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextMeshProUGUI errorMessage;
    public Button startButton;

    private void Start()
    {
        errorMessage.gameObject.SetActive(false);
        startButton.onClick.AddListener(() => StartCoroutine(HandleSignUp()));
    }

    IEnumerator HandleSignUp()
    {
        string playerName = nameInputField.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            ShowError("Please enter a name!");
            yield break;
        }

        startButton.interactable = false;
        ShowError(""); // Clears previous error messages

        // Step 1: Check if player already exists
        yield return StartCoroutine(CheckPlayerExists(playerName, exists =>
        {
            if (exists)
            {
                ShowError("This name is already taken, try again!");
                startButton.interactable = true;
            }
        }));

        // If an error message was set during the check, abort the creation of a new player
        if (!string.IsNullOrEmpty(errorMessage.text))
            yield break;

        // Step 2: Create the new player
        yield return StartCoroutine(CreateNewPlayer(playerName));
    }

    // Sends GET request to check if a player already exists in the database
    IEnumerator CheckPlayerExists(string playerName, System.Action<bool> callback)
    {
        string url = Endpoints.PlayerByName(playerName);
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success && request.responseCode != 404)
        {
            Debug.LogError("Error checking player: " + request.error);
            ShowError("Connection error, try again!");
            callback(false);
            yield break;
        }

        // If status 200, player exists
        callback(request.responseCode == 200);
    }

    // Sends POST request to create a new player with initial score.
    IEnumerator CreateNewPlayer(string playerName)
    {
        string jsonData = $"{{\"name\":\"{playerName}\",\"points\":0}}";
        UnityWebRequest request = new UnityWebRequest(Endpoints.playerUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error creating player: " + request.error);
            ShowError("Failed to create player :(");
            startButton.interactable = true;
        }
        else
        {
            // Store the player name in PlayerCleanup so we can delete it later
            if (PlayerCleanup.Instance != null)
                PlayerCleanup.Instance.playerName = playerName;

            SceneManager.LoadScene("ClientScene1");
        }
    }

    // Displays an error message on the screen
    void ShowError(string message)
    {
        errorMessage.text = message;
        errorMessage.gameObject.SetActive(!string.IsNullOrEmpty(message));
    }
}