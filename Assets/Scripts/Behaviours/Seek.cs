using UnityEngine;

namespace Behaviours
{
    public class Seek : AgentBehaviour
    {
        private Align _alignAlgorithm;

        public override void Awake()
        {
            base.Awake();
            _alignAlgorithm = gameObject.AddComponent<Align>();
            _alignAlgorithm.SetTarget(target);
            _alignAlgorithm.minAngle = 2;
            _alignAlgorithm.angularSpeed = 360;
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
            Debug.Log("done");
            base.SetTarget(newTarget);
            _alignAlgorithm.SetTarget(newTarget);
        }
    }
}