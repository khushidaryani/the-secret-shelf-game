using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class BookListLoader : MonoBehaviour
{
    public GameObject bookButtonPrefab;
    public Transform gridContent;
    public GameObject firstManualButton;

    private string url = Endpoints.bookUrl;

    void Start()
    {
        StartCoroutine(LoadBookList());
    }

    IEnumerator LoadBookList()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Parse JSON response into array of Book objects
                Book[] books = JsonHelper.FromJson<Book>(www.downloadHandler.text);

                if (books.Length == 0)
                {
                    Debug.LogError("No books found in the response");
                    yield break;
                }

                // Assign first book to the manually placed button
                BookButton manualBookButton = firstManualButton.GetComponent<BookButton>();
                if (manualBookButton != null)
                {
                    manualBookButton.SetBook(books[0]);
                }
                else
                {
                    Debug.LogError("BookButton script missing on firstManualButton");
                }

                // Instantiate and assign remaining books to dynamically created buttons
                for (int i = 1; i < books.Length; i++)
                {
                    GameObject bookButton = Instantiate(bookButtonPrefab, gridContent);

                    if (bookButton != null)
                    {
                        BookButton bb = bookButton.GetComponent<BookButton>();
                        if (bb != null)
                        {
                            bb.SetBook(books[i]);
                        }
                        else
                        {
                            Debug.LogError("BookButton script missing in instantiated prefab");
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Error fetching book data: " + www.error);
            }
        }
    }
}