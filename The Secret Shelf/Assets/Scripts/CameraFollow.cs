using System.Collections.Generic;
using UnityEngine;

// Controls camera behavior to follow clients or the librarian
public class CameraFollow : MonoBehaviour
{
    [Header("Targets")]
    public List<Transform> clients;
    public Transform librarian;
    public Transform clientTargetPoint;

    [Header("Camera Settings")]
    public float smoothing = 5f;
    public float cameraZoomOut = 5f;

    [Header("Map Boundaries")]
    private float minX = -15f, maxX = 14f, minY = -13f, maxY = 8f;
    public float leftMargin = 0.5f;
    public float rightMargin = 0.5f;
    public float bottomMargin = 0.5f;
    public float topMargin = 0.5f;

    private Camera mainCamera;
    private Transform target;
    private int currentClientIndex = 0;
    private bool hasClientArrived = false;
    private bool hasLibrarianMoved = false;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera != null)
            mainCamera.orthographicSize = cameraZoomOut;

        if (clients.Count > 0)
            target = clients[0];
        else
            Debug.LogWarning("No clients assigned in CameraFollow script!");
    }

    void Update()
    {
        HandleCameraFlow();
        FollowTarget();
    }

    // Determines when to switch from following the client to the librarian
    void HandleCameraFlow()
    {
        if (!hasClientArrived && target != null && clientTargetPoint != null)
        {
            float dist = Vector2.Distance(target.position, clientTargetPoint.position);
            if (dist < 0.2f)
            {
                hasClientArrived = true;
                target = null; // The camera stops
                Debug.Log("Client reached counter");
            }
        }

        if (hasClientArrived && !hasLibrarianMoved)
        {
            // Detect if the librarian has started moving
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
            {
                hasLibrarianMoved = true;
                target = librarian; // Start following the librarian
                Debug.Log("Librarian started moving");
            }
        }
    }

    // Makes the camera follow the current target smoothly, within map limits
    void FollowTarget()
    {
        if (target != null)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);

            float camHalfHeight = mainCamera.orthographicSize;
            float camHalfWidth = camHalfHeight * mainCamera.aspect;

            float clampedX = Mathf.Clamp(targetPos.x, minX + camHalfWidth - leftMargin, maxX - camHalfWidth + rightMargin);
            float clampedY = Mathf.Clamp(targetPos.y, minY + camHalfHeight - bottomMargin, maxY - camHalfHeight + topMargin);

            Vector3 clampedPos = new Vector3(clampedX, clampedY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, clampedPos, smoothing * Time.deltaTime);
        }
    }

    // Set a new camera target manually
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    // Call this after the current client has finished their interaction and left.
    // Moves the camera to follow the next client.
    public void AdvanceToNextClient()
    {
        currentClientIndex++;
        hasClientArrived = false;
        hasLibrarianMoved = false;

        if (currentClientIndex < clients.Count)
        {
            target = clients[currentClientIndex];
            Debug.Log("Next client entering");
        }
        else
        {
            //Debug.Log("All clients finished");
        }
    }
}