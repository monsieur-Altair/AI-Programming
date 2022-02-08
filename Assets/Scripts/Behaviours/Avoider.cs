using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
    public class Avoider : Seek
    {
        [SerializeField] private float avoidingRadius;
        
        private bool _isPreviousRight;
        private GameObject _firstTarget;
        private Transform _transform;
        private GameObject _temporaryTarget;
        private bool _isSwappedToTemporary;
        [SerializeField] private float maxOffsetLength = 6.0f;
        private float _offsetLength = 6.0f;
        
        public override void Awake()
        {
            base.Awake();
            _temporaryTarget = new GameObject();
            _transform = transform;
            base.SetTarget(target);
            _firstTarget = target;
            _offsetLength = maxOffsetLength;
        }

        public override Steering GetSteering()
        {
            LaunchRays();
            
            var distance = Vector3.Distance(target.transform.position, _transform.position);
            //Debug.Log("target pos= "+target.transform.position);
            //Debug.Log("distance "+distance);
            
            if (distance < 2f)
            {
                if (_isSwappedToTemporary)
                {
                    base.SetTarget(_firstTarget);
                    _isSwappedToTemporary = false;
                    Debug.Log("approached");
                }
                else
                {
                    return new Steering();
                }
            }
            
            return base.GetSteering();
        }

        private void LaunchRays()
        {
            var currentPos = _transform.position;
            
            Debug.DrawRay( currentPos,_transform.forward*avoidingRadius,Color.black);

            if (Physics.Raycast(currentPos,
                _transform.forward,
                out var hitInfo,
                avoidingRadius))
            {
                if (hitInfo.transform.position == _firstTarget.transform.position)
                {
                    return;
                }
                //get perpendicular vector to current forward vector = left offset 
                var hitPoint = hitInfo.point;
                _offsetLength = (hitInfo.distance > avoidingRadius) ? maxOffsetLength : hitInfo.distance;
                var newTargetOffset = Vector3.Cross(transform.forward, Vector3.up).normalized;
                var targetPoint = hitPoint + newTargetOffset.normalized * _offsetLength;
                _temporaryTarget.transform.position = targetPoint;
                Debug.DrawRay( hitPoint,newTargetOffset.normalized * _offsetLength,Color.black,0.5f);
                SwapTarget();
                _isSwappedToTemporary = true;
            }
        }


        void SwapTarget()
        {
            if (_isSwappedToTemporary == false)
            {
                //target = _temporaryTarget;
                base.SetTarget(_temporaryTarget);
                Debug.Log("Swapped");
            }
        }
        
        /*private void LaunchRays()
        {
            var currentPos = transform.position;
            var rightTargetPoint=Vector3.zero;
            var leftTargetPoint=Vector3.zero;
            
            Debug.DrawRay( transform.TransformPoint(_rightOffset),transform.forward*avoidingRadius,Color.black);
            Debug.DrawRay( transform.TransformPoint(-_rightOffset),transform.forward*avoidingRadius,Color.black);
            
            if (Physics.Raycast(transform.TransformPoint(_rightOffset), 
                transform.forward,
                out var hitInfoRight, 
                avoidingRadius))
            {
                _isRight = true;
                //get perpendicular vector to current forward vector = offset 
                var newTargetOffset = Vector3.Cross(transform.forward, Vector3.up).normalized;
                leftTargetPoint = hitInfoRight.transform.position + newTargetOffset.normalized * OffsetLength;
                _temporaryTarget.transform.position = leftTargetPoint;
            }
            
            if (Physics.Raycast(transform.TransformPoint(-_rightOffset), 
                transform.forward, 
                out var hitInfoLeft,
                avoidingRadius))
            {
                _isLeft = true;
                var newTargetOffset = -1 * Vector3.Cross(transform.forward, Vector3.up).normalized;
                rightTargetPoint = hitInfoLeft.transform.position + newTargetOffset.normalized * OffsetLength;
                
                _temporaryTarget.transform.position = rightTargetPoint;
            }
            
            if (_isLeft && _isRight)
            {
                if (_isPreviousRight)
                {
                    _temporaryTarget.transform.position = rightTargetPoint;
                    _isPreviousRight=true;
                }
                else
                {
                    _temporaryTarget.transform.position = leftTargetPoint;
                    _isPreviousRight = false;
                }
            }

            _isPreviousRight = (_temporaryTarget.transform.position == rightTargetPoint);
            _isLeft = _isRight = false;
            
            //если левый сработал, смещаем вправо и наобоорот, если оба, то туда, где предыдущий

        }*/
        
        
        
        
    }
}