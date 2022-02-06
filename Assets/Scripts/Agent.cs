using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxAccel;
    [HideInInspector] public float rotationAngle;
    public float maxSpeed;
    //[HideInInspector] public float orientationAngle;
    [HideInInspector] public Vector3 velocity;
    public float maxAngularAccel;

    //private const float MinOrientation = 0.0f;
   //private const float MaxOrientation = 360.0f;
        
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
        //orientationAngle += rotationAngle * Time.deltaTime;
        
        //LimitOrientation();

        if (rotationAngle != 0.0f)
            _transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
        
        _transform.Translate(displacement,Space.World);
        //_transform.rotation = new Quaternion();
        //_transform.Rotate(Vector3.up, orientationAngle);
        //Debug.Log(steering.angular);
       
    }

    public virtual void LateUpdate()
    {
        //Debug.Log(steering.angular);
        //rotationAngle = steering.angular * Time.deltaTime;
        velocity += steering.linear * Time.deltaTime;

        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }

        /*if (steering.angular == 0.0f)
            rotationAngle = 0.0f;*/

        if (steering.linear.magnitude == 0.0f)
            velocity = Vector3.zero;

        steering = new Steering();
    }

    /*private void LimitOrientation()
    {
        if (orientationAngle > MaxOrientation)
            orientationAngle -= MaxOrientation;
        else if (orientationAngle < MinOrientation)
            orientationAngle += MaxOrientation;
    }*/
}