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
                steering.LinearSpeed = 0.0f;
            }
            else
            {
                var agentSpeed = (distance > slowRadius) ? Agent.MaxSpeed : (Agent.MaxSpeed * distance / slowRadius);
                steering.LinearSpeed = agentSpeed;
            }
            
//            Debug.Log(steering.linearSpeed);
           
            return steering;
        }
    }
}