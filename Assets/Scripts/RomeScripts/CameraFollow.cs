using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.1f;
    public Vector3 offset = new Vector3(0f, 2f, -5f);
    public float maxDistance = 10f;
    public float resetSmoothTime = 1f;

    private Vector3 desiredPosition;
    private Vector3 smoothVelocity;
    private Quaternion desiredRotation;
    private Vector3 initialOffset;
    private bool isObstacleAvoiding;

    private void Start()
    {
        initialOffset = offset;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        desiredPosition = target.position + offset;

        RaycastHit hit;
        if (Physics.Raycast(target.position, -transform.forward, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                isObstacleAvoiding = false;
            }
            else
            {
                desiredPosition = hit.point;
                isObstacleAvoiding = true;
            }
        }
        else
        {
            isObstacleAvoiding = false;
        }

        Vector3 lookDirection = target.position - transform.position;
        desiredRotation = Quaternion.LookRotation(lookDirection);

        float currentSmoothSpeed = isObstacleAvoiding ? resetSmoothTime : smoothSpeed;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref smoothVelocity, currentSmoothSpeed);
        transform.position = smoothedPosition;

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, currentSmoothSpeed);
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(desiredPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            desiredPosition.y = hit.point.y + offset.y+5;
        }
    }
}