using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    public Transform playerBase;
    public Transform enemyBase;

    public float smoothTime = 0.15f;
    public float minX;
    public float maxX;

    private float targetX;
    private float velocity;

    private Vector3 lastMouseWorld;

    void Start()
    {
        targetX = transform.position.x; 
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        minX = playerBase.position.x + halfWidth;
        maxX = enemyBase.position.x - halfWidth;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            lastMouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 currentMouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            float deltaX = lastMouseWorld.x - currentMouseWorld.x;

            targetX += deltaX;
            targetX = Mathf.Clamp(targetX, minX, maxX);

            lastMouseWorld = currentMouseWorld;
        }

        float smoothX = Mathf.SmoothDamp(
            transform.position.x,
            targetX,
            ref velocity,
            smoothTime
        );

        transform.position = new Vector3(
            smoothX,
            transform.position.y,
            transform.position.z
        );
    }
}
