using UnityEngine;

// Controls whether the book list is visible when the librarian is near a shelf
public class ShelfInteraction : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject bookListUI;

    [Header("Librarian Reference")]
    public Transform librarian;

    [Header("Interaction Settings")]
    [Range(0.1f, 2f)]
    public float interactionRadius = 0.7f;

    [Header("Shelf Settings")]
    public string shelfTag = "Shelf";

    void Start()
    {
        // Hides the book list at the beginning
        if (bookListUI != null) 
            bookListUI.SetActive(false);
    }

    void Update()
    {
        // Check for nearby shelves using OverlapCircle
        Collider2D[] hits = Physics2D.OverlapCircleAll(librarian.position, interactionRadius);
        bool nearShelf = false;

        foreach (var hit in hits)
        {
            if (hit.CompareTag(shelfTag))
            {
                nearShelf = true;
                break;
            }
        }

        // Toggle book list UI based on proximity to shelf
        if (bookListUI != null && bookListUI.activeSelf != nearShelf)
            bookListUI.SetActive(nearShelf);
    }

    // Draw a yellow wire sphere in Scene view to show interaction radius
    void OnDrawGizmosSelected()
    {
        if (librarian != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(librarian.position, interactionRadius);
        }
    }
}
