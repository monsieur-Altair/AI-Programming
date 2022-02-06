using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxAccel;
    [HideInInspector] public float rotationAngle;
    public float maxSpeed;
    [HideInInspector] public Vector3 velocity;
    public float maxAngularAccel;

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

        if (rotationAngle != 0.0f)
            _transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
        
        _transform.Translate(displacement,Space.World);

    }

    public virtual void LateUpdate()
    {
        velocity += steering.linear * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        if (steering.linear.magnitude == 0.0f)
            velocity = Vector3.zero;

        steering = new Steering();
    }
}