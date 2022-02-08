using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Camera _mainCamera;
    private Transform _transform;
    private Vector3 _destination;
    private float _yPos;
    [SerializeField] private float speed;
    [SerializeField] float slowRadius = 7.5f;
    [SerializeField] float stopRadius = 0.3f;
    [SerializeField] float minAngle = 3f;
    [SerializeField] float angularSpeed = 180f;
    private float _speedCoefficient;
    private float _clockwise = 1.0f;
    
    

    private void Awake()
    {
        _mainCamera=Camera.main;
        _transform = transform;
        _destination=_transform.position;
        _yPos = _destination.y;
    }

    private void Update()
    {
        CheckInput();
        
        var direction = _destination - _transform.position;
       
        RotateTo(direction);
        
        TranslateTo(direction);
    }

    private void RotateTo(Vector3 direction)
    {
        var angle = Vector3.Angle(_transform.forward, direction.normalized);

        if (angle > minAngle)
        {
            _transform.Rotate(Vector3.up, _clockwise * Time.deltaTime * angularSpeed);
        }
    }

    private void TranslateTo(Vector3 direction)
    {
        var distance = direction.magnitude;
        if (distance > stopRadius)
        {
            _speedCoefficient = (distance >= slowRadius) ? 1.0f : distance / slowRadius;
            _transform.Translate(Vector3.forward * speed * _speedCoefficient * Time.deltaTime);
        }
    }
    
    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _destination = hitInfo.point;
                _destination.y = _yPos;
                
                var toTargetDirection = (_destination - _transform.position).normalized;
                var currentDirection = _transform.forward;
                var cross1 = Vector3.Cross(currentDirection, toTargetDirection);
                _clockwise = cross1.y/Mathf.Abs(cross1.y);
            }
        }
    }

}