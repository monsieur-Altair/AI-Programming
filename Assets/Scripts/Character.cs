using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Camera _mainCamera;
    private Transform _transform;
    private Vector3 _destination;
    private float _yPos;
    [SerializeField] private float speed;
    [SerializeField] float MaxSlowRadius = 7.5f;
    private float _speedCoefficient;

        private void Awake()
    {
        _mainCamera=Camera.main;
        _transform = transform;
        _destination=_transform.position;
        _yPos = _destination.y;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _destination = hitInfo.point;
                _destination.y = _yPos;
            }
        }
        
        var direction = _destination - _transform.position;
        var distance = direction.magnitude;
        if (distance==0.0f)
            return;

        _speedCoefficient = (distance >= MaxSlowRadius) ? 1.0f : distance / MaxSlowRadius;
        
        _transform.Translate(direction.normalized*speed*_speedCoefficient*Time.deltaTime);
    }
}