using System;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Behaviours
{
    public class Align : AgentBehaviour
    {
        private float _clockwise;
        public float angularSpeed;
        public float minAngle;


        protected Transform Transform;

        public override void Awake()
        {
            base.Awake();
            Transform = transform;
        }

        public override Steering GetSteering()
        {
            var steering = new Steering();

            var destination = target.transform.position;
            var toTargetDirection = (destination - Transform.position).normalized;
            var currentDirection = Transform.forward;
            var cross1 = Vector3.Cross(currentDirection, toTargetDirection);
            _clockwise = cross1.y/Mathf.Abs(cross1.y);

            var rotationAngle = Vector3.Angle(currentDirection, toTargetDirection);
            Debug.Log(rotationAngle);
            if (rotationAngle > minAngle)
            {
                agent.rotationAngle = angularSpeed * _clockwise;
            }
            else
            {
                agent.rotationAngle = 0.0f;
                transform.LookAt(target.transform);
            }
            //not work with steering.angular
            return steering;
        }
    }
}