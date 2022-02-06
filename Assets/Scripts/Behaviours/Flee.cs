using UnityEngine;

namespace Behaviours
{
    public class Flee : AgentBehaviour
    {
        public override Steering GetSteering()
        {
            var steering = new Steering();
            steering.linear = transform.position - target.transform.position ;
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
            return steering;
        }  
    }
}