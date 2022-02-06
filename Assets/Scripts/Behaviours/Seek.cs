namespace Behaviours
{
    public class Seek : AgentBehaviour
    {
        private Align _alignAlgorithm;

        public override void Awake()
        {
            base.Awake();
            //_alignAlgorithm = gameObject.AddComponent<Align>();
            //_alignAlgorithm.SetTarget(target);
        }

        public override Steering GetSteering()
        {
            //var steering = _alignAlgorithm.GetSteering();
            var steering = new Steering();
            steering.linear = target.transform.position - transform.position;
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
            return steering;
        }
    }
}