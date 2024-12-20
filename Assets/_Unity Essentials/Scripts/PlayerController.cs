using UnityEngine;

// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Set player's movement speed.
    public float rotationSpeed = 120.0f; // Set player's rotation speed.
    public float verticalSpeed = 3.0f; // Set player's vertical movement speed.
    public float stabilizationSpeed = 5.0f; // Speed at which the UFO stabilizes

    private Rigidbody rb; // Reference to player's Rigidbody.

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {
        // Move player based on vertical input.
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveVertical * speed * Time.fixedDeltaTime;

        // Add vertical movement using VerticalFlight axis
        float verticalMovement = Input.GetAxis("VerticalFlight") * verticalSpeed;
        movement += Vector3.up * verticalMovement * Time.fixedDeltaTime;

        // Always stabilize
        // Gradually reduce velocity
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, stabilizationSpeed * Time.fixedDeltaTime);
        rb.angularVelocity = Vector3.Lerp(rb.angularVelocity, Vector3.zero, stabilizationSpeed * Time.fixedDeltaTime);
        
        // Level out rotation
        Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, stabilizationSpeed * Time.fixedDeltaTime));
        
        rb.MovePosition(rb.position + movement);

        // Rotate player based on horizontal input.
        float turn = Input.GetAxis("Horizontal") * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}