using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerIntegration : MonoBehaviour
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
        PerformEulerIntegration();
        UpdateTransform();
    }

    void Initialize()
    {
        position = initialPosition;
        velocity = initialVelocity;
        deltaTime = Time.fixedDeltaTime;
    }

    void PerformEulerIntegration()
    {
        Vector3 acceleration = CalculateAcceleration(position);

        velocity += acceleration * deltaTime;
        position += velocity * deltaTime;
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
