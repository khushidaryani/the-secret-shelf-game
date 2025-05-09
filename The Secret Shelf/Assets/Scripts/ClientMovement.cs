using UnityEngine;
using System.Collections;

public class ClientMovement : MonoBehaviour
{
    [Header("Client Settings")]
    public float speed = 2f;
    public Transform targetPoint;
    public Transform exitPoint;
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 direction;

    private bool reachedTarget = false;
    private bool exiting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (targetPoint != null && !reachedTarget)
        {
            direction = ((Vector2)targetPoint.position - rb.position).normalized;
            AnimateMovement(direction);
        }
    }

    void FixedUpdate()
    {
        if (targetPoint != null && !reachedTarget)
        {
            float distance = Vector2.Distance(rb.position, targetPoint.position);

            if (distance > 0.1f)
            {
                rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
                AnimateMovement(direction); // Apply movement animation
            }
            else
            {
                reachedTarget = true;
                AnimateMovement(Vector2.zero); // Stop movement animation

                if (exiting)
                {
                    StartCoroutine(DeactivateAfterDelay(1.5f)); // Wait before deactivating client after moving to exit
                }
            }
        }
    }
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // Destroy client after delay
    }

    void AnimateMovement(Vector2 direction)
    {
        bool isMoving = direction.magnitude > 0.01f;
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            animator.SetFloat("vertical", direction.y);
        }
    }

    // Called when the client is set to exit
    public void StartExit()
    {
        if (exitPoint != null)
        {
            targetPoint = exitPoint;
            reachedTarget = false;
            exiting = true;
        }
    }
}