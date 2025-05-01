using UnityEngine;

public class LibrarianMovement : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;

    private Rigidbody2D rb;

    // Margins to restrict player movement
    public float leftMargin = -15f;
    public float rightMargin = 14f;
    public float bottomMargin = -13f;
    public float topMargin = 8f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;

        // Clamp using margins
        newPosition.x = Mathf.Clamp(newPosition.x, leftMargin, rightMargin);
        newPosition.y = Mathf.Clamp(newPosition.y, bottomMargin, topMargin);

        rb.MovePosition(newPosition);

        AnimateMovement(direction);
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
}