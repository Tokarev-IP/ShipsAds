using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class ShipMover : MonoBehaviour
{
    [Header("Spline Settings")]
    [SerializeField] private SplineContainer splineContainer;

    [Tooltip("Moving speed of the ship")]
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float fastSpeed = 1f;

    private float progress = 0f;

    private void Start()
    {
        if (splineContainer == null)
        {
            enabled = false;
            return;
        }

        UpdateShipPosition(0f);
    }

    private void Update()
    {
        if (splineContainer == null || moveSpeed == 0) return;


        float splineLength = splineContainer.Spline.GetLength();

        progress += (moveSpeed * Time.deltaTime) / splineLength;

        if (progress >= 1f)
        {
            progress = 1f;
            enabled = false;
        }

        UpdateShipPosition(progress);
    }

    private void UpdateShipPosition(float t)
    {
        Vector3 position = splineContainer.EvaluatePosition(t);
        transform.position = position;

        float3 tangent = splineContainer.EvaluateTangent(t);

        Vector2 dir = new Vector2(tangent.x, tangent.y);

        if (dir.sqrMagnitude > 0.001f)
        {
            dir.Normalize();

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            float offsetAngle = 90f;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle + offsetAngle);

            float rotationSpeed = 15f;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public float GetShipSpeed()
    {
        return moveSpeed;
    }

    public void SpeedUp()
    {
        moveSpeed = fastSpeed;
    }

    public void StopShip()
    {
        moveSpeed = 0f;
    }
}
