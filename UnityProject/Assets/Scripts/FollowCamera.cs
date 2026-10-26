using UnityEngine;

/// <summary>Attach to the camera and assign the car transform as Target.</summary>
public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 4.2f, -7.5f);
    [SerializeField] private float positionSmoothness = 6f;
    [SerializeField] private float rotationSmoothness = 8f;
    [SerializeField] private float lookHeight = 1.2f;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            1f - Mathf.Exp(-positionSmoothness * Time.deltaTime));

        Vector3 lookPoint = target.position + Vector3.up * lookHeight;
        Quaternion desiredRotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            1f - Mathf.Exp(-rotationSmoothness * Time.deltaTime));
    }
}
