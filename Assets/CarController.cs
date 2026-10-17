using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float acceleration = 1f;
    public float brakeForce = 4f;
    public float maxSpeed = 5f;
    public float turnSpeed = 90f;

    public event Action<float> OnSpeedChanged;

    private float speed = 0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        speed = getSpeed();
        OnSpeedChanged?.Invoke(speed);
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }

    public float getSpeed() => Mathf.Abs(rb.linearVelocity.magnitude);

    public void Accelerate() => rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
    public void Brake() => rb.linearVelocity *= 0.95f;
    public void Turn(float direction)
    {
        if (Mathf.Abs(getSpeed()) > 0.1f) 
            transform.Rotate(0, direction * turnSpeed * speed / maxSpeed * Time.deltaTime, 0);
    }
}
