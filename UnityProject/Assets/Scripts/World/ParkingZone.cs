using UnityEngine;
using UnityEngine.Events;

namespace UltimateCarSimulator
{
    [RequireComponent(typeof(Collider))]
    public sealed class ParkingZone : MonoBehaviour
    {
        [SerializeField] private float maxSpeedKph = 4f;
        [SerializeField] private float requiredStaySeconds = 2f;
        [SerializeField] private UnityEvent onParkingCompleted;
        private VehicleController candidate;
        private float stayTimer;
        private bool completed;

        private void Reset()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (completed) return;
            VehicleController vehicle = other.GetComponentInParent<VehicleController>();
            if (vehicle == null || vehicle.SpeedKph > maxSpeedKph)
            {
                candidate = null;
                stayTimer = 0f;
                return;
            }
            if (candidate != vehicle)
            {
                candidate = vehicle;
                stayTimer = 0f;
            }
            stayTimer += Time.deltaTime;
            if (stayTimer >= requiredStaySeconds)
            {
                completed = true;
                onParkingCompleted?.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            VehicleController vehicle = other.GetComponentInParent<VehicleController>();
            if (vehicle == candidate)
            {
                candidate = null;
                stayTimer = 0f;
            }
        }
    }
}