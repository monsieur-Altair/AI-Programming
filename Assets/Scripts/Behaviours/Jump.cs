using Shooting;
using UnityEngine;

namespace Behaviours
{
    class Jump : Arrive
    {
        private Transform _transform;
        [SerializeField] private float avoidingRadius=8.0f;
        private bool _isOnGround;
        private float _ownHeight;
        private Projectile _projectileBehaviour;
        private Vector3 _landPos;
        private Quaternion _selfRotation;
        
        public override void Awake()
        {
            base.Awake();
            _transform = transform;
            _isOnGround = true;
            _ownHeight = GetComponent<CapsuleCollider>().height / 2.0f;
            base.SetTarget(target);
            _projectileBehaviour = gameObject.AddComponent<Projectile>();
            _projectileBehaviour.Fallen += EnableBaseAlgorithm;
            _projectileBehaviour.enabled = false;
            _landPos=Vector3.zero;
        }

        private void EnableBaseAlgorithm(Projectile projectile)
        {
            _isOnGround = true;
            projectile.enabled = false;
            _transform.position = _landPos;
            _transform.rotation = _selfRotation;
        }
        
        public override Steering GetSteering()
        {
            LaunchRays();
            
            var steering = base.GetSteering();
            if (_isOnGround == false)
                steering.Weight = 0.0f;
            return steering;
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
                if (_isOnGround==false)
                    return;

                
                var pos = _transform.position;
                var direction = hitInfo.point - pos;
                var localDir = _transform.InverseTransformDirection(direction);
                _landPos = _transform.TransformPoint(2 * localDir);
                
                var obstacle = hitInfo.collider.gameObject;
                var res = obstacle.TryGetComponent<BoxCollider>(out var boxCollider);
                if (!res)
                    return;
                
                var obstacleHeight = boxCollider.size.y / 2.0f;
                var h = obstacleHeight + _ownHeight;
                var l = Vector3.Distance(_landPos, pos);
                var a = Mathf.Atan(4 * h / l);
                var speed = Mathf.Sqrt(2 * 9.81f * h) / Mathf.Sin(a);
                //мб разбить в корутине на итерации
                var jumpDir = Projectile.GetFireDirection(pos, _landPos, speed);

                _projectileBehaviour.enabled = true;
                _selfRotation = _transform.rotation;
                _projectileBehaviour.Launch(pos, jumpDir, speed, _landPos.y);
                _isOnGround = false;

                Debug.Log("find="+_landPos);
                Debug.DrawRay( _landPos,Vector3.up * 3.0f,Color.yellow,2.5f);
                Debug.DrawRay( pos,jumpDir.normalized * avoidingRadius,Color.magenta,1);
            }
        }
    }
}