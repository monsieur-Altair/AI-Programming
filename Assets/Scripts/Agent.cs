using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxAccel;
    [HideInInspector] public float rotation;
    public float maxSpeed;
    [HideInInspector] public float orientation;
    [HideInInspector] public Vector3 velocity;

    private const float MinOrientation = 0.0f;
    private const float MaxOrientation = 360.0f;
        
    protected Steering steering;

    private Transform _transform;
    
    private void Start()
    {
        steering = new Steering();
        velocity=Vector3.zero;
        _transform = GetComponent<Transform>();
    }

    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }

    public virtual void Update()
    {
        var displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;
        
        LimitOrientation();
        
        _transform.Translate(displacement,Space.World);
        _transform.rotation = new Quaternion();
        _transform.Rotate(Vector3.up, orientation);
    }

    public virtual void LateUpdate()
    {
        rotation += steering.angular * Time.deltaTime;
        velocity += steering.linear * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        if (steering.angular == 0.0f)
            rotation = 0.0f;

        if (steering.linear.magnitude == 0.0f)
            velocity = Vector3.zero;

        steering = new Steering();
    }

    private void LimitOrientation()
    {
        if (orientation > MaxOrientation)
            orientation -= MaxOrientation;
        else if (orientation < MinOrientation)
            orientation += MaxOrientation;
    }
}