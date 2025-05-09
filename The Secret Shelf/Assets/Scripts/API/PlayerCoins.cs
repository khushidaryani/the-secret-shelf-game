using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCoins : MonoBehaviour
{
    public TextMeshProUGUI coinsText;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        LoadCoinsFromServer();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Asegúrate de que se llama solo si este objeto sigue existiendo en la nueva escena
        LoadCoinsFromServer();
    }

    public void LoadCoinsFromServer()
    {
        if (PlayerCleanup.Instance != null && !string.IsNullOrEmpty(PlayerCleanup.Instance.playerName))
        {
            string playerName = PlayerCleanup.Instance.playerName;
            string url = Endpoints.PlayerByName(playerName);
            StartCoroutine(GetPlayerCoins(url));
        }
        else
        {
            Debug.LogWarning("Player name not found");
        }
    }

    IEnumerator GetPlayerCoins(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to fetch player coins: " + request.error);
        }
        else
        {
            Player player = JsonUtility.FromJson<Player>(request.downloadHandler.text);
            Debug.Log("Coins retrieved: " + player.coins);
            UpdateCoinsUI(player.coins);
        }
    }

    public void UpdateCoinsUI(int newCoins)
    {
        if (coinsText != null)
        {
            coinsText.text = newCoins.ToString();
            Debug.Log("Coins updated: " + newCoins);
        }
        else
        {
            Debug.LogError("coinsText is null. Check if it is assigned in the Inspector.");
        }
    }

    public int GetCurrentCoins()
    {
        if (int.TryParse(coinsText.text, out int coins))
        {
            return coins;
        }
        return 0;
    }
}
