using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [HideInInspector] public float rotationAngle;
    protected float speed;
    [SerializeField] private float maxSpeed;
    public float MaxSpeed { private set; get; }

    protected Steering steering;

    private Transform _transform;
    
    private void Start()
    {
        steering = new Steering();
        _transform = GetComponent<Transform>();
        MaxSpeed = maxSpeed;
        speed = MaxSpeed;
    }

    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }

    public virtual void Update()
    {
        Debug.DrawRay(transform.position,transform.forward*8,Color.black);
        if (rotationAngle != 0.0f)
            _transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
        
        //_transform.Translate(displacement,Space.World);
        _transform.Translate(transform.forward*speed*Time.deltaTime,Space.World);

    }

    public virtual void LateUpdate()
    {
        speed = steering.linearSpeed;

        if (speed > MaxSpeed)
        {
            speed = MaxSpeed;
        }

        steering = new Steering();
    }

    public void EnableMaxSpeed()
    {
        speed = MaxSpeed;
    }
}