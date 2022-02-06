using UnityEngine;

namespace Behaviours
{
    public class Leave : AgentBehaviour
    {
        public float escapeRadius;
        public float dangerRadius;
        private const float TimeToTarget = 0.1f;

        public override Steering GetSteering()
        {
            var steering = new Steering();
            var direction = transform.position - target.transform.position;
            var distance = direction.magnitude;

            if (distance > dangerRadius)
                return steering;

            var reduce = (distance < escapeRadius) ? 0.0f : (agent.maxSpeed * distance / dangerRadius);
            var targetSpeed = agent.maxSpeed - reduce;
            

            var desiredVelocity = direction;
            desiredVelocity.Normalize();
            desiredVelocity *= targetSpeed;

            steering.linear = (desiredVelocity - agent.velocity)/TimeToTarget;
            if (steering.linear.magnitude > agent.maxAccel)
            {
                steering.linear.Normalize();
                steering.linear *= agent.maxAccel;
            }

            return steering;
        }
    }
}