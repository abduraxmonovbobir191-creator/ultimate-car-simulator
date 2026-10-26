using UnityEngine;

namespace UltimateCarSimulator
{
    [RequireComponent(typeof(Collider))]
    public sealed class VehicleResetZone : MonoBehaviour
    {
        [SerializeField] private Transform resetPoint;
        private void Reset()
        {
            Collider trigger = GetComponent<Collider>();
            trigger.isTrigger = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            VehicleController vehicle = other.GetComponentInParent<VehicleController>();
            if (vehicle != null) vehicle.ResetVehicle(resetPoint);
        }
    }
}