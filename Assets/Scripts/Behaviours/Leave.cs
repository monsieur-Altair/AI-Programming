
namespace Behaviours
{
    public class Leave : Flee
    {
        public float escapeRadius;

        public float dangerRadius;

        public override Steering GetSteering()
        {
            var steering = new Steering();
            var direction = transform.position - target.transform.position;
            var distance = direction.magnitude;

            if (distance > escapeRadius)
                steering.LinearSpeed = 0.0f;
            else
            {
                var coeff= (distance <= dangerRadius) ? 1.0f : (dangerRadius/distance);
                steering.LinearSpeed = coeff*Agent.MaxSpeed;
            }

            return steering;
        }
    }
}