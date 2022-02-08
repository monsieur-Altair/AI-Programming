using UnityEngine;

namespace Behaviours
{
    public class Flee : Seek
    {
        public override void Awake()
        {
            base.Awake();
            AlignAlgorithm.EnableReversing();
        }
        /*public override Steering GetSteering()
        {
            var steering = new Steering();
            steering.linearSpeed = transform.position - target.transform.position ;
            steering.linearSpeed.Normalize();
            steering.linearSpeed *= agent.maxAccel;
            return steering;
        }  */
    }
}