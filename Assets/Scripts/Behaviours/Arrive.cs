using UnityEngine;
using UnityEngine.UIElements;

namespace Behaviours
{
    public class Arrive : AgentBehaviour
    {
        public float targetRadius;
        public float slowRadius;
        private const float TimeToTarget = 0.1f;

        public override Steering GetSteering()
        {
            var steering = new Steering();
            var direction = target.transform.position - transform.position;
            var distance = direction.magnitude;

            if (distance < targetRadius)
                return steering;

            var targetSpeed = (distance > slowRadius) ? agent.maxSpeed : (agent.maxSpeed * distance / slowRadius);



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