using UnityEngine;

/// <summary>
/// Starter arcade-style car controller for the first playable prototype.
/// Add this to a car root with a Rigidbody and colliders.
/// This uses Unity's built-in input axes (WASD / arrow keys).
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Driving")]
    [SerializeField] private float acceleration = 22f;
    [SerializeField] private float reverseAcceleration = 10f;
    [SerializeField] private float maxForwardSpeed = 32f;
    [SerializeField] private float maxReverseSpeed = 9f;
    [SerializeField] private float steeringDegreesPerSecond = 95f;
    [SerializeField] private float brakeStrength = 28f;

    [Header("Grip")]
    [SerializeField, Range(0f, 1f)] private float lateralGrip = 0.88f;
    [SerializeField] private float handbrakeGrip = 0.35f;
    [SerializeField] private float downforce = 18f;

    private Rigidbody body;
    private float throttleInput;
    private float steeringInput;
    private bool handbrake;

    public float SpeedKph => body == null ? 0f : body.linearVelocity.magnitude * 3.6f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.mass = Mathf.Max(body.mass, 800f);
        body.linearDamping = 0.08f;
        body.angularDamping = 1.6f;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.centerOfMass = new Vector3(0f, -0.45f, 0f);
    }

    private void Update()
    {
        throttleInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
        handbrake = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(body.linearVelocity);
        float forwardSpeed = localVelocity.z;

        if (throttleInput > 0f && forwardSpeed < maxForwardSpeed)
        {
            body.AddForce(transform.forward * (throttleInput * acceleration), ForceMode.Acceleration);
        }
        else if (throttleInput < 0f)
        {
            if (forwardSpeed > 1.0f)
            {
                body.AddForce(-transform.forward * (Mathf.Abs(throttleInput) * brakeStrength), ForceMode.Acceleration);
            }
            else if (forwardSpeed > -maxReverseSpeed)
            {
                body.AddForce(-transform.forward * (Mathf.Abs(throttleInput) * reverseAcceleration), ForceMode.Acceleration);
            }
        }

        float speedFactor = Mathf.Clamp01(body.linearVelocity.magnitude / 2f);
        float directionFactor = forwardSpeed >= -0.5f ? 1f : -1f;
        float turn = steeringInput * steeringDegreesPerSecond * speedFactor * directionFactor;
        Quaternion rotation = Quaternion.Euler(0f, turn * Time.fixedDeltaTime, 0f);
        body.MoveRotation(body.rotation * rotation);

        Vector3 sideVelocity = transform.right * Vector3.Dot(body.linearVelocity, transform.right);
        float grip = handbrake ? handbrakeGrip : lateralGrip;
        body.AddForce(-sideVelocity * grip * 8f, ForceMode.Acceleration);

        if (body.linearVelocity.sqrMagnitude > 1f)
            body.AddForce(-transform.up * downforce * body.linearVelocity.magnitude, ForceMode.Acceleration);
    }
}
