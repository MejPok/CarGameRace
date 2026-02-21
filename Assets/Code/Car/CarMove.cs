using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CarDrifting))]
public class CarMove : MonoBehaviour
{
    [Header("Movement")]
    public float enginePower = 15f;
    public float brakePower = 30f;
    public float reversePower = 8f;
    public float maxForwardSpeed = 20f;
    public float maxReverseSpeed = 7f;

    [Header("Steering")]
    public float turnSpeed = 200f;

    [Header("Friction")]
    [Range(0f, 1f)]
    public float grip = 0.2f;      // sideways grip
    [Range(0f, 1f)]
    public float drag = 0.98f;     // global drag

    Rigidbody2D rb;
    CarDrifting carDrift;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        carDrift = GetComponent<CarDrifting>();
    }

    void FixedUpdate()
    {
        float throttle = Input.GetAxis("Vertical");   // W/S
        float steer = Input.GetAxis("Horizontal");     // A/D

        ApplyEngine(throttle);
        ApplySteering(steer);
        ApplyFriction();
        LimitSpeed();
    }

    void ApplyEngine(float throttle)
{
    Vector2 forward = transform.up;
    float forwardSpeed = Vector2.Dot(rb.velocity, forward);

    // Forward
    if (throttle > 0)
    {
        rb.AddForce(forward * throttle * enginePower, ForceMode2D.Force);
    }
    // Brake
    else if (throttle < 0 && forwardSpeed > 0.5f)
    {
        rb.AddForce(-forward * brakePower, ForceMode2D.Force);
    }
    // Reverse
    else if (throttle < 0 && forwardSpeed <= 0.5f)
    {
        rb.AddForce(forward * throttle * reversePower, ForceMode2D.Force);
    }
}

    void ApplySteering(float steer)
    {   
        
        float forwardSpeed = Vector2.Dot(rb.velocity, transform.up);

        if (Mathf.Abs(forwardSpeed) < 0.1f)
            return;
        
        float direction = forwardSpeed >= 0 ? 1f : -1f;

        float speedFactor = rb.velocity.magnitude / maxForwardSpeed;
        speedFactor = Mathf.Clamp01(speedFactor);


        rb.rotation -= steer * direction * turnSpeed * ( 1f - speedFactor) * Time.fixedDeltaTime;
    }

    void ApplyFriction()
    {
        Vector2 forward = transform.up;
        Vector2 right = transform.right;

        float forwardVel = Vector2.Dot(rb.velocity, forward);
        float sideVel = Vector2.Dot(rb.velocity, right);

        float currentGrip = carDrift.drifting ? carDrift.driftModifier : grip;
        sideVel *= currentGrip; // kill sideways sliding

        rb.velocity =
            forward * forwardVel +
            right * sideVel;

        rb.velocity *= drag; // global drag
    }

    void LimitSpeed()
    {
        Vector2 forward = transform.up;
        float forwardSpeed = Vector2.Dot(rb.velocity, forward);

        if (forwardSpeed > maxForwardSpeed)
            rb.velocity = forward * maxForwardSpeed;

        if (forwardSpeed < -maxReverseSpeed)
            rb.velocity = -forward * maxReverseSpeed;
    }

}
