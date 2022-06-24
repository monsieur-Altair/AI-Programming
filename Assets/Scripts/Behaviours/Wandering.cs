using UnityEngine;

namespace Behaviours
{
    public class Wandering : Arrive
    {
        [SerializeField] private float maxSearchRadius=30f;

        [SerializeField] private float wanderingDistance=25f;

        [SerializeField] private float timeBetweenWandering=1.5f;

        private float _currentTime;

        public override void Awake()
        {
            base.Awake();
            target = new GameObject
            {
                transform =
                {
                    position = new Vector3(0.0f, transform.position.y, 0.0f)
                }
            };
            base.SetTarget(target);
        }

        public override Steering GetSteering()
        {
            var position = transform.position;
            _currentTime += Time.deltaTime;
            if (_currentTime < timeBetweenWandering)
            {
                return base.GetSteering();
            }

            _currentTime = 0.0f;
            
            var randomX = Random.Range(-maxSearchRadius, maxSearchRadius);
            var randomZ = Random.Range(-maxSearchRadius, maxSearchRadius);
            var targetPosition = new Vector3(randomX, 0.0f, randomZ).normalized*wanderingDistance;
            target.transform.position = new Vector3(targetPosition.x, position.y, targetPosition.z);
//            Debug.Log(target.transform.position);
            
            return base.GetSteering();
        }

    }
}