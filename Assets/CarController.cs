using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public float acceleration = 1f;
    public float brakeForce = 4f;
    public float maxSpeed = 5f;
    public float turnSpeed = 90f;
    public GameObject ghostCarPrefab;

    public bool isGhost = false;
    private bool hasLeftFinishLine = false;

    public event Action<float> OnSpeedChanged;
    public event Action<float> TimerChanged;

    private float speed = 0f;
    private float timer = 0f;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void LeftFinishLine() => hasLeftFinishLine = true;

    private void Update()
    {
        speed = GetSpeed();
        OnSpeedChanged?.Invoke(speed);
        timer += Time.deltaTime;
        TimerChanged?.Invoke(timer);
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }

    public float GetSpeed() => Mathf.Abs(rb.linearVelocity.magnitude);

    public void Accelerate() => rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
    public void Brake() => rb.AddForce(-transform.forward * brakeForce, ForceMode.Acceleration);
    public void Turn(float direction)
    {
        if (Mathf.Abs(GetSpeed()) > 0.1f) 
            transform.Rotate(0, direction * turnSpeed * speed / maxSpeed * Time.deltaTime, 0);
    }

    public void FinishLap()
    {
        if (!hasLeftFinishLine)
            return;

        hasLeftFinishLine = false;

        if (isGhost)
        {
            Destroy(gameObject);
            return;
        }

        timer = 0f;

        var ghost = Instantiate(ghostCarPrefab, originalPosition, originalRotation);

        ghost.GetComponent<CarController>().isGhost = true;
        var recorder = GetComponent<PlayerInputRecorder>();
        ghost.GetComponent<GhostCarPlayer>().replayCommands = recorder.GetRecordedCommands();

        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }
}
