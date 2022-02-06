using UnityEngine;

namespace Behaviours
{
    public class AgentBehaviour : MonoBehaviour
    {
        public GameObject target;
        protected Transform targetTransform;
        protected Agent agent;
        

        public float MapToRange(float rotation)
        {
            rotation %= 360.0f;
            if(Mathf.Abs(rotation)>180.0f)
                if (rotation < 0.0f)
                    rotation += 360.0f;
                else
                    rotation -= 360.0f;
            return rotation;
            //rotation=[-180;180];
        }
        
        public virtual void Awake()
        {
            agent = gameObject.GetComponent<Agent>();
            targetTransform = target.transform;
        }

        public virtual void Update()
        {
            agent.SetSteering(GetSteering());
        }

        public virtual Steering GetSteering()
        {
            return new Steering();
        }
    }
}