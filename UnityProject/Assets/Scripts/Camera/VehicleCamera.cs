using UnityEngine;

namespace UltimateCarSimulator
{
    public sealed class VehicleCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 followOffset = new Vector3(0f, 4.2f, -7.5f);
        [SerializeField, Min(0.1f)] private float positionSmooth = 7f;
        [SerializeField, Min(0.1f)] private float rotationSmooth = 6f;
        [SerializeField, Min(0f)] private float lookAhead = 2.2f;
        [SerializeField] private bool useTargetYaw = true;

        public void SetTarget(Transform newTarget) => target = newTarget;

        private void LateUpdate()
        {
            if (target == null) return;
            Quaternion yaw = useTargetYaw ? Quaternion.Euler(0f, target.eulerAngles.y, 0f) : Quaternion.identity;
            Vector3 desiredPosition = target.position + yaw * followOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, 1f - Mathf.Exp(-positionSmooth * Time.deltaTime));
            Vector3 lookPoint = target.position + Vector3.up * 1.2f + target.forward * lookAhead;
            Quaternion desiredRotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 1f - Mathf.Exp(-rotationSmooth * Time.deltaTime));
        }
    }
}