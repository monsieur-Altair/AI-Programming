using System;
using UnityEditor.Rendering;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    public float MaxSpeed { private set; get; }

    private Steering _steering;
    private Transform _transform;
    private float _rotationAngle;
    private float _speed;
    private float _coefficient;
    
    private void Start()
    {
        _steering = new Steering();
        _transform = GetComponent<Transform>();
        MaxSpeed = maxSpeed;
        _speed = MaxSpeed;
    }

    public void SetSteering(Steering steering1)
    {
        this._steering = steering1;
    }

    public virtual void Update()
    {
        //Debug.DrawRay(transform.position,transform.forward*8,Color.black);
        if (_rotationAngle != 0.0f)
            _transform.Rotate(Vector3.up, _rotationAngle * _coefficient * Time.deltaTime);

        _transform.Translate(transform.forward * _coefficient * _speed * Time.deltaTime, Space.World);

    }

    public virtual void LateUpdate()
    {
        _speed = _steering.LinearSpeed;
        _coefficient = _steering.Weight;
        _rotationAngle = _steering.RotationAngle;
        
        if (_speed > MaxSpeed)
        {
            _speed = MaxSpeed;
        }

        _steering = new Steering();
    }

    public void EnableMaxSpeed()
    {
        _speed = MaxSpeed;
    }
}