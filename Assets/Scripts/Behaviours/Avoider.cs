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
                    //Debug.Log("approached");
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
                //get perpendicular vector to current forward vector = left offset (it depends on cross product) 
                var hitPoint = hitInfo.point;
                _offsetLength = (hitInfo.distance > avoidingRadius) ? maxOffsetLength : hitInfo.distance;
                var newTargetOffset = Vector3.Cross(transform.forward, Vector3.up).normalized;
                var targetPoint = hitPoint + newTargetOffset.normalized * _offsetLength;
                _temporaryTarget.transform.position = targetPoint;
                Debug.DrawRay( hitPoint,newTargetOffset.normalized * _offsetLength,Color.black,2);
                SwapTarget();
                _isSwappedToTemporary = true;
            }
        }


        void SwapTarget()
        {
            if (_isSwappedToTemporary == false)
            {
                base.SetTarget(_temporaryTarget);
            }
        }
    }
}