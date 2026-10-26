using UnityEngine;

namespace UltimateCarSimulator
{
    /// <summary>WheelCollider-based vehicle physics. Configure four WheelColliders in the Inspector.</summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class VehicleController : MonoBehaviour
    {
        [Header("Wheel colliders")]
        [SerializeField] private WheelCollider frontLeft;
        [SerializeField] private WheelCollider frontRight;
        [SerializeField] private WheelCollider rearLeft;
        [SerializeField] private WheelCollider rearRight;
        [Header("Wheel visuals (optional)")]
        [SerializeField] private Transform frontLeftVisual;
        [SerializeField] private Transform frontRightVisual;
        [SerializeField] private Transform rearLeftVisual;
        [SerializeField] private Transform rearRightVisual;
        [Header("Driving")]
        [SerializeField, Min(100f)] private float motorTorque = 1800f;
        [SerializeField, Min(10f)] private float brakeTorque = 3500f;
        [SerializeField, Min(10f)] private float handbrakeTorque = 6000f;
        [SerializeField, Range(10f, 50f)] private float maxSteerAngle = 32f;
        [SerializeField, Min(0f)] private float downforce = 35f;
        [SerializeField, Min(0f)] private float steeringReductionSpeed = 22f;
        [SerializeField] private bool frontWheelDrive;
        [SerializeField] private bool allWheelDrive;
        [Header("Stability")]
        [SerializeField, Range(0f, 1f)] private float lateralGripAssist = 0.18f;
        [SerializeField, Min(0f)] private float centerOfMassDrop = 0.45f;

        private Rigidbody body;
        private float throttle;
        private float steering;
        private float brake;
        private bool handbrake;

        public float SpeedKph => body == null ? 0f : body.linearVelocity.magnitude * 3.6f;
        public float SignedSpeedKph => body == null ? 0f : Vector3.Dot(body.linearVelocity, transform.forward) * 3.6f;
        public float NormalizedThrottle => throttle;
        public float SteeringInput => steering;
        public bool IsGrounded => (frontLeft != null && frontLeft.isGrounded) || (frontRight != null && frontRight.isGrounded) || (rearLeft != null && rearLeft.isGrounded) || (rearRight != null && rearRight.isGrounded);

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            body.mass = Mathf.Max(body.mass, 800f);
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            body.centerOfMass += Vector3.down * centerOfMassDrop;
        }

        // Called by keyboard, gamepad or mobile input adapters.
        public void SetDriverInput(float throttleInput, float steeringInput, float brakeInput, bool handbrakeInput)
        {
            throttle = Mathf.Clamp(throttleInput, -1f, 1f);
            steering = Mathf.Clamp(steeringInput, -1f, 1f);
            brake = Mathf.Clamp01(brakeInput);
            handbrake = handbrakeInput;
        }

        private void FixedUpdate()
        {
            if (body == null) return;
            ApplySteering();
            ApplyDrive();
            ApplyBrakes();
            body.AddForce(-transform.up * (downforce * body.linearVelocity.magnitude), ForceMode.Force);
            ApplyGripAssist();
        }

        private void ApplySteering()
        {
            float speedFactor = Mathf.Lerp(1f, 0.48f, Mathf.Clamp01(body.linearVelocity.magnitude / steeringReductionSpeed));
            float angle = steering * maxSteerAngle * speedFactor;
            if (frontLeft != null) frontLeft.steerAngle = angle;
            if (frontRight != null) frontRight.steerAngle = angle;
        }

        private void ApplyDrive()
        {
            float torque = throttle * motorTorque;
            if (frontWheelDrive || allWheelDrive)
            {
                if (frontLeft != null) frontLeft.motorTorque = torque;
                if (frontRight != null) frontRight.motorTorque = torque;
            }
            else
            {
                if (frontLeft != null) frontLeft.motorTorque = 0f;
                if (frontRight != null) frontRight.motorTorque = 0f;
            }

            if (rearLeft != null) rearLeft.motorTorque = (!frontWheelDrive || allWheelDrive) ? torque : 0f;
            if (rearRight != null) rearRight.motorTorque = (!frontWheelDrive || allWheelDrive) ? torque : 0f;
        }

        private void ApplyBrakes()
        {
            float rearBrake = handbrake ? handbrakeTorque : brake * brakeTorque;
            float frontBrake = brake * brakeTorque;
            if (frontLeft != null) frontLeft.brakeTorque = frontBrake;
            if (frontRight != null) frontRight.brakeTorque = frontBrake;
            if (rearLeft != null) rearLeft.brakeTorque = rearBrake;
            if (rearRight != null) rearRight.brakeTorque = rearBrake;
        }

        private void ApplyGripAssist()
        {
            if (lateralGripAssist <= 0f || !IsGrounded) return;
            Vector3 localVelocity = transform.InverseTransformDirection(body.linearVelocity);
            Vector3 correction = -transform.right * (localVelocity.x * lateralGripAssist);
            body.AddForce(correction, ForceMode.Acceleration);
        }

        private void LateUpdate()
        {
            UpdateWheelVisual(frontLeft, frontLeftVisual);
            UpdateWheelVisual(frontRight, frontRightVisual);
            UpdateWheelVisual(rearLeft, rearLeftVisual);
            UpdateWheelVisual(rearRight, rearRightVisual);
        }

        private static void UpdateWheelVisual(WheelCollider wheel, Transform visual)
        {
            if (wheel == null || visual == null) return;
            wheel.GetWorldPose(out Vector3 position, out Quaternion rotation);
            visual.SetPositionAndRotation(position, rotation);
        }

        public void ResetVehicle(Transform spawn)
        {
            if (spawn == null) return;
            body.linearVelocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            body.position = spawn.position + Vector3.up * 0.5f;
            body.rotation = spawn.rotation;
            SetDriverInput(0f, 0f, 1f, false);
        }
    }
}