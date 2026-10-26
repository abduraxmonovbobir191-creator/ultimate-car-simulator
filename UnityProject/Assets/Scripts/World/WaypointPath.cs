using UnityEngine;

namespace UltimateCarSimulator
{
    public sealed class WaypointPath : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        public int Count => points == null ? 0 : points.Length;
        public Transform GetPoint(int index) => Count == 0 ? null : points[Mathf.Clamp(index, 0, Count - 1)];

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (points == null || points.Length == 0) return;
            Gizmos.color = Color.yellow;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] == null) continue;
                Gizmos.DrawSphere(points[i].position, 0.3f);
                if (i + 1 < points.Length && points[i + 1] != null)
                    Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
        }
#endif
    }
}