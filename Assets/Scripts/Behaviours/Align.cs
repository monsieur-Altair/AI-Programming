using System;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Behaviours
{
    public class Align : AgentBehaviour
    {
        private float _clockwise;
        [SerializeField] private float angularSpeed;
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

            agent.rotationAngle = rotationAngle > minAngle ? angularSpeed * _clockwise : 0.0f;
            //not work with steering.angular
            return steering;
        }
    }
}