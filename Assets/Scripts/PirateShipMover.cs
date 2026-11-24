using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class PirateShipMover: MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerShip;
    [SerializeField] private ShipMover playerShipMover;

    [Header("Speed Settings")]
    [SerializeField] private float catchUpSpeed = 1f;
    [SerializeField] private float heightTolerance = 0.1f;
    private float progress = 0f;
    private float baseSpeed;

    [Header("Spline Settings")]
    [SerializeField] private SplineContainer splineContainer;

    private void Start()
    {
        if (splineContainer == null)
        {
            enabled = false;
            return;
        }
        baseSpeed = playerShipMover.GetShipSpeed();

        UpdateShipPosition(0f);
    }

    private void Update()
    {
        if (splineContainer == null || playerShip == null || baseSpeed == 0) return;

        float heightDifference = Mathf.Abs(transform.position.y - playerShip.position.y);

        float currentSpeed = baseSpeed;

        if (heightDifference > heightTolerance)
        {
            float t = Mathf.Clamp01(heightDifference / 3f); 
            currentSpeed = Mathf.Lerp(baseSpeed, catchUpSpeed, t);
        }

        float splineLength = splineContainer.Spline.GetLength();
        progress += (currentSpeed * Time.deltaTime) / splineLength;
        progress = Mathf.Clamp01(progress);

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

    public void StopPirateShip()
    {
        baseSpeed = 0;
    }
}
