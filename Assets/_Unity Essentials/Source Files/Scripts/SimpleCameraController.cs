using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    public float moveSpeed = 3.0f;           // Movement speed
    public float rotationSpeed = 100.0f;      // Rotation speed for A/D keys in degrees per second
    public float mouseSensitivity = 1.0f;    // Mouse look sensitivity

    [Header("Smooth Movement")]
    public float acceleration = 10f;
    public float deceleration = 15f;

    [Header("Head Bobbing")]
    public float bobbingSpeed = 10f;         // Speed of the head bob
    public float bobbingAmount = 0.1f;       // Amount of head bob
    private float defaultPosY = 0;           // The default Y position
    private float timer = 0;                 // Timer for the bob effect

    private float rotationX = 0.0f;          // Rotation around the X axis (up and down look)
    private float currentSpeed = 0f;

    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        // Smooth movement with acceleration/deceleration
        float targetSpeed = Input.GetAxis("Vertical") * moveSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 
            (targetSpeed != 0 ? acceleration : deceleration) * Time.deltaTime);

        // Movement direction
        Vector3 moveDirection = new Vector3(transform.forward.x, 0.0f, transform.forward.z).normalized;
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

        // Head bobbing effect
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            // Increment the timer and calculate bob offset
            timer += Time.deltaTime * bobbingSpeed;
            float bobOffset = Mathf.Sin(timer) * bobbingAmount;
            
            // Apply the bob offset to the camera's local position
            Vector3 pos = transform.localPosition;
            pos.y = defaultPosY + bobOffset;
            transform.localPosition = pos;
        }
        else
        {
            // Reset the timer and position when not moving
            timer = 0;
            Vector3 pos = transform.localPosition;
            pos.y = Mathf.Lerp(pos.y, defaultPosY, Time.deltaTime * 2f);
            transform.localPosition = pos;
        }

        // Rotation with A/D keys
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0, Space.World); // Rotate around the global Y axis

        // Mouse Look
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;  // Subtracting to invert the up and down look
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);  // Clamp the up and down look to avoid flipping

        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, 0);
    }
}
