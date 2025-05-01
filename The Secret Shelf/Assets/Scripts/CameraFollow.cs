using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> clients;       // Lista de clientes (en orden de aparición)
    public Transform librarian;           // Bibliotecaria (jugador principal)
    public float smoothing = 5f;
    public float cameraZoomOut = 5f;
    public Transform clientTargetPoint;   // Punto donde se detiene el cliente

    private Camera mainCamera;
    private Transform target;
    private int currentClientIndex = 0;
    private bool hasClientArrived = false;
    private bool hasLibrarianMoved = false;

    // Límites del mapa
    private float minX = -15f, maxX = 14f, minY = -13f, maxY = 8f;
    public float leftMargin = 0.5f;
    public float rightMargin = 0.5f;
    public float bottomMargin = 0.5f;
    public float topMargin = 0.5f;

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

    void HandleCameraFlow()
    {
        if (!hasClientArrived && target != null && clientTargetPoint != null)
        {
            float dist = Vector2.Distance(target.position, clientTargetPoint.position);
            if (dist < 0.2f)
            {
                hasClientArrived = true;
                target = null; // Se detiene la cámara
                Debug.Log("Client reached counter");
            }
        }

        if (hasClientArrived && !hasLibrarianMoved)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0)
            {
                hasLibrarianMoved = true;
                target = librarian;
                Debug.Log("Librarian started moving");
            }
        }
    }

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

    // Llama a esto cuando el cliente haya sido atendido y se vaya
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
            Debug.Log("All clients finished");
        }
    }
}