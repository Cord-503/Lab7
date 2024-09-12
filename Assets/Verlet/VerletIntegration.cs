using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerletIntegration : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private float springConstant;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] private Vector3 initialVelocity;

    private Vector3 previousPosition;
    private Vector3 position;
    private Vector3 velocity;

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        PerformVerletIntegration();
        UpdateTransform();
    }

    void Initialize()
    {
        position = initialPos;
        velocity = initialVelocity;
        previousPosition = position - velocity * Time.fixedDeltaTime;
    }

    void PerformVerletIntegration()
    {
        Vector3 acceleration = CalculateAcceleration(position);
        Vector3 newPosition = CalculateNewPosition(acceleration);

        previousPosition = position;
        position = newPosition;
    }

    Vector3 CalculateNewPosition(Vector3 acceleration)
    {
        return 2.0f * position - previousPosition + acceleration * Time.fixedDeltaTime * Time.fixedDeltaTime;
    }

    void UpdateTransform()
    {
        transform.position = position;
    }

    Vector3 CalculateAcceleration(Vector3 currentPosition)
    {
        return -(springConstant / mass) * currentPosition;
    }
}
