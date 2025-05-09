using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerCleanup : MonoBehaviour
{
    // Singleton instance to persist across scenes
    public static PlayerCleanup Instance { get; private set; }

    // The name of the current player to delete on exit
    [HideInInspector]
    public string playerName;

    private void Awake()
    {
        // Prevent duplicates on scene reload
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /* Called when the application quits (on build)
    private void OnApplicationQuit()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            StartCoroutine(DeletePlayerFromDatabase(playerName));
        }
    }

#if UNITY_EDITOR
    // Handles cleanup when exiting Play Mode in the Unity Editor
    [UnityEditor.Callbacks.DidReloadScripts]
    private static void OnEditorQuit()
    {
        if (Instance != null && !string.IsNullOrEmpty(Instance.playerName))
        {
            Instance.StartCoroutine(Instance.DeletePlayerFromDatabase(Instance.playerName));
        }
    }
#endif*/

    // Coroutine to send DELETE request to backend and remove player
    public IEnumerator DeletePlayerFromDatabase(string name)
    {
        string url = Endpoints.PlayerByName(name);
        UnityWebRequest deleteRequest = UnityWebRequest.Delete(url);
        yield return deleteRequest.SendWebRequest();

        if (deleteRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning("Failed to delete player: " + deleteRequest.error);
        }
        else
        {
            Debug.Log("Player deleted succesfully from database on exit");
        }
    }
}