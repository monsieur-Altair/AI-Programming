namespace Behaviours
{
    public class Seek : AgentBehaviour
    {
        public override Steering GetSteering()
        {
            var steering = new Steering();
            steering.linear = target.transform.position - transform.position;
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
            return steering;
        }
    }
}