using UnityEngine;

namespace UltimateCarSimulator
{
    [RequireComponent(typeof(VehicleController))]
    public sealed class KeyboardVehicleInput : MonoBehaviour
    {
        [SerializeField] private VehicleController vehicle;
        [SerializeField] private KeyCode handbrakeKey = KeyCode.Space;
        [SerializeField] private KeyCode resetKey = KeyCode.R;
        [SerializeField] private Transform resetPoint;

        private void Awake()
        {
            if (vehicle == null) vehicle = GetComponent<VehicleController>();
        }

        private void Update()
        {
            if (vehicle == null || !vehicle.isActiveAndEnabled) return;
            float throttle = 0f;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) throttle += 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) throttle -= 1f;
            float steer = 0f;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) steer -= 1f;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) steer += 1f;
            bool handbrake = Input.GetKey(handbrakeKey);
            float brake = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ? 1f : 0f;
            vehicle.SetDriverInput(throttle, steer, brake, handbrake);
            if (Input.GetKeyDown(resetKey)) vehicle.ResetVehicle(resetPoint);
        }
    }
}