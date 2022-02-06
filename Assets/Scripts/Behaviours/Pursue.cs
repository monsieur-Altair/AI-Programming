using UnityEngine;

namespace Behaviours
{
    public class Pursue : Seek
    {
        public float maxPredictionTime;
        private GameObject _targetAux;
        private Agent _targetAgent;

        public override void Awake()
        {
            base.Awake();
            _targetAgent = target.GetComponent<Agent>();
            _targetAux = target;
            target = new GameObject();
        }

        private void OnDestroy()
        {
            Destroy(_targetAux);
        }

        public override Steering GetSteering()
        {
            var targetAuxPos = _targetAux.transform.position;
            var distance = (targetAuxPos - transform.position).magnitude;
            var speed = agent.velocity.magnitude;
            var predictionTime = (speed <= distance / maxPredictionTime) ? maxPredictionTime : (distance / speed);

            target.transform.position = targetAuxPos + _targetAgent.velocity * predictionTime;
            return base.GetSteering();
        }
    }
}