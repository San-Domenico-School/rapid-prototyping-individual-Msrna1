using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float turnSpeed = 30.0f;
    [SerializeField] private float handbrakeForce = 3000f;
    [SerializeField] private float dragForce = 0.1f;
    [SerializeField] private float maxSpeed = 50f;

    private float verticalInput;
    private float horizontalInput;
    private bool isHandbrakeActive;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Apply drag force
        rb.drag = dragForce;

        // Calculate current speed
        float currentSpeed = rb.velocity.magnitude;

        // Apply forward/backward force
        if (currentSpeed < maxSpeed)
        {
            rb.AddRelativeForce(Vector3.forward * speed * rb.mass * verticalInput);
        }

        // Apply turning
        if (rb.velocity.magnitude > 0.1f) // Only turn if moving
        {
            float turn = horizontalInput * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // Apply handbrake
        if (isHandbrakeActive)
        {
            rb.AddForce(-rb.velocity.normalized * handbrakeForce);
        }
    }

    private void OnMove(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();
        verticalInput = inputVector.y;
        horizontalInput = inputVector.x;
    }

    private void OnHandbrake(InputValue input)
    {
        isHandbrakeActive = input.isPressed;
    }
}