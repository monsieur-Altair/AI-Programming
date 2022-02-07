using UnityEngine;

namespace Behaviours
{
    public class AgentBehaviour : MonoBehaviour
    {
        public GameObject target;
        protected Agent agent;

        public virtual void Awake()
        {
            agent = gameObject.GetComponent<Agent>();
        }

        public virtual void Update()
        {
            agent.SetSteering(GetSteering());
        }

        public virtual Steering GetSteering()
        {
            return new Steering();
        }

        public virtual void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }
    }
}