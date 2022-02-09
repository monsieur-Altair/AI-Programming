using UnityEngine;

namespace Behaviours
{
    public class Align : AgentBehaviour
    {
        private float _clockwise;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float minAngle;
        //private float _minDistance = 1.5f;
        private bool _isReversing;
        private Transform _transform;
        //private Vector3 _toTarget;

        public override void Awake()
        {
            base.Awake();
            _transform = transform;
        }

        public void SetSpeedAndAngle(float speed, float angle)
        {
            angularSpeed = speed;
            minAngle = angle;
        }

        public override Steering GetSteering()
        {
            var steering = new Steering();

            var destination = target.transform.position;
            var toTarget = (destination - _transform.position).normalized;
            if (_isReversing)
                toTarget *= -1;//toTarget=fromTarget
            
            //var distance = Vector3.Distance(destination, Transform.position);
            //var coeff = (distance > _minDistance) ? 1.0f : (_minDistance / distance);
            var currentDirection = _transform.forward;
            var cross1 = Vector3.Cross(currentDirection, toTarget);
            _clockwise = cross1.y/Mathf.Abs(cross1.y);

            var rotationAngle = Vector3.Angle(currentDirection, toTarget);
            //Debug.Log(rotationAngle);
            if (rotationAngle > minAngle)
            {
               // agent.rotationAngle = /*coeff * */angularSpeed * _clockwise;
               steering.RotationAngle = angularSpeed * _clockwise;
            }
            else
            {
                //agent.rotationAngle = 0.0f;
                steering.RotationAngle = 0.0f;
                transform.rotation = Quaternion.LookRotation(toTarget);
            }
            return steering;
        }

        public void EnableReversing()
        {
            _isReversing = true;
        }
    }
}