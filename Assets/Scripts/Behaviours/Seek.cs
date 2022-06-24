using UnityEngine;

namespace Behaviours
{
    public class Seek : AgentBehaviour
    {
        protected Align AlignAlgorithm;
        [SerializeField] private float angularSpeed=360f;

        [SerializeField] private float minAngle=2;

        public override void Awake()
        {
            base.Awake();
            AlignAlgorithm = gameObject.AddComponent<Align>();
            AlignAlgorithm.SetTarget(target);
            AlignAlgorithm.SetSpeedAndAngle(angularSpeed, minAngle);
        }

        public override Steering GetSteering()
        {
            var steering = AlignAlgorithm.GetSteering();
            steering.LinearSpeed = Agent.MaxSpeed;
            return steering;
        }

        public override void SetTarget(GameObject newTarget)
        {
            base.SetTarget(newTarget);
            AlignAlgorithm.SetTarget(newTarget);
        }
    }
}