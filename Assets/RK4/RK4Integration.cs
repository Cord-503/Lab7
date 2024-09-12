using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RK4Integration : MonoBehaviour
{
    [SerializeField] private float springConstant;
    [SerializeField] private float mass;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 initialVelocity;

    private float deltaTime;
    private Vector3 position;
    private Vector3 velocity;

    void Start()
    {
        Initialize();
    }

    void FixedUpdate()
    {
        PerformRK4Integration();
        UpdateTransform();
    }

    void Initialize()
    {
        position = initialPosition;
        velocity = initialVelocity;
        deltaTime = Time.fixedDeltaTime;
    }

    void PerformRK4Integration()
    {
        // Compute RK4 coefficients
        Vector3 k1_v, k1_x, k2_v, k2_x, k3_v, k3_x, k4_v, k4_x;
        CalculateRK4Coefficients(out k1_v, out k1_x, out k2_v, out k2_x, out k3_v, out k3_x, out k4_v, out k4_x);

        // Update velocity and position
        velocity += (k1_v + 2.0f * k2_v + 2.0f * k3_v + k4_v) / 6.0f;
        position += (k1_x + 2.0f * k2_x + 2.0f * k3_x + k4_x) / 6.0f;
    }

    void CalculateRK4Coefficients(out Vector3 k1_v, out Vector3 k1_x, out Vector3 k2_v, out Vector3 k2_x, out Vector3 k3_v, out Vector3 k3_x, out Vector3 k4_v, out Vector3 k4_x)
    {
        // k1
        Vector3 acceleration1 = CalculateAcceleration(position);
        k1_v = deltaTime * acceleration1;
        k1_x = deltaTime * velocity;

        // k2
        k2_v = deltaTime * CalculateAcceleration(position + k1_x * 0.5f);
        k2_x = deltaTime * (velocity + k1_v * 0.5f);

        // k3
        k3_v = deltaTime * CalculateAcceleration(position + k2_x * 0.5f);
        k3_x = deltaTime * (velocity + k2_v * 0.5f);

        // k4
        k4_v = deltaTime * CalculateAcceleration(position + k3_x);
        k4_x = deltaTime * (velocity + k3_v);
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
