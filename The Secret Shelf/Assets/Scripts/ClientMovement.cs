using UnityEngine;

public class ClientMovement : MonoBehaviour
{
    public float speed = 2f;
    public Transform targetPoint;
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 direction;
    private bool reachedTarget = false;

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
            }
            else
            {
                reachedTarget = true;
                AnimateMovement(Vector2.zero); // Para animación
            }
        }
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
}
