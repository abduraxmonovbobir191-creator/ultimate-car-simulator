using UnityEngine;

namespace UltimateCarSimulator
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class TrafficVehicleAI : MonoBehaviour
    {
        [SerializeField] private WaypointPath path;
        [SerializeField, Min(1f)] private float cruiseSpeed = 10f;
        [SerializeField, Min(0.1f)] private float waypointReachDistance = 3f;
        [SerializeField, Min(0f)] private float acceleration = 5f;
        [SerializeField, Min(0f)] private float turnSpeed = 3f;
        [SerializeField] private LayerMask obstacleMask = ~0;
        [SerializeField, Min(1f)] private float obstacleCheckDistance = 8f;
        [SerializeField, Min(0f)] private float stopDistance = 4f;
        private Rigidbody body;
        private int currentWaypoint;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            body.interpolation = RigidbodyInterpolation.Interpolate;
            body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        private void FixedUpdate()
        {
            if (path == null || path.Count == 0) return;
            Transform point = path.GetPoint(currentWaypoint);
            if (point == null) return;
            Vector3 toPoint = point.position - transform.position;
            toPoint.y = 0f;
            if (toPoint.magnitude <= waypointReachDistance)
            {
                currentWaypoint = (currentWaypoint + 1) % path.Count;
                point = path.GetPoint(currentWaypoint);
                if (point == null) return;
                toPoint = point.position - transform.position;
                toPoint.y = 0f;
            }

            bool blocked = Physics.SphereCast(transform.position + Vector3.up * 0.8f, 0.65f,
                transform.forward, out RaycastHit hit, obstacleCheckDistance, obstacleMask, QueryTriggerInteraction.Ignore)
                && hit.collider.transform.root != transform.root;

            float targetSpeed = blocked && hit.distance < stopDistance ? 0f : cruiseSpeed;
            Vector3 desiredVelocity = toPoint.sqrMagnitude > 0.01f ? toPoint.normalized * targetSpeed : Vector3.zero;
            Vector3 horizontalVelocity = body.linearVelocity;
            horizontalVelocity.y = 0f;
            body.linearVelocity = Vector3.MoveTowards(horizontalVelocity, desiredVelocity, acceleration * Time.fixedDeltaTime)
                                   + Vector3.up * body.linearVelocity.y;

            if (toPoint.sqrMagnitude > 0.05f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(toPoint.normalized, Vector3.up);
                body.MoveRotation(Quaternion.Slerp(body.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
            }
        }

        public void SetPath(WaypointPath newPath) { path = newPath; currentWaypoint = 0; }
    }
}