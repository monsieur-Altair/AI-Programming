using UnityEngine;
using UnityEngine.UIElements;

namespace Behaviours
{
    public class Arrive : Seek
    {
        public float targetRadius;
        public float slowRadius;

        public override Steering GetSteering()
        {
            var steering = base.GetSteering();
            var direction = target.transform.position - transform.position;
            var distance = direction.magnitude;

            if (distance < targetRadius)
            {
                steering.linearSpeed = 0.0f;
            }
            else
            {
                var agentSpeed = (distance > slowRadius) ? agent.MaxSpeed : (agent.MaxSpeed * distance / slowRadius);
                steering.linearSpeed = agentSpeed;
            }
            
//            Debug.Log(steering.linearSpeed);
           
            return steering;
        }
    }
}