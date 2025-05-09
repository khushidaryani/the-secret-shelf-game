using UnityEngine;

// Handle librarian (player) movement and animation
public class LibrarianMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Animation")]
    public Animator animator;

    // Margins to restrict player movement
    [Header("Movement Boundaries")]
    public float leftMargin = -15f;
    public float rightMargin = 14f;
    public float bottomMargin = -13f;
    public float topMargin = 8f;

    private Rigidbody2D rb;
    private bool isDialogueActive = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {
        if (!isDialogueActive)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector2 direction = new Vector2(horizontal, vertical).normalized;
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;

            newPosition.x = Mathf.Clamp(newPosition.x, leftMargin, rightMargin);
            newPosition.y = Mathf.Clamp(newPosition.y, bottomMargin, topMargin);

            rb.MovePosition(newPosition);

            AnimateMovement(direction);
        }
    }

    void AnimateMovement(Vector2 direction)
    {
        if (animator != null)
        {
            bool isMoving = direction.magnitude > 0;

            animator.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
        }
    }

    // Method to update the dialogue's state
    public void SetDialogueActive(bool isActive)
    {
        isDialogueActive = isActive;
    }
}