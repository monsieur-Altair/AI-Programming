using UnityEngine;

namespace Behaviours
{
    public class Seek : AgentBehaviour
    {
        private Align _alignAlgorithm;
        [SerializeField] private float angularSpeed=360f;
        [SerializeField] private float minAngle=2;

        public override void Awake()
        {
            base.Awake();
            _alignAlgorithm = gameObject.AddComponent<Align>();
            _alignAlgorithm.SetTarget(target);
            _alignAlgorithm.minAngle = minAngle;
            _alignAlgorithm.angularSpeed = angularSpeed;
        }

        public override Steering GetSteering()
        {
            var steering = _alignAlgorithm.GetSteering();
            
            //var steering = new Steering();
            steering.linear = target.transform.position - transform.position;
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
            return steering;
        }

        public override void SetTarget(GameObject newTarget)
        {
            base.SetTarget(newTarget);
            _alignAlgorithm.SetTarget(newTarget);
        }
    }
}