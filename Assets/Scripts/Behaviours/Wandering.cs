using UnityEngine;

namespace Behaviours
{
    public class Wandering : Align
    {
        public float maxSearchRadius;
        public float wanderingDistance;
        public float timeBetweenWandering;
        private float _currentTime;
        private Arrive _arriveAlgorithm;

        public override void Awake()
        {
            base.Awake();
            target = new GameObject();
            target.transform.position = new Vector3(0.0f, transform.position.y, 0.0f);
            _arriveAlgorithm = GetComponent<Arrive>();
            _arriveAlgorithm.SetTarget(target);
        }

        public override Steering GetSteering()
        {
            var position = Transform.position;
            _currentTime += Time.deltaTime;
            if (_currentTime < timeBetweenWandering)
            {
                base.GetSteering();
                return _arriveAlgorithm.GetSteering();
            }

            _currentTime = 0.0f;
            var randomX = Random.Range(-maxSearchRadius, maxSearchRadius);
            var randomZ = Random.Range(-maxSearchRadius, maxSearchRadius);
            var targetPosition = new Vector3(randomX, 0.0f, randomZ).normalized*wanderingDistance;
            target.transform.position = new Vector3(targetPosition.x, position.y, targetPosition.z);
            Debug.Log(target.transform.position);
            base.GetSteering();
            return _arriveAlgorithm.GetSteering();
        }

        /*private Steering SeekTarget()
        {
            var steering = base.GetSteering(); 
            steering.linear = target.transform.position - Transform.position; 
            steering.linear.Normalize(); 
            steering.linear *= agent.maxAccel;
            return steering;
        }*/
    }
}