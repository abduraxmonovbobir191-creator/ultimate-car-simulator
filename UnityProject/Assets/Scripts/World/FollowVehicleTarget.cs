using UnityEngine;

namespace UltimateCarSimulator
{
    public sealed class FollowVehicleTarget : MonoBehaviour
    {
        [SerializeField] private VehicleCamera vehicleCamera;
        [SerializeField] private Transform target;
        private void Start()
        {
            if (vehicleCamera == null) vehicleCamera = Camera.main != null ? Camera.main.GetComponent<VehicleCamera>() : null;
            if (vehicleCamera != null && target != null) vehicleCamera.SetTarget(target);
        }
    }
}