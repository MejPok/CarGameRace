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
    public float brakePower = 20f;
    public float maxSpeed = 20f;

    [Header("Steering")]
    public float turnSpeed = 200f;

    [Header("Friction")]
    [Range(0f, 1f)]
    public float grip = 0.2f;      // sideways grip
    [Range(0f, 1f)]
    public float drag = 0.98f;     // global drag

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (throttle > 0)
        {
            rb.AddForce(forward * throttle * enginePower, ForceMode2D.Force);
        }
        else if (throttle < 0)
        {
            rb.AddForce(forward * throttle * brakePower, ForceMode2D.Force);
        }
    }

    void ApplySteering(float steer)
    {   
        
        float forwardSpeed = Vector2.Dot(rb.velocity, transform.up);

        if (Mathf.Abs(forwardSpeed) < 0.1f)
            return;
        
        float direction = forwardSpeed >= 0 ? 1f : -1f;

        float speedFactor = rb.velocity.magnitude / maxSpeed;
        speedFactor = Mathf.Clamp01(speedFactor);

        rb.rotation -= steer * direction * turnSpeed * speedFactor * Time.fixedDeltaTime;
    }

    void ApplyFriction()
    {
        Vector2 forward = transform.up;
        Vector2 right = transform.right;

        float forwardVel = Vector2.Dot(rb.velocity, forward);
        float sideVel = Vector2.Dot(rb.velocity, right);

        sideVel *= grip; // kill sideways sliding

        rb.velocity =
            forward * forwardVel +
            right * sideVel;

        rb.velocity *= drag; // global drag
    }

    void LimitSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

}
