using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [Header("References")]
    public DialogueSequence dialogueSequence;
    public DialogueManager dialogueManager;

    private string url = Endpoints.bookUrl;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Target"))
        {
            Debug.Log("Client triggered the dialogue zone");
            hasTriggered = true;
            StartCoroutine(FetchHintAndStartDialogue());
        }
    }

    // Fetch books from the API, picks one randomly and inserts the first hint into the cloned dialogue
    private IEnumerator FetchHintAndStartDialogue()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to fetch books: " + request.error);
            yield break;
        }

        // Wrap JSON array into an object for Unity JSON utility
        string rawJson = request.downloadHandler.text;
        string wrappedJson = "{\"books\":" + rawJson + "}";
        BookListWrapper wrapper = JsonUtility.FromJson<BookListWrapper>(wrappedJson);

        if (wrapper.books == null || wrapper.books.Length == 0)
        {
            Debug.LogWarning("No books received from API");
            yield break;
        }

        Book randomBook = wrapper.books[Random.Range(0, wrapper.books.Length)];
        string hint = randomBook.hints[Random.Range(0, 3)];
        Debug.Log("Random hint inserted: " + hint);

        // Clone the dialogue lines and insert the hint into the 4th line
        DialogueLine[] clonedLines = CloneDialogueLines(dialogueSequence.lines);
        if (clonedLines.Length >= 4)
        {
            clonedLines[3].dialogueText = clonedLines[3].dialogueText.Replace("[HINT HERE]", hint);
        }

        // Start the dialogue using the cloned and modified lines
        dialogueManager.StartDialogue(clonedLines);
        Debug.Log("✅ Dialogue started with fresh copy");
    }

    // Clone an array of DialogueLine objects to avoid modifying the original asset
    private DialogueLine[] CloneDialogueLines(DialogueLine[] originalLines)
    {
        DialogueLine[] cloned = new DialogueLine[originalLines.Length];
        for (int i = 0; i < originalLines.Length; i++)
        {
            cloned[i] = new DialogueLine
            {
                characterName = originalLines[i].characterName,
                portraitImage = originalLines[i].portraitImage,
                dialogueText = originalLines[i].dialogueText
            };
        }
        return cloned;
    }
}