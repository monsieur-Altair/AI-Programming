using UnityEngine;

namespace Behaviours
{
    public class AgentBehaviour : MonoBehaviour
    {
        [SerializeField] protected GameObject target;
        protected Agent Agent;

        public virtual void Awake()
        {
            Agent = gameObject.GetComponent<Agent>();
        }

        public virtual void Update()
        {
            Agent.SetSteering(GetSteering());
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