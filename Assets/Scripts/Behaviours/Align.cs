using System;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Behaviours
{
    public class Align : AgentBehaviour
    {
        //public float targetRadius;
        //public float slowRadius;
        //private const float TimeToTarget=0.1f;
        private float _clockwise;
        [SerializeField] private float angularSpeed;
        public float minAngle;


        private Transform _transform;

        public override void Awake()
        {
            base.Awake();
            _transform = transform;
        }

        public override Steering GetSteering()
        {
            var steering = new Steering();

            var destination = targetTransform.position;
            var toTargetDirection = (destination - _transform.position).normalized;
            var currentDirection = _transform.forward;
            var cross1 = Vector3.Cross(currentDirection, toTargetDirection);
            _clockwise = cross1.y/Mathf.Abs(cross1.y);

            var rotationAngle = Vector3.Angle(currentDirection, toTargetDirection);
            
            agent.rotationAngle = rotationAngle>minAngle? angularSpeed * _clockwise: 0.0f;
            //Debug.Log(agent.rotationAngle);
            
            
            //var angularAccel = Mathf.Abs(steering.angular);
            /*if (angularAccel > agent.maxAngularAccel)
            {
                steering.angular /= angularAccel;
                steering.angular *= agent.maxAngularAccel;
            }*/
            
            //var targetOrientation = target.GetComponent<Agent>().orientationAngle;
            //var rotation = targetOrientation - agent.orientationAngle;
            /*rotation = MapToRange(rotation);
            var rotationSize = Mathf.Abs(rotation);

            if (rotationSize < targetRadius)//////////
                return steering;

            var targetRotation = (rotationSize > slowRadius)
                ? agent.maxRotation
                : agent.maxRotation * rotationSize / slowRadius;

            targetRotation *= rotation / rotationSize;

            steering.angular = (targetRotation - agent.rotationAngle) / TimeToTarget;
            var angularAccel = Mathf.Abs(steering.angular);
            if (angularAccel > agent.maxAngularAccel)
            {
                steering.angular /= angularAccel;
                steering.angular *= agent.maxAngularAccel;
            }
            */


            //Debug.Log(steering.angular);
            return steering;
        }
    }
}