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
        float halfWidth = Camera.main.orthographicSize * Camera.main.aspect; //Orthographic Size = 5
                                                                             //Aspect = 16 / 9 = 1.777
        minX = playerBase.position.x + halfWidth;
        maxX = enemyBase.position.x - halfWidth;
    }

    void Update()
    {
        Vector2 screenPos;
        bool pressed = false;
        bool down = false;

        if (Touchscreen.current != null)
        {
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            pressed = Touchscreen.current.primaryTouch.press.isPressed;
            down = Touchscreen.current.primaryTouch.press.wasPressedThisFrame;
        }
        else if (Mouse.current != null)
        {
            screenPos = Mouse.current.position.ReadValue();
            pressed = Mouse.current.leftButton.isPressed;
            down = Mouse.current.leftButton.wasPressedThisFrame;
        }
        else
        {
            return;
        }
        if (Touchscreen.current != null)
        {
            Debug.Log("Touch");
        }

        if (Mouse.current != null)
        {
            Debug.Log("Mouse");
        }
        if (down)
        {
            lastMouseWorld = Camera.main.ScreenToWorldPoint(screenPos); //클릭하자마자 위치
        }

        if (pressed)
        {
            Vector3 currentMouseWorld = Camera.main.ScreenToWorldPoint(screenPos);  //드래그 중

            float deltaX = lastMouseWorld.x - currentMouseWorld.x;  //얼마나 움직였는지 계산

            targetX += deltaX;                                       //카메라 이동
            targetX = Mathf.Clamp(targetX, minX, maxX);

            lastMouseWorld = currentMouseWorld;
        }
        float smoothX = Mathf.SmoothDamp(transform.position.x, targetX, ref velocity, smoothTime); 
        transform.position = new Vector3(smoothX, transform.position.y, transform.position.z);
    }
}
